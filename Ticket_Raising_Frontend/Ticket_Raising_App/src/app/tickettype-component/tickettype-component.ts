import { Component, inject } from '@angular/core';
import { TicketTypeService } from '../tickettype-service';
import { TicketType } from '../../Models/TicketType';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
 
@Component({
  selector: 'app-ticket-type-component',
  imports: [CommonModule, FormsModule],
  templateUrl: './ticket-type-component.html',
  styleUrl: './ticket-type-component.css',
})
export class TicketTypeComponent {
 
  ticketTypeSvc: TicketTypeService = inject(TicketTypeService);
  ticketTypes: TicketType[];
  ticketType: TicketType;
  errMsg: string;
  priorityId: string;   
 
  constructor() {
    this.ticketTypes = [];
    this.ticketType = new TicketType("", "", "", "");
    this.errMsg = "";
    this.priorityId = "";
    this.showAllTicketTypes();
  }
 
  showAllTicketTypes() {
    this.ticketTypeSvc.getAllTicketTypes().subscribe({
      next: (response: any) => {
        this.ticketTypes = response;
        console.log(response);
        this.errMsg = "";
      },
      error: (err) => { 
        this.errMsg = err.message; 
        console.log(err); 
      }
    });
  }
 
  saveTicketType() {
    this.ticketTypeSvc.addTicketType(this.ticketType).subscribe({
      next: (response: any) => {
        alert("New Ticket Type added");
        this.errMsg = "";
        this.showAllTicketTypes();
      },
      error: (err) => this.errMsg = err.error
    });
  }
 
  newTicketType() {
    this.ticketType = new TicketType("", "", "", "");
  }
 
  showTicketType() {
    this.ticketTypeSvc.getTicketType(this.ticketType.ticketTypeId).subscribe({
      next: (response: any) => {
        this.ticketType = response;
        this.errMsg = "";
      },
      error: (err) => this.errMsg = err.error
    });
  }
 
  updateTicketType() {
    this.ticketTypeSvc.updateTicketType(this.ticketType.ticketTypeId, this.ticketType).subscribe({
      next: (response: any) => {
        alert("Ticket Type updated successfully");
        this.errMsg = "";
        this.showAllTicketTypes();
      },
      error: (err) => this.errMsg = err.error
    });
  }
 
  deleteTicketType() {
    this.ticketTypeSvc.deleteTicketType(this.ticketType.ticketTypeId).subscribe({
      next: (response: any) => {
        alert("Ticket Type deleted successfully");
        this.errMsg = "";
        this.showAllTicketTypes();
      },
      error: (err) => this.errMsg = err.error
    });
  }
 
  showTicketTypesByPriority() {
    this.ticketTypeSvc.getTicketTypesByPriority(this.priorityId).subscribe({
      next: (response: any) => {
        this.ticketTypes = response;
        this.errMsg = "";
      },
      error: (err) => this.errMsg = err.error
    });
  }
}
 
 