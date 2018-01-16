import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';

import { AppComponent } from './app.component';
import { ProtectedComponent } from './protected/protected.component';
import {AuthGuardService} from './services/auth-guard.service';
import { AuthCallbackComponent } from './auth-callback/auth-callback.component';
import {AuthModule} from './shared/auth/auth.module';


@NgModule({
  declarations: [
    AppComponent,
    ProtectedComponent,
    AuthCallbackComponent
  ],
  imports: [
    AuthModule.forRoot(),
    BrowserModule,
    AppRoutingModule
  ],
  providers: [
    AuthGuardService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
