import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import {ProtectedComponent} from './protected/protected.component';
import {AuthGuardService} from './services/auth-guard.service';
import {AuthCallbackComponent} from './auth-callback/auth-callback.component';

const routes: Routes = [
  {
    path: '',
    children: []
  },
  {
    path: 'auth-callback',
    component: AuthCallbackComponent
  },
  {
    path: 'protected',
    component: ProtectedComponent,
    canActivate: [AuthGuardService]
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
