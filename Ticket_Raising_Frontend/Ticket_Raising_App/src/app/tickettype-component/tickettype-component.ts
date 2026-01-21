import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
 
import { TicketTypeService } from '../tickettype-service';
import { TicketPriorityService } from '../ticketpriority-service';
 
import { TicketType } from '../../Models/TicketType';
import { TicketPriority } from '../../Models/TicketPriority';
 
@Component({
  selector: 'app-ticket-type-component',
  imports: [CommonModule, FormsModule],
  templateUrl: './tickettype-component.html',
  styleUrl: './tickettype-component.css',
})
export class TicketTypeComponent {
 
  ticketTypeSvc: TicketTypeService = inject(TicketTypeService);
  prioritySvc: TicketPriorityService = inject(TicketPriorityService);
 
  ticketType: TicketType;
  ticketTypes: TicketType[];
 
  priorities: TicketPriority[];   
  priorityId: string;      
 
  errMsg: string;
 
  constructor() {
    this.ticketTypes = [];
    this.priorities = [];
    this.priorityId = "";
    this.errMsg = "";
    this.ticketType = new TicketType("", "", "", "");
 
    this.showAllTicketTypes();
    this.loadPriorities();
  }
 
 
  loadPriorities() {
    this.prioritySvc.getAllPriorities().subscribe({
      next: (response: TicketPriority[]) => {
        this.priorities = response;
        this.errMsg = "";
      },
      error: (err) => this.errMsg = err.message
    });
  }
 
 
  showAllTicketTypes() {
    this.ticketTypeSvc.getAllTicketTypes().subscribe({
      next: (response: any) => {
        this.ticketTypes = response;
        this.errMsg = "";
      },
      error: (err) => this.errMsg = err.message
    });
  }
 
  saveTicketType() {
    this.ticketTypeSvc.addTicketType(this.ticketType).subscribe({
      next: () => {
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
      next: () => {
        alert("Ticket Type updated successfully");
        this.errMsg = "";
        this.showAllTicketTypes();
      },
      error: (err) => this.errMsg = err.error
    });
  }
 
  deleteTicketType() {
    this.ticketTypeSvc.deleteTicketType(this.ticketType.ticketTypeId).subscribe({
      next: () => {
        alert("Ticket Type deleted successfully");
        this.errMsg = "";
        this.showAllTicketTypes();
      },
      error: (err) => this.errMsg = err.error
    });
  }
 
  showTicketTypesByPriority() {
    console.log("Selected Priority Id:", this.priorityId);
 
    this.ticketTypeSvc.getTicketTypesByPriority(this.priorityId).subscribe({
      next: (response: any) => {
        console.log("Filtered Ticket Types:", response);
        this.ticketTypes = response;
        this.errMsg = "";
      },
      error: (err) => {
        console.log("API Error:", err);
        this.errMsg = err.error || err.message;
      }
    });
  }
}