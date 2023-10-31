import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { GetMovieDto } from 'src/app/dtos/GetMovieDto';
import { GetMovieFeeddDto } from 'src/app/dtos/GetMovieFeedDto';
import { FirebaseAuthService } from 'src/app/services/firebaseAuth/firebase-auth.service';
import { MoviesService } from 'src/app/services/movies/movies.service';

@Component({
  selector: 'app-favorite-list',
  templateUrl: './favorite-list.component.html',
  styleUrls: ['./favorite-list.component.css']
})
export class FavoriteListComponent implements OnInit 
{
  private _movies:Array<GetMovieFeeddDto>;
  private _isLoggedIn:boolean;
  private _moviesService:MoviesService;
  private _authService:FirebaseAuthService;

  constructor(moviesService:MoviesService,
    activatedRoute:ActivatedRoute,
    authService:FirebaseAuthService) 
  { 
    this._moviesService = moviesService;
    this._movies = new Array<GetMovieFeeddDto>();
    this._isLoggedIn = false;
    this._authService = authService;
  }

  ngOnInit(): void 
  {
    let firebaseUserId:any = this._authService.getFirebaseUserIdFromToken();
    console.log(firebaseUserId);
    this._moviesService.getFavoriteMoviesByUserId(firebaseUserId,5,1)
    .subscribe(res => 
    {
      console.log(res);
    })
      
  }

  public get movies()
  {
    return this._movies;
  }

  public getFavoriteMoviesByUserId()
  {
    
  }



}
