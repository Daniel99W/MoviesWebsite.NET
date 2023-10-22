import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { FeedComponent } from './components/Feed/feed/feed.component';
import { EpisodeComponent } from './components/Episode/episode/episode.component';
import { MovieComponent } from './components/Movie/movie/movie.component';
import { environment } from 'src/environments/environment';
import { LoginComponent } from './components/login/login.component';
import { RegisterComponent } from './components/register/register.component';
import {MatFormFieldModule} from '@angular/material/form-field';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations'; 
import { MatInputModule } from "@angular/material/input";
import {MatButtonModule} from '@angular/material/button';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatFormFieldControl } from '@angular/material/form-field';
import { NavbarComponent } from './components/navbar/navbar.component';
import {MatToolbarModule} from '@angular/material/toolbar'; 
import { AuthGuard } from './services/AuthGuard';
import { AddMovieComponent } from './components/add-movie/add-movie.component';
import {MatSnackBarModule} from '@angular/material/snack-bar';
import {MatDialogModule} from '@angular/material/dialog';
import {MatSelectModule} from '@angular/material/select';
import { MatIconModule } from "@angular/material/icon";
import {MatProgressBarModule} from '@angular/material/progress-bar';
import { AngularFireStorageModule } from '@angular/fire/compat/storage';
import { AngularFireModule } from '@angular/fire/compat';
import {MatGridListModule} from '@angular/material/grid-list'
import {MatCardModule} from '@angular/material/card';
import {MatListModule} from '@angular/material/list';
import { SafePipe } from './pipes/SafePipe';

import { MoviesComponent } from './components/movies/movies.component';
import {MatMenuModule} from '@angular/material/menu';
import { UserProfileComponent } from './user-profile/user-profile.component';
import { NotificationsComponent } from './components/notifications/notifications.component';
import {MatTableModule} from '@angular/material/table';



@NgModule({
  declarations: [
    AppComponent,
    FeedComponent,
    EpisodeComponent,
    MovieComponent,
    LoginComponent,
    RegisterComponent,
    NavbarComponent,
    AddMovieComponent,
    SafePipe,
    MoviesComponent,
    UserProfileComponent,
    NotificationsComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    MatFormFieldModule,
    MatToolbarModule,
    MatButtonModule,
    MatInputModule,
    AngularFireModule.initializeApp(environment.firebase),
    BrowserAnimationsModule,
    HttpClientModule,
    MatDialogModule,
    MatSnackBarModule,
    MatSelectModule,
    MatProgressBarModule,
    MatIconModule,
    FormsModule,
    MatFormFieldModule,
    MatListModule,
    ReactiveFormsModule,
    AngularFireStorageModule,
    MatGridListModule,
    MatCardModule,
    MatMenuModule,
    MatTableModule
  ],
  providers: [AuthGuard],
  bootstrap: [AppComponent]
})
export class AppModule { }
