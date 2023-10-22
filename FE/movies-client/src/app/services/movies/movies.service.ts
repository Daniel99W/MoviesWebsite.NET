import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { CreateMovieDto } from 'src/app/dtos/CreateMovieDto';
import { environment } from 'src/environments/environment';
import { switchMap } from 'rxjs';
import { env } from 'process';
import { AngularFireStorage } from '@angular/fire/compat/storage';
import { GetMoviesQueryParametersDto } from 'src/app/dtos/GetMoviesQueryParametersDto';

@Injectable({
  providedIn: 'root'
})
export class MoviesService 
{
  private _httpClient:HttpClient;
  private _angularFireStorage:AngularFireStorage;

  constructor(httpClient:HttpClient,
    angularFireStorage:AngularFireStorage
    ) 
  {
    this._httpClient = httpClient;
    this._angularFireStorage = angularFireStorage;
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
    console.log(getMoviesQueryParametersDto.EndAddedDate);
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

  public addMovie(createMovieDto:CreateMovieDto,movieFile:any,moviePoster:any)
  {
    
    let formData = new FormData();
    formData.append('file',movieFile);
    formData.append('key',environment.vidGuardApiKey);
    let imageName:string = moviePoster.name;
    let imageNameWithoutExtension:string = imageName.split('.')[0];
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
          this._angularFireStorage.upload(imageNameWithId,moviePoster);
          createMovieDto.PosterImageUrl = imageNameWithId;
          return this._httpClient.post(environment.api+'/Movies/CreateMovie',createMovieDto);
        })
    )
  }

  public getMovieById(id:string)
  {
    return this._httpClient.get(environment.api+'/Movies/GetMovieById/'+id);
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
    return this._httpClient.get(environment.api+'/Movies/GetMoviesByTitle',{params:params});
  }

  public updateMovieViews(id:string)
  {
    let body = 
    {
      Id:id
    }
    return this._httpClient.patch(environment.api+'/Movies/UpdateViewsCounter',body);
  }

  public deleteMovie(id:string)
  {
    return this._httpClient.delete(environment.api+'/Movies/DeleteMovieById/'+id);
  }
}
