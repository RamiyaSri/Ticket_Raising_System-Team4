import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TicketassignmentComponent } from './ticketassignment-component';   

describe('TicketassignmentComponent', () => {
  let component: TicketassignmentComponent;
  let fixture: ComponentFixture<TicketassignmentComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [TicketassignmentComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(TicketassignmentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
