import { Component, Input, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { map } from 'rxjs';
import { GetMovieDto } from 'src/app/dtos/GetMovieDto';
import { MoviesService } from 'src/app/services/movies/movies.service';

@Component({
  selector: 'app-movie-description',
  templateUrl: './movie-description.component.html',
  styleUrls: ['./movie-description.component.css']
})
export class MovieDescriptionComponent implements OnInit 
{
  private _getMovieDto!:GetMovieDto;
  private _route:ActivatedRoute;
  private _moviesService:MoviesService;

  constructor(route:ActivatedRoute,moviesService:MoviesService) 
  { 
    this._route = route;
    this._moviesService = moviesService;
  }

  ngOnInit(): void
  {
    this._route.parent?.params.subscribe(res => 
      {
        let Id = res['Id'];
        this._moviesService.getMovieById(Id)
        .subscribe((res:any) => 
          {
            this._getMovieDto = res;
          })
      })
  }

  public get movie()
  {
    return this._getMovieDto;
  }




}
