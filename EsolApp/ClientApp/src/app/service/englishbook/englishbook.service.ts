import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { map, tap } from 'rxjs/operators';
@Injectable({
  providedIn: 'root'
})
export class EnglishbookService {
  private API_KEY = 'AIzaSyDAfGGa9mqz5tt-pG8O2l4qk40fZz6TTn0';
  private translateUrl = 'https://translation.googleapis.com/language/translate/v2?key=' + this.API_KEY;
  translateApi = 'https://localhost:44346/api/EnglishBook';
  constructor(private http: HttpClient) { }
  translate(body, headers) {
    return this.http.post<any>(this.translateUrl, body, { headers });
  }
}
