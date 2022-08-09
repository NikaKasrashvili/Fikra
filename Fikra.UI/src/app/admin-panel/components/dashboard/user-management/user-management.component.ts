import { Component, OnDestroy, OnInit } from '@angular/core';
import { Subscription } from 'rxjs';
import { UserListItemModel } from 'src/app/models/user-list-item.model';
import { UsersService } from 'src/app/services/users.service';
@Component({
  selector: 'app-user-management',
  templateUrl: './user-management.component.html',
  styleUrls: ['./user-management.component.scss'],
})
export class UserManagementComponent implements OnInit, OnDestroy {
  users: UserListItemModel[];
  subs: Subscription[] = [];

  constructor(private usersService: UsersService) {}

  ngOnInit(): void {
    this.subs.push(
      this.usersService.getAllUsers().subscribe((res) => {
        console.log(res);
        this.users = res;
      })
    );
  }

  ngOnDestroy(): void {
    this.subs.forEach((x) => x.unsubscribe());
  }
}
