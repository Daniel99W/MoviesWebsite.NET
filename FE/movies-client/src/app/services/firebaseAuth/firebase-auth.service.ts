import { Injectable } from '@angular/core';
import { AngularFireAuth } from '@angular/fire/compat/auth';
import { Observable, map } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { SignUpUser } from 'src/app/dtos/SignUpDto';
import { environment } from 'src/environments/environment';
import { signInDto } from 'src/app/dtos/SignInDto';
import jwtDecode from 'jwt-decode';

@Injectable({
  providedIn: 'root'
})
export class FirebaseAuthService 
{
  private _userData: firebase.default.User|null;
  private _authAngularFire:AngularFireAuth;
  private _httpClient:HttpClient;

  constructor(angularFireAuth:AngularFireAuth,httpClient:HttpClient) 
  { 
    this._authAngularFire = angularFireAuth;
    this._userData = null;
    this._authAngularFire
    .user
    .subscribe(res => 
      {
        this._userData = res;
      })
    this._httpClient = httpClient;
  }

  public signIn(signIn:signInDto)
  {
    return this._authAngularFire
    .signInWithEmailAndPassword(signIn.Email,signIn.Password)
    .then( async res => 
      {
        let token = (await res.user?.getIdTokenResult())!.token;
        localStorage.setItem("accessToken",token);
      })
  }

  public signUp(signUp:SignUpUser):Observable<any>
  {
    return this._httpClient.post(environment.api+"/Users/CreateUser",
    {
      Email:signUp.Email,
      Password:signUp.Password,
      Name:signUp.UserName
    });
  }

  public signOut()
  {
    this._authAngularFire
    .signOut()
    .then(res => 
      {
        localStorage.removeItem('accessToken');
      })
  }

  public getFirebaseUserIdFromToken():string|null
  {
    let token = this.getToken();
    if(token == null)
    {
      return null;
    }
    let tokenDecoded:any = jwtDecode(token!);
    let firebaseId = tokenDecoded.user_id;
    return firebaseId;
  }

  public isAuthenticated():Observable<boolean>
  {
    return this._authAngularFire
    .user
    .pipe(
      map(res => 
        {
          if(res == null)
          {
            return false;
          }
          return true;
        })
    )
  }

  public getToken()
  {
    return localStorage.getItem("accessToken");
  }


}
