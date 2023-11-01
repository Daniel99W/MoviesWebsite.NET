import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { CreateMovieDto } from 'src/app/dtos/CreateMovieDto';
import { environment } from 'src/environments/environment';
import { switchMap } from 'rxjs';
import { env } from 'process';
import { AngularFireStorage } from '@angular/fire/compat/storage';
import { GetMoviesQueryParametersDto } from 'src/app/dtos/GetMoviesQueryParametersDto';
import { UpdateMovieDto } from 'src/app/dtos/UpdateMovieDto';
import { FirebaseAuthService } from '../firebaseAuth/firebase-auth.service';

@Injectable({
  providedIn: 'root'
})
export class MoviesService 
{
  private _httpClient:HttpClient;
  private _angularFireStorage:AngularFireStorage;
  private _firebaseAuth:FirebaseAuthService;
  private _headers:HttpHeaders;

  constructor(httpClient:HttpClient,
    angularFireStorage:AngularFireStorage,
    firebaseAuthService:FirebaseAuthService
    ) 
  {
    this._httpClient = httpClient;
    this._angularFireStorage = angularFireStorage;
    this._firebaseAuth = firebaseAuthService;
    this._headers = new HttpHeaders();
    if(this._firebaseAuth.getToken() != null)
    {
      this._headers = this._headers.append('Authorization','Bearer '+this._firebaseAuth.getToken()!);
    }
  }

  public getMovies(getMoviesQueryParametersDto:GetMoviesQueryParametersDto)
  {
    let params =  new HttpParams();
    params = params.append('ItemsPerPage',getMoviesQueryParametersDto.ItemsPerPage.toString());
    params = params.append('Page',getMoviesQueryParametersDto.Page.toString());
    if(getMoviesQueryParametersDto.Title != undefined && getMoviesQueryParametersDto.Title.length > 0)
    {
      params = params.append('Title',getMoviesQueryParametersDto.Title);
    }
    if(getMoviesQueryParametersDto.CategoriesIds != undefined && getMoviesQueryParametersDto.CategoriesIds.length > 0)
    {
      params = params.append('CategoriesIds',getMoviesQueryParametersDto.CategoriesIds.join(', '));
    }
    if(getMoviesQueryParametersDto.BeginAddedDate != undefined)
    {
      params = params.append("BeginAddedDate",getMoviesQueryParametersDto.BeginAddedDate.toString());
    }
    if(getMoviesQueryParametersDto.EndAddedDate != undefined)
    {
      params = params.append("EndAddedDate",getMoviesQueryParametersDto.EndAddedDate.toString())
    }
    
    return this._httpClient.get(environment.api+'/Movies/GetMovies',{params:params});
  }

  public addMovie(createMovieDto:CreateMovieDto,movieFile:any,moviePoster:any,moviePosterGif:any)
  {
    
    let formData = new FormData();
    formData.append('file',movieFile);
    formData.append('key',environment.vidGuardApiKey);
    let imageName:string = moviePoster.name;
    let imageNameWithoutExtension:string = imageName.split('.')[0];
    let gifName:string = moviePosterGif.name;
    let gifNameWithoutExtension = gifName.split('.')[0];
  
    //call asyncron, va fi triggeruit cand requestul este completat si vine un raspuns, non blocking
    return this._httpClient.get(environment.vidGuardUpload+environment.vidGuardApiKey)
    .pipe(
      switchMap(res => {
        let resJsonObj = JSON.parse(JSON.stringify(res));
        let url = resJsonObj.result.url;
        return this._httpClient.post(url,formData);
      }),
      switchMap(res => 
        {
          let resJson = JSON.parse(JSON.stringify(res));
          let id = resJson.result.HashID;
          createMovieDto.VidGuardId = id;
          let imageNameWithId = imageNameWithoutExtension + id;
          let gifNameWithId = gifNameWithoutExtension + "gif"+id;
          this._angularFireStorage.upload(imageNameWithId,moviePoster);
          this._angularFireStorage.upload(gifNameWithId,moviePosterGif);
          createMovieDto.PosterImageUrl = imageNameWithId;
          createMovieDto.PosterImageUrlGif = gifNameWithId;
          return this._httpClient.post(environment.api+'/Movies/CreateMovie',createMovieDto,{headers:this._headers});
        })
    )
  }

