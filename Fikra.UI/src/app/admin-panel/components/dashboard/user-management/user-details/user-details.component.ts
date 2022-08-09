import { Component, OnDestroy, OnInit } from '@angular/core';
import { UsersService } from 'src/app/services/users.service';
import { ActivatedRoute } from '@angular/router';
import { UserByEmailModel } from 'src/app/models/user-by-email.model';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-user-details',
  templateUrl: './user-details.component.html',
  styleUrls: ['./user-details.component.scss'],
})
export class UserDetailsComponent implements OnInit, OnDestroy{
  user: UserByEmailModel;
  userEmail: string;
  subs: Subscription[]=[];

  constructor(
    private usersService: UsersService,
    private route: ActivatedRoute
  ) {
    this.userEmail = this.route.snapshot.params['userEmail'];
  }

  ngOnInit(): void {
    this.subs.push(
      this.usersService.getUserByEmail(this.userEmail).subscribe((res) => {
        this.user = res;
      })
    );
  }

  ngOnDestroy(): void {
    this.subs.forEach(x => x.unsubscribe() );
  }
}
