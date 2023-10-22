import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class UsersService 
{
  private _httpClient:HttpClient;

  constructor(httpClient:HttpClient) 
  {
    this._httpClient = httpClient;
   }

   public getUserIdByFirebaseId(firebaseId:string)
   {
    return this._httpClient.get(environment.api+'/Users/GetUserIdByFirebaseId/'+firebaseId);
   }
}
