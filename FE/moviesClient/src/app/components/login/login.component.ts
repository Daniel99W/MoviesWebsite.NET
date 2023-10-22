import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { signInDto } from 'src/app/dtos/SignInDto';
import { FirebaseAuthService } from 'src/app/services/firebaseAuth/firebase-auth.service';
import jwt_decode, { JwtPayload } from 'jwt-decode';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ErrMessages } from 'src/app/constants/ErrMessages';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit 
{
  private _firebaseAuthService:FirebaseAuthService;
  private _loginForm:FormGroup;
  private _router:Router;
  private _snackbar:MatSnackBar;

  constructor(
    firebaseAuthService:FirebaseAuthService,
    router:Router,
    snackbar:MatSnackBar
    ) 
  {
    this._firebaseAuthService = firebaseAuthService;
    this._loginForm = new FormGroup(
      {
        Email:new FormControl('',[Validators.required,Validators.email]),
        Password:new FormControl('',[Validators.required])
      }
    )
    this._router = router;
    this._snackbar = snackbar;
  }

  ngOnInit(): void 
  {
  }

  public get getSignInForm():FormGroup
  {
    return this._loginForm;
  }

  public Login():void 
  {
    let signIn = new signInDto();
    signIn.Email = this._loginForm.get('Email')?.value;
    signIn.Password = this._loginForm.get('Password')?.value;
    this._firebaseAuthService.signIn(signIn)
    .then(res => {
      this._router.navigate(['/']);
    })
    .catch(err => 
      {
        this._snackbar.open(ErrMessages.WrongCredentials);
      })
  }

}
