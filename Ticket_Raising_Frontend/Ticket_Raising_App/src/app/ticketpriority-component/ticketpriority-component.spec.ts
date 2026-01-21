import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TicketpriorityComponent } from './ticketpriority-component';

describe('TicketpriorityComponent', () => {
  let component: TicketpriorityComponent;
  let fixture: ComponentFixture<TicketpriorityComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [TicketpriorityComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(TicketpriorityComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
