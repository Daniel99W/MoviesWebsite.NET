import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { FirebaseAuthService } from 'src/app/services/firebaseAuth/firebase-auth.service';
import { AddMovieComponent } from '../add-movie/add-movie.component';
import { MoviesComponent } from '../movies/movies.component';
import jwtDecode from 'jwt-decode';

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

  public openAddMovieDialog(role:string)
  {
    if(this._isLoggedIn && this.isAuthorized(role))
    {
      this._dialog.open(AddMovieComponent,{
        width:'55rem',
        height:'40rem'
      })
    }
  }

  public openMoviesDialog(role:string)
  {
    if(this._isLoggedIn && this.isAuthorized(role))
    {
      this._dialog.open(MoviesComponent,
        {
        width:'70rem',
        height:'45rem'
      })
    }
  }

  public isLoggedIn():boolean
  {
    return this._isLoggedIn;
  }

  public isAuthorized(role:string)
  {
    let token = this._firebaseAuthService.getToken();
    if(token == null)
      return false;
    let tokenDecoded:any = jwtDecode(token!);
    if(tokenDecoded["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"] == role)
    {
      return true;
    }
    return false;
  }

  public signOut()
  {
    this._firebaseAuthService.signOut();
  }



}
