import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './components/login/login.component';
import { RegisterComponent } from './components/register/register.component';
import { FeedComponent } from './components/Feed/feed/feed.component';
import { MovieComponent } from './components/Movie/movie/movie.component';
import { AuthGuard } from './services/AuthGuard';
import { AddMovieComponent } from './components/add-movie/add-movie.component';
import { FavoriteListComponent } from './components/favorite-list/favorite-list.component';

const routes: Routes = 
[
  {
    path:'login',
    component:LoginComponent,
  },
  {
    path:'signUp',
    component:RegisterComponent
  },
  {
    path:'feed',
    component:FeedComponent
  },
  {
    path:'favoriteMovies',
    component:FavoriteListComponent,
    canActivate:[AuthGuard],
    data:
    {
      roles:['ADMIN','USER']
    }
  },
  {
    path:'addMovie',
    component:AddMovieComponent
  },
  {
    path:'movie/:id/:title',
    component:MovieComponent
  },
  {
    path:'',redirectTo:'feed',pathMatch:'full'
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
