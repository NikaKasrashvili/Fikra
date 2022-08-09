import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Role } from '../models/roles.model';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root',
})
export class RolesService {
  constructor(private http: HttpClient) {}

  getRoles() {
    return this.http.get<Role[]>(`${environment.webApi}Roles/get-all`);
  }

  getRoleByID(roleID: number) {
    return this.http.get<Role>(
      `${environment.webApi}Roles/get-by-id/${roleID}`
    );
  }

  
}
