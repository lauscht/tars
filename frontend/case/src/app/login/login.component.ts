import { Component, OnInit } from '@angular/core';
import { OAuthService } from 'angular-oauth2-oidc';
import { AuthCodeFlowConfig } from './authCodeFlowConfig';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  constructor(private oauthService: OAuthService) {
    oauthService.configure(AuthCodeFlowConfig);    
    oauthService.setupAutomaticSilentRefresh();
  }

  ngOnInit(): void {
  }

  login(): void {
    console.log('Login pressed..')
    this.oauthService.initLoginFlow();
  }

  logout(): void {
    console.log('Logout pressed..')
    this.oauthService.logOut();
  }

}
