import { TestBed } from '@angular/core/testing';

import { PupilService } from './pupil.service';

describe('PupilService', () => {
  let service: PupilService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(PupilService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
