import { CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { EnglishBookPage } from './english-book.page';

describe('EnglishBookPage', () => {
  let component: EnglishBookPage;
  let fixture: ComponentFixture<EnglishBookPage>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ EnglishBookPage ],
      schemas: [CUSTOM_ELEMENTS_SCHEMA],
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EnglishBookPage);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
