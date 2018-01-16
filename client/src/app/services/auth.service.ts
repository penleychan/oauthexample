import { Injectable } from '@angular/core';
import {OidcClientSettings, User, UserManager, UserManagerSettings} from 'oidc-client';

@Injectable()
export class AuthService {
  private manager = new UserManager(getClientSettings());
  private user: User = null;

  constructor() {
    this.manager.getUser().then(user => {
      this.user = user;
    });
  }

  isLoggedIn(): boolean {
    return this.user !== null && !this.user.expired;
  }

  getClaims(): any {
    return this.user.profile;
  }

  getAuthorizationHeaderValue(): string {
    return `${this.user.token_type} ${this.user.access_token}`;
  }

  startAuthentication(): Promise<void> {
    return this.manager.signinRedirect();
  }

  completeAuthentication(): Promise<void> {
    return this.manager.signinRedirectCallback().then(user => {
      this.user = user;
    });
  }
}

export function getClientSettings(): UserManagerSettings {
  const userManagerSettings: UserManagerSettings = {};
  userManagerSettings.authority = 'http://localhost:5000/identity';
  userManagerSettings.client_id = 'angular_spa';
  userManagerSettings.redirect_uri = 'http://localhost:4200/auth-callback';
  userManagerSettings.post_logout_redirect_uri = 'http://localhost:4200';
  userManagerSettings.response_type = 'id_token token';
  userManagerSettings.scope = 'openid profile api1';
  userManagerSettings.filterProtocolClaims = true;
  userManagerSettings.loadUserInfo = true;

  return userManagerSettings;
}
