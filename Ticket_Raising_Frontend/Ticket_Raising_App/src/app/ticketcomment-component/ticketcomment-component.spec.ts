import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TicketcommentComponent } from './ticketcomment-component';

describe('TicketcommentComponent', () => {
  let component: TicketcommentComponent;
  let fixture: ComponentFixture<TicketcommentComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [TicketcommentComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(TicketcommentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
