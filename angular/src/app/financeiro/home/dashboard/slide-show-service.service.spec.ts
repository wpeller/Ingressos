import { TestBed } from '@angular/core/testing';

import { SlideShowServiceService } from './slide-show-service.service';

describe('SlideShowServiceService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: SlideShowServiceService = TestBed.get(SlideShowServiceService);
    expect(service).toBeTruthy();
  });
});
