import { Component, OnDestroy, OnInit } from '@angular/core';
import { Subscription } from 'rxjs';
import { Role } from 'src/app/models/roles.model';
import { RolesService } from 'src/app/services/roles.service';

@Component({
  selector: 'app-role-management',
  templateUrl: './role-management.component.html',
  styleUrls: ['./role-management.component.scss']
})
export class RoleManagementComponent implements OnInit, OnDestroy {
  roles: Role[];
  subs: Subscription[]=[];

  constructor(private rolesService : RolesService) { }

  ngOnInit(): void {
    this.subs.push(
      this.rolesService.getRoles().subscribe(res=>{
        this.roles=res;
      })
    );
  }

  ngOnDestroy(): void {
    this.subs.forEach(x=> x.unsubscribe());
  }
}
