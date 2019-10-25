import { Component } from '@angular/core';
import { LoginService } from '../service/auth/login.service';
import { RouterNamesService } from '../service/router-names.service';

@Component({
  selector: 'app-home',
  templateUrl: 'home.page.html',
  styleUrls: ['home.page.scss'],
})
export class HomePage {

  constructor(private loginService: LoginService, private routerNameService: RouterNamesService) {
  }
  logout() {
    this.loginService.logout();
  }
  ionViewWillEnter() {
    this.routerNameService.name.next('Home');
  }
}
