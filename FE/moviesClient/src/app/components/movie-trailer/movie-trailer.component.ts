import { Component, Input, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { map } from 'rxjs';
import { GetMovieDto } from 'src/app/dtos/GetMovieDto';

@Component({
  selector: 'app-movie-trailer',
  templateUrl: './movie-trailer.component.html',
  styleUrls: ['./movie-trailer.component.css']
})
export class MovieTrailerComponent implements OnInit
 {
  @Input() public _getMovieDto!:GetMovieDto;
  private _route:ActivatedRoute;

  constructor(route:ActivatedRoute) 
  {
    this._route = route;
  }

  ngOnInit(): void 
  {
    this._route.paramMap
    .pipe(map(() => window.history.state))
    .subscribe(res => 
      {
        console.log(res);
      })
  }

  public get movie()
  {
    return this._getMovieDto;
  }

}
