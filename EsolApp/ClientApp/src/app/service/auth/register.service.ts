import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
@Injectable({
  providedIn: 'root'
})
export class RegisterService {
  rootUrl = 'https://localhost:44398/api/Account';
  constructor(private http: HttpClient) { }
  register(registerData) {
    return this.http.post(this.rootUrl + '/register', registerData);
  }
}
