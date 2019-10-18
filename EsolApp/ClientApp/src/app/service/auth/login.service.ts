import { Injectable } from '@angular/core';
import { LoginModel } from './login-model.model';

@Injectable({
  providedIn: 'root'
})
export class LoginService {
  User: LoginModel = null;
  constructor() { }
}
