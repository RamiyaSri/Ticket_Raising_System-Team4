import { TestBed } from '@angular/core/testing';

import { TicketassignmentService } from './ticketassignment-service';

describe('TicketassignmentService', () => {
  let service: TicketassignmentService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(TicketassignmentService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
