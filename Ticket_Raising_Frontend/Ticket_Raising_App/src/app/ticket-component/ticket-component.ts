import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { TicketService } from '../ticket-service';
import { Ticket } from '../../Models/Ticket';
import { EmployeeService } from '../employee-service';
import { Employee } from '../../Models/Employee';
import { TicketTypeService } from '../tickettype-service';
import { TicketType } from '../../Models/TicketType';
import { TicketAssignmentService } from '../ticketassignment-service';
import { TicketAssignment } from '../../Models/TicketAssignment';
 
@Component({
  selector: 'app-ticket-component',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './ticket-component.html',
  styleUrls: ['./ticket-component.css'],
})
export class TicketComponent {
  username = sessionStorage.getItem('username');
  role = sessionStorage.getItem('role');
  EmpId = sessionStorage.getItem('empId');
 
  ticketSvc = inject(TicketService);
  employeeSvc = inject(EmployeeService);
  ticketTypeSvc = inject(TicketTypeService);
  assignmentSvc = inject(TicketAssignmentService);
 
  ticket: Ticket = new Ticket();
  tickets: Ticket[] = [];
  employees: Employee[] = [];
  ticketTypes: TicketType[] = [];
  errMsg: string = '';
 
 
  supportUserMap: { [ticketId: string]: string } = {};
 
  constructor() {
    this.ticket.status = 'Open';
    this.loadEmployees();
    this.loadTicketTypes();
    this.showAllTickets();
  }
 
  loadEmployees() {
    this.employeeSvc.getAllEmployees().subscribe({
      next: data => {
        this.employees = data.filter(e => e.role === 'User');
        this.ticket.empId = this.EmpId;
      },
      error: err => this.errMsg = err.error,
    });
  }
 
  loadTicketTypes() {
    this.ticketTypeSvc.getAllTicketTypes().subscribe({
      next: data => (this.ticketTypes = data),
      error: err => (this.errMsg = err.error),
    });
  }
 
  newTicket() {
    this.ticket = new Ticket();
    this.errMsg = '';
  }
 
  showAllTickets() {
 
    if (this.role === 'User') {
      this.ticketSvc.getAllTickets().subscribe({
        next: data => {
          this.tickets = data.filter(tkt => tkt.empId === this.EmpId);
          this.loadAssignmentsForTickets();
        },
        error: err => (this.errMsg = err.error),
      });
    }
 
    else if (this.role === 'Supporter') {
      this.assignmentSvc.getAssignmentsBySupportEmployee(this.EmpId!).subscribe({
        next: assignments => {
          this.tickets = [];
          this.supportUserMap = {};
 
          assignments.forEach(assign => {
            this.supportUserMap[assign.ticketId] = assign.Support_Emp_Id;
 
            this.ticketSvc.getTicket(assign.ticketId).subscribe({
              next: ticket => {
                if (!this.tickets.some(t => t.ticketId === ticket.ticketId)) {
                  this.tickets.push(ticket);
                }
              },
              error: err => console.error(err),
            });
          });
        },
        error: err => (this.errMsg = err.error),
      });
    }
   
    else {
      this.ticketSvc.getAllTickets().subscribe({
        next: data => {
          this.tickets = data;
          this.loadAssignmentsForTickets();
        },
        error: err => (this.errMsg = err.error),
      });
    }
  }
 
 
  loadAssignmentsForTickets() {
    this.assignmentSvc.getAllAssignments().subscribe({
      next: assignments => {
        this.supportUserMap = {};
        assignments.forEach(assign => {
          this.supportUserMap[assign.ticketId] = assign.Support_Emp_Id;
        });
      },
      error: err => (this.errMsg = err.error),
    });
  }
 
  addTicket() {
    this.ticketSvc.addTicket(this.ticket).subscribe({
      next: () => {
        alert('Ticket added');
        this.showAllTickets();
        this.newTicket();
      },
      error: err => (this.errMsg = err.error),
    });
  }
 
  showTicket() {
    this.ticketSvc.getTicket(this.ticket.ticketId).subscribe({
      next: data => (this.ticket = data),
      error: err => (this.errMsg = err.error),
    });
  }
 
  updateTicket() {
    this.ticketSvc.updateTicket(this.ticket.ticketId, this.ticket).subscribe({
      next: () => {
        alert('Ticket updated');
        this.showAllTickets();
      },
      error: err => (this.errMsg = err.error),
    });
  }
 
  deleteTicket() {
    this.ticketSvc.deleteTicket(this.ticket.ticketId).subscribe({
      next: () => {
        alert('Ticket deleted');
        this.showAllTickets();
        this.newTicket();
      },
      error: err => (this.errMsg = err.error),
    });
  }
 
  showTicketsByEmployee() {
    if (!this.ticket.empId) {
      this.errMsg = 'Select Employee';
      return;
    }
    this.ticketSvc.getTicketsByEmployee(this.ticket.empId).subscribe({
      next: data => {
        this.tickets = data;
        this.loadAssignmentsForTickets();
      },
      error: err => (this.errMsg = err.error),
    });
  }
 
  showTicketsByStatus() {
    this.ticketSvc.getTicketsByStatus(this.ticket.status).subscribe({
      next: data => {
        this.tickets = data;
        this.loadAssignmentsForTickets();
      },
      error: err => (this.errMsg = err.error),
    });
  }
 
  showTicketsByType() {
    if (!this.ticket.ticketTypeId) {
      this.errMsg = 'Select Ticket Type';
      return;
    }
    this.ticketSvc.getTicketsByType(this.ticket.ticketTypeId).subscribe({
      next: data => {
        this.tickets = data;
        this.loadAssignmentsForTickets();
      },
      error: err => (this.errMsg = err.error),
    });
  }
}