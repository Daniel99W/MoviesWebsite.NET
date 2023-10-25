import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { SignUpUser } from 'src/app/dtos/SignUpDto';
import { FirebaseAuthService } from 'src/app/services/firebaseAuth/firebase-auth.service';
import { Constants } from 'src/app/utilities/Constants';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit 
{
  private _firebaseAuthService:FirebaseAuthService;
  private _registerForm:FormGroup;
  private _router:Router;

  constructor(
    firebaseAuthService:FirebaseAuthService,
    router:Router
    ) 
  {
    this._firebaseAuthService = firebaseAuthService;
    this._registerForm = new FormGroup({
      Email:new FormControl('',[Validators.required,Validators.email,Validators.maxLength(Constants.inputEmailMaxSize)]),
      Password:new FormControl('',[Validators.required,Validators.maxLength(Constants.inputPasswordMaxSize)]),
      Name:new FormControl('',[Validators.required,Validators.maxLength(Constants.inputNameMaxSize)])
    })
    this._router = router;
  }

  ngOnInit(): void 
  {

  }

  public get signUpForm():FormGroup
  {
    return this._registerForm;
  }

  public signUp()
  {
    let signUpDto = new SignUpUser();
    signUpDto.Email = this._registerForm.get('Email')?.value;
    signUpDto.Password = this._registerForm.get('Password')?.value;
    signUpDto.UserName = this._registerForm.get('Name')?.value;
    this._firebaseAuthService
    .signUp(signUpDto)
    .subscribe(res => 
      {
        this._router.navigate(['/']);
      })
  }

}
