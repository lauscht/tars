import { TestBed } from '@angular/core/testing';

import { WeeklyHelper } from './weekly.helper';

describe('WeeklyService', () => {
  let service: WeeklyHelper;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(WeeklyHelper);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
