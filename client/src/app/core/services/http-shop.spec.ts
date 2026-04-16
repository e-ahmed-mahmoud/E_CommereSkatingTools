import { TestBed } from '@angular/core/testing';

import { HttpShop } from './http-shop';

describe('HttpShop', () => {
  let service: HttpShop;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(HttpShop);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
