import { Component, OnInit } from '@angular/core';
import { LoginViewModel } from 'src/app/service/auth/login-model.model';
import { LoginService } from 'src/app/service/auth/login.service';
import { Router } from '@angular/router';
import { RouterNamesService } from 'src/app/service/router-names.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss'],
})
export class LoginComponent implements OnInit {
  constructor(private service: LoginService,
              private router: Router,
              private routerNameService: RouterNamesService) {}

  error: string;
  userLogin: LoginViewModel = {
    UserName: '',
    Password: ''
  };
  ngOnInit() { }
  login() {
    this.service.login(this.userLogin).subscribe((res: any) => {
      localStorage.setItem('token', res.token);
      this.router.navigateByUrl('/home');
    },
      err => {
        if (err.status === 400) {
          this.error = 'Username or password is wrong.';
        } else {
          console.log(err);
        }
      });
  }
  ionViewWillEnter() {
    this.routerNameService.name.next('Login');
  }
}
