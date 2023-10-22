import { Component, OnInit } from '@angular/core';
import { AngularFireStorage } from '@angular/fire/compat/storage';
import { ActivatedRoute, Router } from '@angular/router';
import { GetMovieDto } from 'src/app/dtos/GetMovieDto';
import { CategoryService } from 'src/app/services/categories/category.service';
import { MoviesService } from 'src/app/services/movies/movies.service';
import { DomSanitizer } from '@angular/platform-browser';
import { environment } from 'src/environments/environment';

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

  constructor(moviesService:MoviesService,
    categoriesService:CategoryService,
    activatedRoute:ActivatedRoute,
    firebaseStorage:AngularFireStorage,
    sanitizer:DomSanitizer
    ) 
  {
    this._moviesService = moviesService;
    this._categoriesService = categoriesService;
    this._activatedRoute = activatedRoute;
    this._firebaseStorage = firebaseStorage;
    this._sanitizer = sanitizer;
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
          console.log(this._movieGetDto)
        })
      this._moviesService.updateMovieViews(id)
      .subscribe((res:any)=>
      {
        console.log(res);
      })
      
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


}
