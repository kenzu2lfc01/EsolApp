import { Injectable } from '@angular/core';
import { LoginViewModel, UsersViewModel } from './login-model.model';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class LoginService {
  public username: string;
  public users: Array<UsersViewModel> = [];
  constructor(private http: HttpClient, private router: Router) { }
  rootUrl = 'https://localhost:44398/api/Account';
  login(dataLogin) {
    this.username = dataLogin.UserName;
    return this.http.post(this.rootUrl + '/Login', dataLogin);
  }
  logout() {
    localStorage.removeItem('token');
    this.router.navigate(['/home']);
  }
  isCurrentUser() {
    if (localStorage.getItem('token') == null) { return false; }
    return true;
  }

  getUsers() {
    // tslint:disable-next-line: max-line-length
    return this.http.get(this.rootUrl + '/GetUsers/' + localStorage.getItem('token')).subscribe((res) => {
      this.users = res as Array<UsersViewModel>;
    });
  }
}
