import { TestBed } from '@angular/core/testing';

import { AccessControllerService } from './access-controller.service';

describe('AccessControllerService', () => {
  let service: AccessControllerService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(AccessControllerService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
