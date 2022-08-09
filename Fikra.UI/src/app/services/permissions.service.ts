import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { PermissionsModel } from '../models/permissions.model';
import { environment } from 'src/environments/environment';
import { RolePermission } from '../models/role-permission.model';
import { FormGroup } from '@angular/forms';

@Injectable({
  providedIn: 'root'
})
export class PermissionsService {

  constructor(private http: HttpClient) { }

  
  
  getRolePermissions(roleID: number) {
    return this.http.get<RolePermission[]>(
      `${environment.webApi}Permisions/get-role-permissions/${roleID}`
    );
  }
}
