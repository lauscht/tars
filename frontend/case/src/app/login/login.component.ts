import { Component, OnInit } from '@angular/core';
import { JwksValidationHandler, OAuthService } from 'angular-oauth2-oidc';
import { AuthCodeFlowConfig } from './authCodeFlowConfig';
import { filter } from 'rxjs/operators';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  userName: string = null;
  flightCount: number;

  get accessToken() {
    return this.oauthService.getAccessToken();
  }

  get refreshToken() {
    return this.oauthService.getRefreshToken();
  }

  get idToken() {
    return this.oauthService.getIdToken();
  }

  get userClaims(){
    return this.oauthService.getIdentityClaims();
  }

  constructor(private oauthService: OAuthService) {
    
    //importent we can not use responseType: 'code',
    //remarks:
    //https://medium.com/@ishmeetsingh/google-auth-integration-with-angular-5-and-angular-oauth2-oidc-ed01b997e1df
    //https://github.com/damienbod/angular-auth-oidc-client/issues/399
    //https://stackoverflow.com/questions/60724690/using-google-oidc-with-code-flow-and-pkce    

    oauthService.configure(AuthCodeFlowConfig);        
    this.oauthService.tokenValidationHandler = new JwksValidationHandler();
    this.oauthService.loadDiscoveryDocumentAndTryLogin();

    // oauthService.setupAutomaticSilentRefresh();    
    // oauthService.loadDiscoveryDocumentAndTryLogin().then(_ => {
    //   const claims = this.oauthService.getIdentityClaims();
    //   if (!claims) {
    //     return null;
    //   }    
    // });

    oauthService.events
      .pipe(filter(e => e.type === 'token_received'))
      .subscribe(_ => {
        console.debug('state', this.oauthService.state);
        this.oauthService.loadUserProfile();
      });

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
