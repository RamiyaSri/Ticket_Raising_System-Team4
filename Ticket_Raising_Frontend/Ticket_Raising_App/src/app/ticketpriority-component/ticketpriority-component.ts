import { Component, inject } from '@angular/core';
import { TicketPriorityService } from '../ticketpriority-service';
import { TicketPriority } from '../../Models/TicketPriority';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
 
@Component({
  selector: 'app-ticket-priority-component',
  imports: [CommonModule, FormsModule],
  templateUrl: './ticketpriority-component.html',
  styleUrls: ['./ticketpriority-component.css'],
})
export class TicketPriorityComponent {
 
  prioritySvc = inject(TicketPriorityService);
 
  priorities: TicketPriority[] = [];
  priority: TicketPriority = new TicketPriority(undefined, "", "", 0, 0);
 
  errMsg: string = "";
 
  priorityLevel: string = "";
  priorityLevelSearch: string = "";
 
  constructor() {
    this.resetPriority();
    this.showAllPriorities();
  }
 
  resetPriority() {
    this.priority = new TicketPriority(undefined, "", "", 0, 0);
    this.priorityLevel = "";
  }
 
  showAllPriorities() {
    this.prioritySvc.getAllPriorities().subscribe({
      next: (res: TicketPriority[]) => {
        this.priorities = res;
        this.errMsg = "";
      },
      error: (err) => this.errMsg = err?.error?.message || err.message
    });
  }
 
  savePriority() {
    this.priority.priorityLevel = this.priorityLevel;
 
    this.prioritySvc.addPriority(this.priority).subscribe({
      next: () => {
        alert("New Priority added");
        this.resetPriority();
        this.showAllPriorities();
      },
      error: (err) => this.errMsg = err?.error?.message || err.message
    });
  }
 
  newPriority() {
    this.resetPriority();
    this.errMsg = "";
  }
 
  showPriority() {
    this.prioritySvc.getPriority(this.priority.priorityId).subscribe({
      next: (res: TicketPriority) => {
        this.priority = res;
        this.priorityLevel = res.priorityLevel;
        this.errMsg = "";
      },
      error: (err) => this.errMsg = err?.error?.message || err.message
    });
  }
 
  updatePriority() {
    this.priority.priorityLevel = this.priorityLevel;
 
    this.prioritySvc.updatePriority(this.priority.priorityId, this.priority).subscribe({
      next: () => {
        alert("Priority updated successfully");
        this.resetPriority();
        this.showAllPriorities();
      },
      error: (err) => this.errMsg = err?.error?.message || err.message
    });
  }
 
  deletePriority() {
    this.prioritySvc.deletePriority(this.priority.priorityId).subscribe({
      next: () => {
        alert("Priority deleted successfully");
        this.resetPriority();
        this.showAllPriorities();
      },
      error: (err) => this.errMsg = err?.error?.message || err.message
    });
  }
 
  showPriorityByLevel() {
    this.prioritySvc.getPriorityByLevel(this.priorityLevelSearch).subscribe({
      next: (res: TicketPriority[]) => {
        this.priorities = res;
        this.errMsg = "";
      },
      error: (err) => {
        this.errMsg = err?.error?.message || err.message;
        this.priorities = [];
      }
    });
  }
 
  goBack() {
    this.priorityLevelSearch = "";
    this.showAllPriorities();
  }
}
 
 