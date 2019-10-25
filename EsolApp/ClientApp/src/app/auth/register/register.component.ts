import { Component, OnInit } from '@angular/core';
import { RouterNamesService } from 'src/app/service/router-names.service';
import { RegisterModel } from 'src/app/service/auth/register-model.model';
import { RegisterService } from 'src/app/service/auth/register.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss'],
})
export class RegisterComponent implements OnInit {
  message = '';
  RegisterData: RegisterModel = {
    UserName: '',
    Email: '',
    Password: '',
    Name: ''
  };
  retyePassword = '';
  constructor(private routerNameService: RouterNamesService, private registerService: RegisterService, private router: Router) {
  }
  ionViewWillEnter() {
    this.routerNameService.name.next('Register');
  }
  ngOnInit() { }
  register() {
    if (this.retyePassword !== this.RegisterData.Password) {
      this.message = 'Retype password is wrong';
    } else {
      this.registerService.register(this.RegisterData).subscribe((res: any) => {
        console.log(res);
        //this.router.navigateByUrl('/afterregister');
      },
        err => {
          if (err.Status === 400) {
            this.message = 'Username or Email is already';
          } else {
            console.log(err);
          }
        });
    }
    this.router.navigateByUrl('/afterregister');
  }
}
