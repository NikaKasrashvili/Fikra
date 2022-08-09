import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, map } from 'rxjs';
import { environment } from 'src/environments/environment';
import { LoginModel } from '../models/login.model';
import { User } from '../models/user.model';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private currentUserSubject$: BehaviorSubject<any>;

  constructor(private http: HttpClient) {
    this.currentUserSubject$ = new BehaviorSubject(
      JSON.parse(localStorage.getItem('creds') || '{}' || null)
    );
  }

  public get currentUserValue() {
    return this.currentUserSubject$.value;
  }

  login(creds: LoginModel) {
    return this.http.post<User>(`${environment.webApi}auth/login`, creds).pipe(
      map((user: any) => {
        if (user) {
          localStorage.setItem('creds', JSON.stringify(user));
          console.log('setting current User');
          this.setCurrentUser(user);
        }
        return user;
      })
    );
  }

  logout() {
    localStorage.removeItem('creds');
    this.currentUserSubject$.next(null);
  }

  setCurrentUser(user: User) {
    this.currentUserSubject$.next(user);
  }

  public isLoggedIn() {
    const currentUser = this.currentUserValue;
    const isLoggedIn = !!currentUser && !!currentUser.jwt;
    return isLoggedIn;
  }

  public isAdmin(): boolean {
    if (this.currentUserSubject$.value.roleName != 'Admin') return false;
    else return true;
  }
}

export interface LoginResponse {}
