import { Component, OnInit } from '@angular/core';
import { AngularFireStorage } from '@angular/fire/compat/storage';
import { ActivatedRoute, Router } from '@angular/router';
import { GetMovieDto } from 'src/app/dtos/GetMovieDto';
import { CategoryService } from 'src/app/services/categories/category.service';
import { MoviesService } from 'src/app/services/movies/movies.service';
import { DomSanitizer } from '@angular/platform-browser';
import { environment } from 'src/environments/environment';
import { Utilities } from 'src/app/utilities/Utilities';
import { FirebaseAuthService } from 'src/app/services/firebaseAuth/firebase-auth.service';
import jwtDecode from 'jwt-decode';
import { Token } from '@angular/compiler';
import { UsersService } from 'src/app/services/users/users.service';
import { MatDialog } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
  selector: 'app-movie',
  templateUrl: './movie.component.html',
  styleUrls: ['./movie.component.css']
})
export class MovieComponent implements OnInit 
{
  private _moviesService:MoviesService;
  private _categoriesService:CategoryService;
  private _movieGetDto!:GetMovieDto;
  private _firebaseStorage:AngularFireStorage;
  private _activatedRoute:ActivatedRoute;
  private _videoClone:any;
  private _sanitizer:DomSanitizer;
  private _fireAuth:FirebaseAuthService;
  private _snackBar:MatSnackBar;
  private _isLoggedIn:boolean;
  private _userService:UsersService;

  constructor(moviesService:MoviesService,
    categoriesService:CategoryService,
    activatedRoute:ActivatedRoute,
    firebaseStorage:AngularFireStorage,
    sanitizer:DomSanitizer,
    fireAuth:FirebaseAuthService,
    userService:UsersService,
    snackBar:MatSnackBar
    ) 
  {
    this._moviesService = moviesService;
    this._categoriesService = categoriesService;
    this._activatedRoute = activatedRoute;
    this._firebaseStorage = firebaseStorage;
    this._sanitizer = sanitizer;
    this._fireAuth = fireAuth;
    this._isLoggedIn = false;
    this._userService = userService;
    this._snackBar = snackBar;
  }

  ngOnInit(): void 
  {
   this._activatedRoute.params
   .subscribe(params => 
    {
      let id = params['Id'];
      this._moviesService.getMovieById(id)
      .subscribe((res:any) => 
        {
          this._movieGetDto = res;
        })
      this._moviesService.updateMovieViews(id)
      .subscribe((res:any)=>
      {
      })
      
    })
    this._fireAuth.isAuthenticated()
    .subscribe(res => 
      {
        this._isLoggedIn = res;
      })
  }


 public get movieURL():string
 {
  let url = environment.videoURL+this._movieGetDto.vidGuardId;
  return url;
 }

 public get movie()
 {
    return this._movieGetDto;
 }

 public formatDate(date:Date)
 {
   return Utilities.formatDate(date);
 }

 public upvoteMovie(movieId:string)
 {
   if(this._isLoggedIn)
   {
      let token = this._fireAuth.getToken();
      let tokenDecoded:any = jwtDecode(token!);
      let firebaseId = tokenDecoded.user_id;
      this._userService.getUserIdByFirebaseId(firebaseId)
      .subscribe((res:any) => 
        {
          let userId:string = res;
          this._moviesService.upvoteMovieById(movieId,userId)
          .subscribe(res => 
          {
            console.log(res);
            this._moviesService.getMovieById(movieId)
            .subscribe((res:any) => 
              {
                this._movieGetDto = res;
              })
          });
        })
   }
   else 
   {
    this._snackBar.open("U have to be logged In to like this video",'close',
    {
      duration:3000,
      verticalPosition:'top'
    });
   }
 }

 public downvoteMovie(movieId:string)
 {
  if(this._isLoggedIn)
   {
      let token = this._fireAuth.getToken();
      let tokenDecoded:any = jwtDecode(token!);
      let firebaseId = tokenDecoded.user_id;
      this._userService.getUserIdByFirebaseId(firebaseId)
      .subscribe((res:any) => 
        {
          let userId:string = res;
          this._moviesService.downvoteMovieById(movieId,userId)
          .subscribe(res => 
          {
            console.log(res);
            this._moviesService.getMovieById(movieId)
            .subscribe((res:any) => 
              {
                this._movieGetDto = res;
              })
          });
        })  
   }
   else 
   {
    this._snackBar.open("U have to be logged In to like this video",'close',
    {
      duration:3000,
      verticalPosition:'top'
    });
   }
 }

 public addToFavorite(movieId:string)
 {
  if(this._isLoggedIn)
  {
     let token = this._fireAuth.getToken();
     let tokenDecoded:any = jwtDecode(token!);
     let firebaseId = tokenDecoded.user_id;
     this._userService.getUserIdByFirebaseId(firebaseId)
     .subscribe((res:any) => 
       {
         let userId:string = res;
         this._moviesService.addMovieToFavorite(movieId,userId)
         .subscribe(res => 
         {
           console.log(res);
           this._moviesService.getMovieById(movieId)
           .subscribe((res:any) => 
             {
               this._movieGetDto = res;
             })
         });
       })  
      }
  {
    this._snackBar.open("U have to be logged In to like this video",'close',
    {
      duration:3000,
      verticalPosition:'top'
    });
  }
 }




}
