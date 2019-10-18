import { Component } from '@angular/core';
import { LoginService } from '../service/auth/login.service';

@Component({
  selector: 'app-home',
  templateUrl: 'home.page.html',
  styleUrls: ['home.page.scss'],
})
export class HomePage {

  constructor(private loginService: LoginService) { }
  isCurrentUser() {
    if (this.loginService.User == null) { return false; }
    return true;
  }
}
