import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthGuard } from '../guards/auth.guard';
import { DashboardComponent } from './components/dashboard/dashboard.component';
import { MainComponent } from './components/dashboard/main/main.component';
import { PostDetailsComponent } from './components/dashboard/posts/post-details/post-details.component';
import { PostsComponent } from './components/dashboard/posts/posts.component';
import { RoleDetailsComponent } from './components/dashboard/role-management/role-details/role-details.component';
import { RoleManagementComponent } from './components/dashboard/role-management/role-management.component';
import { UserDetailsComponent } from './components/dashboard/user-management/user-details/user-details.component';
import { UserManagementComponent } from './components/dashboard/user-management/user-management.component';
import { LoginComponent } from './components/login/login.component';

const routes: Routes = [
  { path: '', component: LoginComponent },
  {
    path: 'dashboard',
    component: DashboardComponent,
    canActivate: [AuthGuard],
    children: [
      { path: '', component: MainComponent },
      { path: 'posts', component: PostsComponent },
      { path: 'posts/:id', component: PostDetailsComponent },
      { path: 'users', component: UserManagementComponent },
      { path: 'users/:userEmail', component: UserDetailsComponent},
      { path: 'roles', component: RoleManagementComponent},
      { path: 'roles/:roleID', component: RoleDetailsComponent}
    ],
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class AdminPanelRoutingModule {}
