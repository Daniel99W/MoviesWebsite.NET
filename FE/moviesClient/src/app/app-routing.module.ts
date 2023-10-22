import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './components/login/login.component';
import { RegisterComponent } from './components/register/register.component';
import { FeedComponent } from './components/Feed/feed/feed.component';
import { MovieComponent } from './components/Movie/movie/movie.component';
import { AuthGuard } from './services/AuthGuard';
import { AddMovieComponent } from './components/add-movie/add-movie.component';
import { MovieDescriptionComponent } from './components/movie-description/movie-description.component';
import { MovieTrailerComponent } from './components/movie-trailer/movie-trailer.component';
import { MovieInformationComponent } from './components/movie-information/movie-information.component';
import { MoviesComponent } from './components/movies/movies.component';

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
    path:'addMovie',
    component:AddMovieComponent
  },
  {
    path:'movie/:Id',
    component:MovieComponent,
    children:
    [
      {path:'description',component:MovieDescriptionComponent},
      {path:'trailer',component:MovieTrailerComponent},
      {path:'infos',component:MovieInformationComponent},
      {path:'',redirectTo:'description',pathMatch:'full'}
    ]
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
