import { Component, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Subscription } from 'rxjs';
import { Role } from 'src/app/models/roles.model';
import { RolesService } from 'src/app/services/roles.service';
import { PermissionsService } from 'src/app/services/permissions.service';
import { PermissionsModel } from 'src/app/models/permissions.model';
import { RolePermission } from 'src/app/models/role-permission.model';
import { FormArray, FormGroup } from '@angular/forms';
@Component({
  selector: 'app-role-details',
  templateUrl: './role-details.component.html',
  styleUrls: ['./role-details.component.scss'],
})
export class RoleDetailsComponent implements OnInit, OnDestroy {
  role: Role;
  subs: Subscription[] = [];
  roleID: number;
  permissions: RolePermission[];

  constructor(
    private rolesService: RolesService,
    private permissionsService: PermissionsService,
    private route: ActivatedRoute
  ) {
    this.roleID = route.snapshot.params['roleID'];
  }

  ngOnInit(): void {
    this.subs.push(
      this.rolesService.getRoleByID(this.roleID).subscribe((res) => {
        this.role = res;
      })
    );

    this.subs.push(
      this.permissionsService
        .getRolePermissions(this.roleID)
        .subscribe((res) => {
          this.permissions = res;
        })
    );
  }

  onSubmit() {
    console.log("needs implemantation")
  }

  ngOnDestroy(): void {
    this.subs.forEach((x) => x.unsubscribe());
  }
}
