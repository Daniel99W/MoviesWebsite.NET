import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { FirebaseAuthService } from 'src/app/services/firebaseAuth/firebase-auth.service';
import { AddMovieComponent } from '../add-movie/add-movie.component';
import { MoviesComponent } from '../movies/movies.component';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent implements OnInit
 {
  private _firebaseAuthService:FirebaseAuthService;
  private _isLoggedIn:boolean;
  private _dialog:MatDialog;

  constructor(
    firebaseAuthService:FirebaseAuthService,
    dialog:MatDialog
    ) 
  { 
    this._firebaseAuthService = firebaseAuthService;
    this._isLoggedIn = false;
    this._firebaseAuthService.isAuthenticated()
    .subscribe(res => 
      {
        this._isLoggedIn = res;
      })
    this._dialog = dialog;
  }

  ngOnInit(): void 
  {

  }

  public openAddMovieDialog()
  {
    this._dialog.open(AddMovieComponent,{
      width:'55rem',
      height:'40rem'
    })
  }

  public openMoviesDialog()
  {
    this._dialog.open(MoviesComponent,{
      width:'70rem',
      height:'45rem'
    })
  }

  public isLoggedIn():boolean
  {
    return this._isLoggedIn;
  }

  public signOut()
  {
    this._firebaseAuthService.signOut();
  }



}
