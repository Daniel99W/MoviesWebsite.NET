import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class CategoryService 
{
  private _httpClient:HttpClient;

  constructor(httpClient:HttpClient) 
  {
    this._httpClient = httpClient;
  }

  public getCategories()
  {
    return this._httpClient.get(environment.api+'/Categories/GetCategories');
  }
}
