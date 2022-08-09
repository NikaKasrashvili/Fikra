import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';

import { AdminPanelRoutingModule } from './admin-panel-routing.module';
import { AdminPanelComponent } from './admin-panel.component';
import { LoginComponent } from './components/login/login.component';
import { DashboardComponent } from './components/dashboard/dashboard.component';
import { HeaderComponent } from './components/shared/header/header.component';
import { PostsComponent } from './components/dashboard/posts/posts.component';
import { PostDetailsComponent } from './components/dashboard/posts/post-details/post-details.component';
import { MainComponent } from './components/dashboard/main/main.component';
import { UserManagementComponent } from './components/dashboard/user-management/user-management.component';
import { UserDetailsComponent } from './components/dashboard/user-management/user-details/user-details.component';
import { RoleManagementComponent } from './components/dashboard/role-management/role-management.component';
import { RoleDetailsComponent } from './components/dashboard/role-management/role-details/role-details.component';


@NgModule({
  declarations: [
    AdminPanelComponent,
    LoginComponent,
    DashboardComponent,
    HeaderComponent,
    PostsComponent,
    PostDetailsComponent,
    MainComponent,
    UserManagementComponent,
    UserDetailsComponent,
    RoleManagementComponent,
    RoleDetailsComponent,
  ],
  imports: [
    CommonModule,
    AdminPanelRoutingModule,
    ReactiveFormsModule
  ]
})
export class AdminPanelModule { }
