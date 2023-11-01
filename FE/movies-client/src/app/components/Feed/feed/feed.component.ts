
import { Component, OnInit } from '@angular/core';
import { AngularFireStorage } from '@angular/fire/compat/storage';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { GetCategoryDto } from 'src/app/dtos/GetCategoryDto';
import { GetMovieDto } from 'src/app/dtos/GetMovieDto';
import { GetMoviesQueryParametersDto } from 'src/app/dtos/GetMoviesQueryParametersDto';
import { PaginatedResultDto } from 'src/app/dtos/PaginatedResultDto';
import { CategoryService } from 'src/app/services/categories/category.service';
import { MoviesService } from 'src/app/services/movies/movies.service';
import { Constants } from 'src/app/utilities/Constants';
import { Utilities } from 'src/app/utilities/Utilities';

@Component({
  selector: 'app-feed',
  templateUrl: './feed.component.html',
  styleUrls: ['./feed.component.css']
})
export class FeedComponent implements OnInit 
{
  private _movieService:MoviesService;
  private _breakpoint:any;
  private _getMoviesQueryParams:GetMoviesQueryParametersDto;
  private _movies:PaginatedResultDto<GetMovieDto>
  private _categoriesService:CategoryService;
  private _categories:GetCategoryDto[];
  private _firebaseStorage:AngularFireStorage;
  private _filterForm:FormGroup;
  private _router:Router;

  constructor(movieService:MoviesService,
    categoriesService:CategoryService,
    firebaseStorage:AngularFireStorage,
    router:Router
    ) 
  {
    this._movieService = movieService;
    this._getMoviesQueryParams = new GetMoviesQueryParametersDto();
    this._breakpoint = (window.innerWidth <= 480) ? 1 : 5;
    this._movies = new PaginatedResultDto<GetMovieDto>();
    this._categories = [];
    this._categoriesService = categoriesService;
    this._firebaseStorage = firebaseStorage;
    this._router = router;

    this._filterForm = new FormGroup(
      {
        Title:new FormControl('',[Validators.maxLength(Constants.inputSearchTitleMaxSize)]),
        Categories:new FormControl(),
        BeginAddedDate:new FormControl(),
        EndAddedDate:new FormControl(),
        MoviesPerPage:new FormControl()
      });
  }

  ngOnInit(): void 
  {
    this._movieService.getMovies(this._getMoviesQueryParams)
    .subscribe((res:any) => 
      {
       this._movies.TotalPages = res.totalPages;
       this._movies.Page = res.page;
       this._movies.Results = res.results;
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
    this._categoriesService.getCategories()
    .subscribe((res:any) => 
      {
        this._categories = res;
      })
    this._filterForm.get('Title')?.setValue(null);
    this._filterForm.get('Categories')?.setValue(null);
    this._filterForm.get('BeginAddedDate')?.setValue(null);
    this._filterForm.get('EndAddedDate')?.setValue(null);
    this._filterForm.get('MoviesPerPage')?.setValue(null);
    this._getMoviesQueryParams.Title = '';
    this._getMoviesQueryParams.CategoriesIds = [];
    this._getMoviesQueryParams.BeginAddedDate = undefined;
    this._getMoviesQueryParams.EndAddedDate = undefined;
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

  public fileChangeEvent($event:any,movie:GetMovieDto)
  {
    movie.movieImageURL = movie.movieImageURL == movie.posterImageUrl ? movie.posterImageUrlGif : movie.posterImageUrl;
  }

  public searchMovies(page:number = 1)
  {
    let title = this._filterForm.get('Title')?.value;
    let movieGenRe = this._filterForm.get('Categories')?.value;
    let beginAddedDate = this._filterForm.get('BeginAddedDate')?.value;
    let endAddedDate = this._filterForm.get('EndAddedDate')?.value;
    if(title != null)
    {
      this._getMoviesQueryParams.Title = title;
    }
    if(movieGenRe != null)
    {
      this._getMoviesQueryParams.CategoriesIds = movieGenRe;
    }
    if(beginAddedDate != null)
    {
      this._getMoviesQueryParams.BeginAddedDate = beginAddedDate;
    }
    if(endAddedDate != null)
    {
      this._getMoviesQueryParams.EndAddedDate = endAddedDate;
    }
    if(this._filterForm.get('MoviesPerPage')?.value != null)
    {
      this._getMoviesQueryParams.ItemsPerPage = this._filterForm.get('MoviesPerPage')?.value;
    }
    this._getMoviesQueryParams.Page = page;
    this.ngOnInit();
  }


  public get categories()
  {
    return this._categories;
  }

  public filterFormGroup():FormGroup
  {
    return this._filterForm;
  }

  public get breakpoint()
  {
    return this._breakpoint;
  }

  public get movies():GetMovieDto[]
  {
    return this._movies.Results;
  }

  public restrictChars(value:string)
  {
    return value.substring(0,27)+'...';
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









}
