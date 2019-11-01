import { Component, OnDestroy } from '@angular/core';

import { Platform } from '@ionic/angular';
import { SplashScreen } from '@ionic-native/splash-screen/ngx';
import { StatusBar } from '@ionic-native/status-bar/ngx';
import { MenuController } from '@ionic/angular';
import { LoginService } from './service/auth/login.service';
import { RouterNamesService } from './service/router-names.service';
import { Subscription } from 'rxjs';
import { MatDialog, MatDialogConfig } from '@angular/material';
import { FindComponent } from './find/find.component';
@Component({
  selector: 'app-root',
  templateUrl: 'app.component.html',
  styleUrls: ['app.component.scss']
})
export class AppComponent {
  constructor(
    private platform: Platform,
    private splashScreen: SplashScreen,
    private statusBar: StatusBar,
    private menu: MenuController,
    private service: LoginService,
    private dialog: MatDialog,
    private routerNameService: RouterNamesService
  ) {
    this.myValueSub = this.routerNameService.name.subscribe((n: any) => this.routerName = n);
    this.initializeApp();
  }
  myValueSub: Subscription;

  routerName = '';
  public openMenu() {
    this.menu.enable(true, 'frist');
    this.menu.open('frist');
  }
  logout() {
    this.service.logout();
  }
  initializeApp() {
    this.platform.ready().then(() => {
      this.statusBar.styleDefault();
      this.splashScreen.hide();
    });
  }
}
