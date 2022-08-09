import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { UserListItemModel } from '../models/user-list-item.model';
import { environment } from 'src/environments/environment';
import { UserByEmailModel } from '../models/user-by-email.model';

@Injectable({
  providedIn: 'root'
})
export class UsersService {

  constructor(private http: HttpClient) {}

  getAllUsers(){
    return this.http.get<UserListItemModel[]>(`${environment.webApi}Users/Get-All-Users`);
  }

  getUserByEmail(userEmail: string){
    return this.http.get<UserByEmailModel>(`${environment.webApi}Users/Get-User-By-Email/${userEmail}`);
  }

}
