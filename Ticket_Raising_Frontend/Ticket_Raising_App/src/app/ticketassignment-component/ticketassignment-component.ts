import { Component, inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { TicketAssignmentService } from '../ticketassignment-service';
import { TicketAssignment } from '../../Models/TicketAssignment';
import { TicketService } from '../ticket-service';
import { EmployeeService } from '../employee-service';
import { Ticket } from '../../Models/Ticket';
import { Employee } from '../../Models/Employee';
 
@Component({
  selector: 'app-ticketassignment-component',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './ticketassignment-component.html',
  styleUrls: ['./ticketassignment-component.css']
})
export class TicketAssignmentComponent implements OnInit {
  assignmentSvc = inject(TicketAssignmentService);
  ticketSvc = inject(TicketService);
  employeeSvc = inject(EmployeeService);
 
  assignment: TicketAssignment = new TicketAssignment("", "", "", new Date());
  assignments: TicketAssignment[] = [];
  tickets: Ticket[] = [];
  employees: Employee[] = [];
  assignmentDate?:Date;
  errMsg: string = '';
 
  ngOnInit() {
    this.loadTickets();
    this.loadEmployees();
    this.showAllAssignments();
    this.assignment.assignmentDate = new Date();
   
  }
 
  loadTickets() {
    this.ticketSvc.getAllTickets().subscribe({
      next: (data:any) => this.tickets = data,
      error: (err:any) => this.errMsg = err.error
    });
  }
 
  loadEmployees() {
    this.employeeSvc.getAllEmployees().subscribe({
      next: data => this.employees = data,
      error: err => this.errMsg = err.error
    });
  }
 
  newAssignment() {
    this.assignment = new TicketAssignment("", "", "", new Date());
    this.errMsg = '';
  }
 
  showAllAssignments() {
    this.assignmentSvc.getAllAssignments().subscribe({
      next: data => this.assignments = data,
      error: err => this.errMsg = err.error
    });
  }
 
  addAssignment() {
    if (!this.assignment.assignmentId || !this.assignment.ticketId || !this.assignment.Support_Emp_Id) {
      this.errMsg = "All fields are required";
      return;
    }
   
    this.assignmentSvc.addAssignment(this.assignment).subscribe({
      next: () => {
        alert("Assignment Added.");
        this.showAllAssignments();
        this.newAssignment();
      },
      error: err => this.errMsg = err.error
    });
  }
 
  showAssignment() {
    if (!this.assignment.assignmentId) {
      this.errMsg = "Enter Assignment ID";
      return;
    }
    this.assignmentSvc.getAssignment(this.assignment.assignmentId).subscribe({
      next: data => this.assignment = data,
      error: err => this.errMsg = err.error
    });
  }
 
  updateAssignment() {
    if (!this.assignment.assignmentId) {
      this.errMsg = "Enter Assignment ID";
      return;
    }
    this.assignmentSvc.updateAssignment(this.assignment.assignmentId, this.assignment).subscribe({
      next: () => this.showAllAssignments(),
      error: err => this.errMsg = err.error
    });
  }
 
  deleteAssignment() {
    if (!this.assignment.assignmentId) {
      this.errMsg = "Enter Assignment ID";
      return;
    }
    this.assignmentSvc.deleteAssignment(this.assignment.assignmentId).subscribe({
      next: () => {
        this.showAllAssignments();
        this.newAssignment();
      },
      error: err => this.errMsg = err.error
    });
  }
 
  showAssignmentsByTicket() {
    if (!this.assignment.ticketId) {
      this.errMsg = "Select Ticket";
      return;
    }
    this.assignmentSvc.getAssignmentsByTicket(this.assignment.ticketId).subscribe({
      next: data => this.assignments = data,
      error: err => this.errMsg = err.error
    });
  }
 
  showAssignmentsBySupportEmployee() {
    if (!this.assignment.Support_Emp_Id) {
      this.errMsg = "Select Employee";
      return;
    }
    this.assignmentSvc.getAssignmentsBySupportEmployee(this.assignment.Support_Emp_Id).subscribe({
      next: data => this.assignments = data,
      error: err => this.errMsg = err.error
    });
  }
  goBack() {
    this.errMsg = '';
    this.showAllAssignments();
    
}
}
 
 
 
 