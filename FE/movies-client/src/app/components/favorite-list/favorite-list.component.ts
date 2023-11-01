import { Component, OnInit } from '@angular/core';
import { AngularFireStorage } from '@angular/fire/compat/storage';
import { ActivatedRoute, Route, Router } from '@angular/router';
import { GetCategoryDto } from 'src/app/dtos/GetCategoryDto';
import { GetMovieDto } from 'src/app/dtos/GetMovieDto';
import { GetMovieFeedDto } from 'src/app/dtos/GetMovieFeedDto';
import { PaginatedResultDto } from 'src/app/dtos/PaginatedResultDto';
import { CategoryService } from 'src/app/services/categories/category.service';
import { FirebaseAuthService } from 'src/app/services/firebaseAuth/firebase-auth.service';
import { MoviesService } from 'src/app/services/movies/movies.service';
import { Utilities } from 'src/app/utilities/Utilities';

@Component({
  selector: 'app-favorite-list',
  templateUrl: './favorite-list.component.html',
  styleUrls: ['./favorite-list.component.css']
})
export class FavoriteListComponent implements OnInit 
{
  private _isLoggedIn:boolean;
  private _moviesService:MoviesService;
  private _authService:FirebaseAuthService;
  private _breakpoint:any;
  private _firebaseStorage:AngularFireStorage
  private _router:Router;
  private _movies:PaginatedResultDto<GetMovieFeedDto>
  public page:number;
  public itemsPerPage:number;

  constructor(moviesService:MoviesService,
    activatedRoute:ActivatedRoute,
    categoriesService:CategoryService,
    router:Router,
    firebaseAngularStorage:AngularFireStorage,
    authService:FirebaseAuthService) 
  { 
    this._moviesService = moviesService;
    this._isLoggedIn = false;
    this._authService = authService;
    this._breakpoint = (window.innerWidth <= 480) ? 1 : 5;
    this._router = router;
    this._firebaseStorage = firebaseAngularStorage;
    this._movies = new PaginatedResultDto<GetMovieFeedDto>();
    this.page = 1;
    this.itemsPerPage = 5;
  }

  ngOnInit(): void 
  {
    let firebaseUserId:any = this._authService.getFirebaseUserIdFromToken();
    console.log(this.itemsPerPage);
    this._moviesService.getFavoriteMoviesByUserId(firebaseUserId,this.itemsPerPage,this.page)
    .subscribe((res:any) => 
    {
      this._movies.TotalPages = res.totalPages;
       this._movies.Page = res.page;
       this._movies.Results = res.results;
       console.log(this._movies);
        this._movies.Results
        .forEach(movie => 
        {
          this._firebaseStorage
          .storage
          .ref()
          .child(movie.posterImageUrl)
          .getDownloadURL()
          .then(res => 
            {
              movie.posterImageUrl = res;
              this._firebaseStorage
              .storage
              .ref(movie.posterImageUrlGif)
              .getDownloadURL()
              .then(res => 
                {
                  movie.posterImageUrlGif = res;
                  movie.movieImageURL =  movie.posterImageUrl;
                })
            });
        })
    })
      
  }


  onResize(event:any) 
  {
    this._breakpoint = (event.target.innerWidth <= 480) ? 1 : 5;
    if(event.target.innerWidth <= 480)
    {
      this._breakpoint = 1;
    }
    else if((event.target.innerWidth >= 480) && (event.target.innerWidth <= 980))
    {
      this._breakpoint = 4;
    }
    else 
    {
      this._breakpoint = 5;
    }
  }

  public get movies()
  {
    return this._movies.Results;
  }

  public fileChangeEvent($event:any,movie:GetMovieFeedDto)
  {
    movie.movieImageURL = movie.movieImageURL == movie.posterImageUrl ? movie.posterImageUrlGif : movie.posterImageUrl;
  }



  public getFavoriteMoviesByUserId(page:number)
  {
    this.page = page;
    this.ngOnInit();
  }

  public unpinFromFavoriteList(movieId:string)
  {
    let firebaseId = this._authService.getFirebaseUserIdFromToken();
    this._moviesService.unpinFromFavoriteMovieList(firebaseId!,movieId)
    .subscribe(res => 
      {
        this.ngOnInit();
      })
  }

  public getMovie(id:string,title:string)
  {
    title = title.split(' ').join('-');
    this._router.navigate(['movie',id,title]);
  }

  public formatDate(date:Date)
  {
    return Utilities.formatDate(date);
  }

  public get breakpoint()
  {
    return this._breakpoint;
  }

  
  public restrictChars(value:string)
  {
    return value.substring(0,30);
  }

  public get getPaginationData():PaginatedResultDto<GetMovieFeedDto>
  {
    return this._movies;
  }

  public counter(i:number):Array<number>
  {
    let arr = new Array<number>();
    for(let j = 1;j<=i;++j)
    {
      arr.push(j);
    }
    return arr;
  }

  public getPrevPage()
  {
    if(this._movies.Page - 1 <= 0)
    {
      return null;
    }
    return this._movies.Page;
  }

  public getNextPage()
  {
    if(this._movies.TotalPages < (+this._movies.Page + +1))
    {
      return null;
    }
    return +this._movies.Page + +1;
  }




}
