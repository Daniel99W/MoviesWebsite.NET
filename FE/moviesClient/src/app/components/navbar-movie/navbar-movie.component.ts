import { Component, Input, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { GetMovieDto } from 'src/app/dtos/GetMovieDto';

@Component({
  selector: 'app-navbar-movie',
  templateUrl: './navbar-movie.component.html',
  styleUrls: ['./navbar-movie.component.css']
})
export class NavbarMovieComponent implements OnInit 
{
  @Input() public _getMovieDto!:GetMovieDto;
  private _route:ActivatedRoute;

  constructor(route:ActivatedRoute) 
  {
    this._route = route;
  }

  ngOnInit(): void 
  {
   
  }



}
