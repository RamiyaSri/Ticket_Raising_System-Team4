import { TestBed } from '@angular/core/testing';

import { TicketcommentService } from './ticketcomment-service';

describe('TicketcommentService', () => {
  let service: TicketcommentService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(TicketcommentService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
