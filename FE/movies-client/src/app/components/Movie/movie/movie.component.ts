import { Component, OnInit } from '@angular/core';
import { AngularFireStorage } from '@angular/fire/compat/storage';
import { ActivatedRoute, Router } from '@angular/router';
import { GetMovieDto } from 'src/app/dtos/GetMovieDto';
import { CategoryService } from 'src/app/services/categories/category.service';
import { MoviesService } from 'src/app/services/movies/movies.service';
import { DomSanitizer } from '@angular/platform-browser';
import { environment } from 'src/environments/environment';
import { Meta } from '@angular/platform-browser';
import { Utilities } from 'src/app/utilities/Utilities';
import { FirebaseAuthService } from 'src/app/services/firebaseAuth/firebase-auth.service';
import jwtDecode from 'jwt-decode';
import { Token } from '@angular/compiler';
import { UsersService } from 'src/app/services/users/users.service';
import { MatDialog } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import {Title} from '@angular/platform-browser';
import { TagDto } from 'src/app/dtos/TagDto';


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
  private _titleService:Title;
  private _meta:Meta;

  constructor(moviesService:MoviesService,
    categoriesService:CategoryService,
    activatedRoute:ActivatedRoute,
    firebaseStorage:AngularFireStorage,
    sanitizer:DomSanitizer,
    fireAuth:FirebaseAuthService,
    userService:UsersService,
    snackBar:MatSnackBar,
    title:Title,
    meta:Meta
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
    this._titleService = title;
    this._meta = meta;
  }

  ngOnInit(): void 
  {
   this._activatedRoute.params
   .subscribe(params => 
    {
      let title = params['title'];
      this._moviesService.getMovieByTitle(title)
      .subscribe((res:any) => 
        {
          this._movieGetDto = res;
          this._titleService.setTitle(this._movieGetDto.title);
          for(let i = 0;i<this._movieGetDto.tags.length; ++i)
          {
            let tag = this._movieGetDto.tags[i];
            this._meta.addTag({keywords:tag.name});
          }
        })
      this._moviesService.updateMovieViews(this.movie.id)
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

 public get categories()
 {
  return this._movieGetDto.categories;
 }

 public formatDate(date:Date)
 {
   return Utilities.formatDate(date);
 }

 public upvoteMovie(movieId:string)
 {
   if(this._isLoggedIn)
   {
      let firebaseId = this._fireAuth.getFirebaseUserIdFromToken();
      this._userService.getUserIdByFirebaseId(firebaseId!)
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




}
