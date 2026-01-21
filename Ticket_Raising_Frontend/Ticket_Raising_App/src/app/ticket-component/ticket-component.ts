import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { TicketService } from '../ticket-service';
import { Ticket } from '../../Models/Ticket';
 
@Component({
  selector: 'app-ticket-component',
  imports: [CommonModule, FormsModule],
  templateUrl: './ticket-component.html',
  styleUrl: './ticket-component.css',
})
export class TicketComponent {
 
  TicketSvc: TicketService = inject(TicketService);
 
  ticket: Ticket;
  tickets: Ticket[];
  errMsg: string;
 
  constructor() {
    this.tickets = [];
    this.errMsg = "";
    this.showAllTickets();
    this.ticket = new Ticket("", "", "", "", "", "", null, null);
  }
 
  newTicket() {
    this.ticket = new Ticket("", "", "", "", "", "", null, null);
  }
 
  showAllTickets() {
    this.TicketSvc.getAllTickets().subscribe({
      next: (response: any) => {
        this.tickets = response;
        this.errMsg = "";
      },
      error: (err) => this.errMsg = err.error
    });
  }
 
  addTicket() {
    this.TicketSvc.addTicket(this.ticket).subscribe({
      next: (response: any) => {
        alert("New ticket added");
        this.errMsg = "";
        this.showAllTickets();
      },
      error: (err) => this.errMsg = err.error
    });
  }
 
  showTicket() {
    this.TicketSvc.getTicket(this.ticket.ticketId).subscribe({
      next: (response: any) => {
        this.ticket = response;
        this.errMsg = "";
      },
      error: (err) => this.errMsg = err.error
    });
  }
 
  updateTicket() {
    this.TicketSvc.updateTicket(this.ticket.ticketId, this.ticket).subscribe({
      next: (response: any) => {
        alert("Ticket details updated");
        this.errMsg = "";
        this.showAllTickets();
      },
      error: (err) => this.errMsg = err.error
    });
  }
 
  deleteTicket() {
    this.TicketSvc.deleteTicket(this.ticket.ticketId).subscribe({
      next: (response: any) => {
        alert("Ticket deleted");
        this.errMsg = "";
        this.showAllTickets();
      },
      error: (err) => this.errMsg = err.error
    });
  }
 
  showTicketsByEmployee() {
    this.TicketSvc.getTicketsByEmployee(this.ticket.empId).subscribe({
      next: (response: any) => {
        this.tickets = response;
        this.errMsg = "";
      },
      error: (err) => this.errMsg = err.error
    });
  }
 
  showTicketsByStatus() {
    this.TicketSvc.getTicketsByStatus(this.ticket.status).subscribe({
      next: (response: any) => {
        this.tickets = response;
        this.errMsg = "";
      },
      error: (err) => this.errMsg = err.error
    });
  }
 
  showTicketsByType() {
    this.TicketSvc.getTicketsByType(this.ticket.ticketTypeId).subscribe({
      next: (response: any) => {
        this.tickets = response;
        this.errMsg = "";
      },
      error: (err) => this.errMsg = err.error
    });
  }
}