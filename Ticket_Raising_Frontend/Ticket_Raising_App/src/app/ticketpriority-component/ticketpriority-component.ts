import { Component, inject } from '@angular/core';
import { TicketPriorityService } from '../ticketpriority-service';
import { TicketPriority } from '../../Models/TicketPriority';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
 
@Component({
  selector: 'app-ticket-priority-component',
  imports: [CommonModule, FormsModule],
  templateUrl: './ticket-priority-component.html',
  styleUrl: './ticket-priority-component.css',
})
export class TicketPriorityComponent {
 
  prioritySvc: TicketPriorityService = inject(TicketPriorityService);
 
  priorities: TicketPriority[];
  priority: TicketPriority;
  errMsg: string;
  priorityLevel: string;  
  constructor() {
    this.priorities = [];
    this.priority = new TicketPriority("", "", "", 0, 0);
    this.errMsg = "";
    this.priorityLevel = "";
    this.showAllPriorities();
  }
 
  showAllPriorities() {
    this.prioritySvc.getAllPriorities().subscribe({
      next: (response: any) => {
        this.priorities = response;
        console.log(response);
        this.errMsg = "";
      },
      error: (err) => {
        this.errMsg = err.message;
        console.log(err);
      }
    });
  }
 
  savePriority() {
    this.prioritySvc.addPriority(this.priority).subscribe({
      next: () => {
        alert("New Priority added");
        this.errMsg = "";
        this.showAllPriorities();
      },
      error: (err) => this.errMsg = err.error
    });
  }
 
  newPriority() {
    this.priority = new TicketPriority("", "", "", 0, 0);
  }
 
  showPriority() {
    this.prioritySvc.getPriority(this.priority.priorityId).subscribe({
      next: (response: any) => {
        this.priority = response;
        this.errMsg = "";
      },
      error: (err) => this.errMsg = err.error
    });
  }
 
  updatePriority() {
    this.prioritySvc.updatePriority(this.priority.priorityId, this.priority).subscribe({
      next: () => {
        alert("Priority updated successfully");
        this.errMsg = "";
        this.showAllPriorities();
      },
      error: (err) => this.errMsg = err.error
    });
  }
 
  deletePriority() {
    this.prioritySvc.deletePriority(this.priority.priorityId).subscribe({
      next: () => {
        alert("Priority deleted successfully");
        this.errMsg = "";
        this.showAllPriorities();
      },
      error: (err) => this.errMsg = err.error
    });
  }
 
  showPriorityByLevel() {
    this.prioritySvc.getPriorityByLevel(this.priorityLevel).subscribe({
      next: (response: any) => {
        this.priority = response;
        this.errMsg = "";
      },
      error: (err) => this.errMsg = err.error
    });
  }
}