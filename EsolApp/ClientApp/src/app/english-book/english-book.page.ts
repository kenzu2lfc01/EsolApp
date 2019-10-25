import { Component, OnInit, OnDestroy } from '@angular/core';
import { RouterNamesService } from '../service/router-names.service';
import { Englishbook } from '../service/englishbook/englishbook.model';
import { Observable } from 'rxjs';
import { HttpHeaders } from '@angular/common/http';
import { EnglishbookService } from '../service/englishbook/englishbook.service';
@Component({
  selector: 'app-english-book',
  templateUrl: './english-book.page.html',
  styleUrls: ['./english-book.page.scss'],
})
export class EnglishBookPage {
  constructor(private routerNameService: RouterNamesService, private englishService: EnglishbookService) {
  }
  dictionary: Englishbook = {
    english: '',
    vietnamese: ''
  };
  ionViewWillEnter() {
    this.routerNameService.name.next('English Book');
  }

  translate() {
    const headers = new HttpHeaders({
      'Content-Type': 'application/json',
      Accept: 'application/json'
    });

    const body = {
      q: this.dictionary.english,
      source: 'en',
      target: 'vi'
    };
    console.log(this.englishService.translate(body, headers));
  }

}
