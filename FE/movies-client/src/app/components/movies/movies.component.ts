import { Component, OnInit } from '@angular/core';
import { GetMovieDto } from 'src/app/dtos/GetMovieDto';
import { PaginatedResultDto } from 'src/app/dtos/PaginatedResultDto';
import { MoviesService } from 'src/app/services/movies/movies.service';
import { Constants } from 'src/app/utilities/Constants';
import { Utilities } from 'src/app/utilities/Utilities';

@Component({
  selector: 'app-movies',
  templateUrl: './movies.component.html',
  styleUrls: ['./movies.component.css']
})
export class MoviesComponent implements OnInit 
{
  private _moviesService:MoviesService;
  private _movies:PaginatedResultDto<GetMovieDto>;
  private _displayedColumns:string[]
  private _itemsPerPage:number;
  private _title:string;


  constructor(moviesService:MoviesService) 
  {
    this._moviesService = moviesService;
    this._movies = new PaginatedResultDto<GetMovieDto>();
    this._displayedColumns = ['Title','Views','AddedDate','Update','Delete'];
    this._itemsPerPage = 10;
    this._title = '';
  }

  ngOnInit(): void 
  {
    this._moviesService.getMoviesByTitle(this._title,this.itemsPerPage,1)
    .subscribe((res:any) => 
      {
      this._movies.Page = res.page;
      this._movies.Results = res.results;
      this._movies.TotalPages = res.totalPages;
      })
  }

  public searchMovieByTitle(page:number)
  {
    this._moviesService
    .getMoviesByTitle(this._title,this._itemsPerPage,page)
    .subscribe((res:any)=>
    {
      this._movies.Page = res.Page;
      this._movies.Results = res.Results;
      this._movies.TotalPages = res.TotalPages;
    })
  }


  public get title()
  {
    return this._title;
  }

  public set title(value:string)
  {
    this._title = value;
  }

  public get itemsPerPage()
  {
    return this._itemsPerPage;
  }

  public setItemsPerPage($event:any)
  {
    this._itemsPerPage = $event.target.value;
  }

  public get movies()
  {
    return this._movies.Results;
  }

  public titleMaxSize(title:string)
  {
    if(title.length > Constants.titleMaxSize)
    {
      return Utilities.wordMaxSize(title,Constants.titleMaxSize);
    }
    return title;
  }

  public formatDate(date:Date)
  {
    return Utilities.formatDate(date);
  }

  public get displayedColumns()
  {
    return this._displayedColumns;
  }

  public get getPaginationData():PaginatedResultDto<GetMovieDto>
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

  public deleteMovie(id:string,vidGuardId:string)
  {
    this._moviesService
    .deleteMovie(id,vidGuardId)
    .subscribe(res => 
      {
        this.ngOnInit();
      })
  }

  


}
