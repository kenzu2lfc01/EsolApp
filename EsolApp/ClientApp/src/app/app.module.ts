import { NgModule } from '@angular/core';
import { DialogComponent } from './todo/image/dialog/dialog.component';
import { BrowserModule } from '@angular/platform-browser';
import { RouteReuseStrategy } from '@angular/router';
import { IonicModule, IonicRouteStrategy } from '@ionic/angular';
import { SplashScreen } from '@ionic-native/splash-screen/ngx';
import { StatusBar } from '@ionic-native/status-bar/ngx';
import { HttpClientModule } from '@angular/common/http';
import { TodoService } from './service/todo/todo.service';
import { AppComponent } from './app.component';
import { NgxSpinnerModule } from 'ngx-spinner';
import { AppRoutingModule } from './app-routing.module';
import { LoginComponent } from './auth/login/login.component';
import { ToastrModule } from 'ngx-toastr';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RegisterComponent } from './auth/register/register.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { AfterRegisterComponent } from './auth/register/after-register/after-register.component';
import { MatDialogModule, MatToolbarModule} from '@angular/material';
import { ShowAddImageComponent } from './todo/image/dialog/show-add-image/show-add-image.component';
import { SpinerService } from './service/spiner.service';
import { LongPress } from './service/long-press';
import { SQLite } from '@ionic-native/sqlite/ngx';
import { SQLitePorter } from '@ionic-native/sqlite-porter/ngx';
import { SharedTodoComponent } from './todo/shared-todo/shared-todo.component';
import { FindComponent } from './find/find.component';
@NgModule({
  // tslint:disable-next-line: max-line-length
  declarations: [AppComponent, LoginComponent, FindComponent, SharedTodoComponent, LongPress, RegisterComponent, AfterRegisterComponent, DialogComponent, ShowAddImageComponent],
  entryComponents: [ShowAddImageComponent, FindComponent],
  imports: [BrowserModule, IonicModule.forRoot(),
    AppRoutingModule, HttpClientModule, MatDialogModule
    , ToastrModule.forRoot(), FormsModule, ReactiveFormsModule, BrowserAnimationsModule, MatToolbarModule, NgxSpinnerModule],
  providers: [
    SpinerService,
    StatusBar,
    SplashScreen,
    SQLite,
    TodoService,
    SQLitePorter,
    { provide: RouteReuseStrategy, useClass: IonicRouteStrategy }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
