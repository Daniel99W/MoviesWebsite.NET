import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ErrMessages } from 'src/app/constants/ErrMessages';
import { Messages } from 'src/app/constants/Messages';
import { CreateMovieDto } from 'src/app/dtos/CreateMovieDto';
import { GetCategoryDto } from 'src/app/dtos/GetCategoryDto';
import { Languages } from 'src/app/enums/Languages';
import { CategoryService } from 'src/app/services/categories/category.service';
import { MoviesService } from 'src/app/services/movies/movies.service';


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
  private _languages!:Languages;
  private _selectedLanguage:any;
  private _movieFile:any;
  private _categoriesService:CategoryService;
  private _moviePoster:any;
  private _snackBar:MatSnackBar;
  private _isFileUploaded:boolean;
  private _selectedSubtitle:any;
  private _categories!:GetCategoryDto[];

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
        title:new FormControl('',[Validators.required]),
        description:new FormControl('',[Validators.required]),
        addedDate:new FormControl('',[Validators.required]),
        categories:new FormControl('')
      })
    this._categoriesService = categoriesService;
  }

  ngOnInit(): void 
  {
    this._categoriesService.getCategories()
    .subscribe((res:any) => 
      {
        this._categories =  res;
        console.log(this._categories);
      });
    
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
