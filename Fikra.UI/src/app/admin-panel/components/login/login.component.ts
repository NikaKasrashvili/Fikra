import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { BehaviorSubject } from 'rxjs';
import { User } from 'src/app/models/user.model';
import { AuthService } from 'src/app/services/auth.service';
import { LoginModel } from '../../../models/login.model';
@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss'],
})
export class LoginComponent implements OnInit {
  loginForm: FormGroup = new FormGroup({
    email: new FormControl('', [Validators.required, Validators.email]),
    password: new FormControl('', Validators.required),
  });

  constructor(private authService: AuthService, private router: Router) {}

  ngOnInit(): void {
    if(this.authService.isAdmin()){
      this.router.navigateByUrl("admin/dashboard")
    }
  }

  onSubmit() {
    let creds: LoginModel = {
      email: this.loginForm.value.email,
      password: this.loginForm.value.password,
    };

    this.authService.login(creds).subscribe((res: User) => {
      if (res.roleName === 'Admin') {
        this.router.navigateByUrl('admin/dashboard');
      }
    });
  }
}
