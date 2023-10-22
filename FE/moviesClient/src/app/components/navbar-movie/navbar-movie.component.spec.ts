import { ComponentFixture, TestBed } from '@angular/core/testing';

import { NavbarMovieComponent } from './navbar-movie.component';

describe('NavbarMovieComponent', () => {
  let component: NavbarMovieComponent;
  let fixture: ComponentFixture<NavbarMovieComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ NavbarMovieComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(NavbarMovieComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
