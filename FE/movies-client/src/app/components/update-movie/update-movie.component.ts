import { Component, OnInit,Inject } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { GetCategoryDto } from 'src/app/dtos/GetCategoryDto';
import { GetMovieDto } from 'src/app/dtos/GetMovieDto';
import { UpdateMovieDto } from 'src/app/dtos/UpdateMovieDto';
import { CategoryService } from 'src/app/services/categories/category.service';
import { MoviesService } from 'src/app/services/movies/movies.service';

@Component({
  selector: 'app-update-movie',
  templateUrl: './update-movie.component.html',
  styleUrls: ['./update-movie.component.css']
})
export class UpdateMovieComponent implements OnInit 
{
  private _movieService:MoviesService;
  private _updateMovieForm:FormGroup;
  private _categoriesService:CategoryService;
  private _categories!:GetCategoryDto[];
  private _getMovieDto:GetMovieDto;
  private _dialogRef:MatDialogRef<UpdateMovieComponent>
  private _data:any;

  constructor(
    @Inject(MAT_DIALOG_DATA) data:{movieId:string},
    movieService:MoviesService,
    categoriesService:CategoryService,
    dialogRef:MatDialogRef<UpdateMovieComponent>
    ) 
  { 
    this._movieService = movieService;
    this._updateMovieForm = new FormGroup(
      {
        title:new FormControl('',[Validators.required]),
        description:new FormControl('',[Validators.required]),
        addedDate:new FormControl('',[Validators.required]),
        categories:new FormControl('')
      })
    this._categoriesService = categoriesService;
    this._categories = new Array<GetCategoryDto>();
    this._getMovieDto = new GetMovieDto();
    this._data = data;
    this._dialogRef = dialogRef;
  }

  ngOnInit(): void 
  {
    this._categoriesService.getCategories()
    .subscribe((res:any)=>
    {
      this._categories = res;
    })
    this._movieService.getMovieById(this._data.movieId)
    .subscribe((res:any) => 
      {
        this._getMovieDto = res;
        this._updateMovieForm.setValue({
          title:this._getMovieDto.title,
          categories:this.categories,
          description:this._getMovieDto.description,
          addedDate:this._getMovieDto.addedDate
        })
        console.log(this._getMovieDto);
      })

  }

  public getUpdateMovieForm()
  {
    return this._updateMovieForm;
  }

  public get categories()
  {
    return this._categories;
  }

  public close()
  {
    this._dialogRef.close();
  }

  public updateMovie()
  {
    let updateMovieDto = new UpdateMovieDto();
    updateMovieDto.title = this._updateMovieForm.get('title')?.value;
    updateMovieDto.description = this._updateMovieForm.get('description')?.value;
    updateMovieDto.categoriesIds = this._updateMovieForm.get('categories')?.value;
    this._movieService.updateMovieById(updateMovieDto,this._data.movieId)
    .subscribe(res => 
      {
        console.log(res)
      })
  }

}
