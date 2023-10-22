import { Component, Input, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { map } from 'rxjs';
import { GetMovieDto } from 'src/app/dtos/GetMovieDto';
import { Utilities } from 'src/app/utilities/Utilities';

@Component({
  selector: 'app-movie-information',
  templateUrl: './movie-information.component.html',
  styleUrls: ['./movie-information.component.css']
})
export class MovieInformationComponent implements OnInit 
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
        this._getMovieDto = res;
      })
  }

  public get movie()
  {
    return this._getMovieDto;
  }

  public formatDate(date:Date)
  {
    return Utilities.formatDate(date);
  }

}
