import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ErrMessages } from 'src/app/constants/ErrMessages';
import { Messages } from 'src/app/constants/Messages';
import { CreateMovieDto } from 'src/app/dtos/CreateMovieDto';
import { GetCategoryDto } from 'src/app/dtos/GetCategoryDto';
import { TagDto } from 'src/app/dtos/TagDto';
import { CategoryService } from 'src/app/services/categories/category.service';
import { MoviesService } from 'src/app/services/movies/movies.service';
import { Constants } from 'src/app/utilities/Constants';


@Component({
  selector: 'app-add-movie',
  templateUrl: './add-movie.component.html',
  styleUrls: ['./add-movie.component.css']
})
export class AddMovieComponent implements OnInit 
{
  private _movieService:MoviesService;
  private _dialogRef:MatDialogRef<AddMovieComponent>
  private _createMovieForm:FormGroup;
  private _movieFile:any;
  private _categoriesService:CategoryService;
  private _moviePoster:any;
  private _snackBar:MatSnackBar;
  private _isFileUploaded:boolean;
  private _categories!:GetCategoryDto[];
  private _tags:Array<TagDto>;
  private _tag:string;

  constructor(
    movieService:MoviesService,
    dialogRef:MatDialogRef<AddMovieComponent>,
    snackBar:MatSnackBar,
    categoriesService:CategoryService
    ) 
  { 
    this._movieService = movieService;
    this._dialogRef = dialogRef;
    this._snackBar = snackBar;
    this._isFileUploaded = true;
    this._createMovieForm = new FormGroup(
      {
        title:new FormControl('',[Validators.required,Validators.maxLength(Constants.inputTitleMaxSize)]),
        description:new FormControl('',[Validators.required,Validators.maxLength(Constants.inputDescriptionMaxSize)]),
        addedDate:new FormControl('',[Validators.required]),
        categories:new FormControl('',[Validators.required])
      })
    this._categoriesService = categoriesService;
    this._tags = new Array<TagDto>();
    this._tag = '';
  }

  ngOnInit(): void 
  {
    this._categoriesService.getCategories()
    .subscribe((res:any) => 
      {
        this._categories =  res;
      });
    
  }

  public get tag()
  {
    return this._tag;
  }

  public set tag(value:string)
  {
    this._tag = value;
  }

  public addTag()
  {
    let checkIfTagExist:any;
    checkIfTagExist = this._tags.find(t => t.name == this.tag);
    if(checkIfTagExist == undefined)
    {
      this._tags.push(new TagDto(this.tag));
    }
    this.tag = '';
    console.log(this._tags);
  }

  public get tags()
  {
    return this._tags;
  }

  public getCreateMovieForm():FormGroup
  {
    return this._createMovieForm;
  }

  public getCurrentYear()
  {
    return new Date().getFullYear();
  }

  public onFileSelected($event:any)
  {
    this._movieFile = $event.target.files[0];
  }

  public onFilePosterSelected($event:any)
  {
    this._moviePoster = $event.target.files[0];
  }

  public isFileUploaded():boolean
  {
    return this._isFileUploaded;
  }

  public get categories()
  {
    return this._categories;
  }


  public addMovieToVidGuard()
  {
    this._isFileUploaded = false;
    let createMovieDto = new CreateMovieDto();
    createMovieDto.Title = this._createMovieForm.get('title')?.value;
    createMovieDto.Description = this._createMovieForm.get('description')?.value;
    createMovieDto.AddedDate = this._createMovieForm.get('addedDate')?.value;
    createMovieDto.CategoriesIds = this._createMovieForm.get('categories')?.value;
    createMovieDto.Tags = this._tags;
    this._movieService
    .addMovie(createMovieDto,this._movieFile,this._moviePoster)
    .subscribe(res => 
      {
        this._isFileUploaded = true;
        this._snackBar.open(Messages.FileUploaded,'close',
        {
          duration:3000,
          horizontalPosition:'center',
          verticalPosition:'top'
        })
        console.log(res);
      },err =>
      {
        console.log(err);
        this._isFileUploaded = true;
      }
      )
  }

  public close()
  {
    this._dialogRef.close();
  }









}
