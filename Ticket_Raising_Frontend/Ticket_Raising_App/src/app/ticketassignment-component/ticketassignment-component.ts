import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { TicketAssignmentService } from '../ticketassignment-service';
import { TicketAssignment } from '../../Models/TicketAssignment';
 
@Component({
  selector: 'app-ticketassignment-component',
  imports: [CommonModule, FormsModule],
  templateUrl: './ticketassignment-component.html',
  styleUrl: './ticketassignment-component.css',
})
export class TicketAssignmentComponent {
 
  assignmentSvc: TicketAssignmentService = inject(TicketAssignmentService);
 
  assignment: TicketAssignment;
  assignments: TicketAssignment[];
  errMsg: string;
 
  constructor() {
    this.assignments = [];
    this.errMsg = "";
    this.assignment = new TicketAssignment("", "", "", new Date());
    this.showAllAssignments();
  }
 
  newAssignment() {
    this.assignment = new TicketAssignment("", "", "", new Date());
  }
 
  showAllAssignments() {
    this.assignmentSvc.getAllAssignments().subscribe({
      next: (response: any) => {
        this.assignments = response;
        this.errMsg = "";
      },
      error: (err) => this.errMsg = err.message
    });
  }
 
  addAssignment() {
    this.assignmentSvc.addAssignment(this.assignment).subscribe({
      next: () => {
        alert("Ticket Assignment added");
        this.errMsg = "";
        this.showAllAssignments();
      },
      error: (err) => this.errMsg = err.message
    });
  }
 
  showAssignment() {
    this.assignmentSvc.getAssignment(this.assignment.assignmentId).subscribe({
      next: (response: any) => {
        this.assignment = response;
        this.errMsg = "";
      },
      error: (err) => this.errMsg = err.message
    });
  }
 
  updateAssignment() {
    this.assignmentSvc
      .updateAssignment(this.assignment.assignmentId, this.assignment)
      .subscribe({
        next: () => {
          alert("Assignment updated");
          this.errMsg = "";
          this.showAllAssignments();
        },
        error: (err) => this.errMsg = err.message
      });
  }
 
  deleteAssignment() {
    this.assignmentSvc
      .deleteAssignment(this.assignment.assignmentId)
      .subscribe({
        next: () => {
          alert("Assignment deleted");
          this.errMsg = "";
          this.showAllAssignments();
        },
        error: (err) => this.errMsg = err.message
      });
  }
 
  showAssignmentsByTicket() {
    this.assignmentSvc
      .getAssignmentsByTicket(this.assignment.ticketId)
      .subscribe({
        next: (response: any) => {
          this.assignments = response;
          this.errMsg = "";
        },
        error: (err) => this.errMsg = err.message
      });
  }
 
  showAssignmentsBySupportEmployee() {
    this.assignmentSvc
      .getAssignmentsBySupportEmployee(this.assignment.supportEmpId)
      .subscribe({
        next: (response: any) => {
          this.assignments = response;
          this.errMsg = "";
        },
        error: (err) => this.errMsg = err.message
      });
  }
}