  public getMovieById(id:string)
  {
    return this._httpClient.get(environment.api+'/Movies/GetMovieById/'+id);
  }

  public getMovieByTitle(title:string)
  {
    return this._httpClient.get(environment.api+'/Movies/GetMovieByTitle/'+title);
  }

  public getMovieClone(id:string)
  {
    return this._httpClient.get(environment.vidGuardClone+environment.vidGuardApiKey+`&id=${id}`);
  }

  public getMoviesByTitle(title:string|undefined,itemsPerPage:number,page:number)
  {
    let params =  new HttpParams();
    params = params.append('ItemsPerPage',itemsPerPage.toString());
    params = params.append('Page',page.toString());
    if(title != undefined && title != '')
    {
      params = params.append('Title',title);
    }
    return this._httpClient.get(environment.api+'/Movies/GetMoviesByTitle',{params:params,headers:this._headers});
  }

  public updateMovieViews(id:string)
  {
    let body = 
    {
      Id:id
    }
    return this._httpClient.patch(environment.api+'/Movies/UpdateViewsCounter',body);
  }

  public deleteMovie(id:string,vidGuardId:string)
  {
      return this._httpClient.get(environment.vidGuardDelete+environment.vidGuardApiKey+'&id='+vidGuardId)
      .pipe(
        switchMap((res:any) => 
          {
            return this._httpClient.delete(environment.api+'/Movies/DeleteMovieById/'+id,{headers:this._headers})
          })
      )
  }

  public upvoteMovieById(movieId:string,userId:string)
  {
    let body = 
    {
      MovieId:movieId,
      userId:userId
    };
    return this._httpClient.post(environment.api+"/VotedMovies/VoteMovieByUserAndMovieId",body,{headers:this._headers});
  }
  public downvoteMovieById(movieId:string,userId:string)
  {
    let body = 
    {
      MovieId:movieId,
      userId:userId
    };
    return this._httpClient.post(environment.api+"/VotedMovies/DownVoteMovieByUserAndMovieId",body,{headers:this._headers});
  }

  public addMovieToFavorite(movieId:string,userId:string)
  {
    let body = 
    {
      UserId:userId,
      MovieId:movieId
    }
    return this._httpClient.post(environment.api+"/FavoriteMovies/AddMovieToFavoriteList",body,{headers:this._headers})
  }

  public updateMovieById(updateMovieDto:UpdateMovieDto,id:string)
  {
    let body = 
    {
      Title:updateMovieDto.title,
      Description:updateMovieDto.description,
      CategoriesIds:updateMovieDto.categoriesIds
    };
    return this._httpClient.patch(environment.api+'/Movies/UpdateMovieById/'+id,body,{headers:this._headers});
  }

  public getFavoriteMoviesByUserId(userId:string,itemsPerPage:number,page:number)
  {
    let params =  new HttpParams();
    params = params.append('ItemsPerPage',itemsPerPage.toString());
    params = params.append('Page',page.toString());
    return this._httpClient.get(environment.api+'/FavoriteMovies/GetFavoriteMoviesByUserId/'+userId,{headers:this._headers,params:params})
  }

  public unpinFromFavoriteMovieList(firebaseId:string,movieId:string)
  {
    let params = new HttpParams();
    params = params.append('FirebaseId',firebaseId.toString());
    params = params.append('MovieId',movieId.toString());
    return this._httpClient.delete(environment.api+'/FavoriteMovies/DeleteFavoriteMovieByUserIdAndMovieId',{headers:this._headers,params:params})
  }
}
