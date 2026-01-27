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
import { forkJoin } from 'rxjs';
 
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
  isStatusEnabled: boolean = false;
  assignedTickets: Ticket[] = [];
  statusMap: { [ticketId: string]: string } = {};
  supportUserMap: { [ticketId: string]: string } = {};
 
  constructor() {
    this.ticket.status = 'Open';
    this.ticket.empId = this.EmpId || '';
    this.loadEmployees();
    this.loadTicketTypes();
    this.showAllTickets();
    this.EmpId = (sessionStorage.getItem('empId') || '').trim();
     this.role = (sessionStorage.getItem('role') || '').trim();
    if (this.role === 'Supporter' || this.role === 'User') {
    this.loadAssignedTicketsForSupporter();
  }
 
  }
loadAssignedTicketsForSupporter() {
 
  if (this.role !== 'Supporter' && this.role !== 'User') return;
  console.log('Supporter EmpId:', this.EmpId);
  this.assignmentSvc.getAllAssignments().subscribe({
    next: assignments => {
      console.log('Assignments fetched:', assignments);
      assignments  = assignments.filter(as=> as.Support_Emp_Id === this.EmpId);
      this.assignedTickets = [];
      this.statusMap = {};
      assignments.forEach(assign => {
        this.ticketSvc.getTicket(assign.ticketId).subscribe({
          next: ticket => {
            this.assignedTickets.push(ticket);
            this.statusMap[ticket.ticketId] = ticket.status;
          },
          error: err => console.error('Error fetching ticket:', err)
        });
      });
    },
    error: err => {
      console.error('Error fetching assignments:', err);
      this.errMsg = err.error || 'Failed to load assignments';
    }
  });
}
 
  isSupporterAssigned(ticketId: string): boolean {
    return this.role === 'Supporter' || this.role === 'User' &&
           this.supportUserMap[ticketId] === this.EmpId;
  }
 
  loadEmployees() {
    this.employeeSvc.getAllEmployees().subscribe({
      next: data => {
        this.employees = data;
        this.ticket.empId = this.EmpId;
      },
      error: err => this.errMsg = err.error,
    });
  }
  goBack() {
  this.errMsg = '';
  this.newTicket();
  this.tickets = [];
  this.assignedTickets = [];
  this.statusMap = {};
  this.supportUserMap = {};
  this.showAllTickets();
}
 
 
  loadTicketTypes() {
    this.ticketTypeSvc.getAllTicketTypes().subscribe({
      next: data => this.ticketTypes = data,
      error: err => this.errMsg = err.error,
    });
  }
 
  newTicket() {
    this.ticket = new Ticket();
    this.errMsg = '';
    this.isStatusEnabled = false;
    this.ticket.empId = this.EmpId || '';
    this.ticket.status = 'Open';
  }
updateTicketStatus(ticketId: string) {
  const ticket = this.assignedTickets.find(t => t.ticketId === ticketId);
  if (!ticket) return;
 
  ticket.status = this.statusMap[ticketId];
 
  if (ticket.status === 'Resolved') {
    ticket.resolutionDate = new Date();
  } else {
    ticket.resolutionDate = null;
  }
 
  this.ticketSvc.updateTicket(ticketId, ticket).subscribe({
    next: () => {
      alert('Status updated');
      this.showAllTickets();
 
      const index = this.tickets.findIndex(t => t.ticketId === ticketId);
      if (index !== -1) {
        this.tickets[index] = { ...ticket };
      }
      const assignedIndex = this.assignedTickets.findIndex(t => t.ticketId === ticketId);
      if (assignedIndex !== -1) {
        this.assignedTickets[assignedIndex] = { ...ticket };
      }
    },
    error: (err: any) => alert(err.error)
  });
}

 
  showAllTickets() {
    if (this.role === 'User') {
      this.ticketSvc.getAllTickets().subscribe({
        next: data => {
          this.tickets = data.filter(tkt => tkt.empId === this.EmpId);
          this.loadAssignmentsForTickets();
        },
        error: err => this.errMsg = err.error,
      });
    }
   
 
 else if (this.role === 'User') {
  this.assignmentSvc.getAssignmentsBySupportEmployee(this.EmpId!).subscribe({
    next: assignments => {
      this.tickets = [];
      this.supportUserMap = {};
 
      assignments.forEach(assign => {
        this.supportUserMap[assign.ticketId] = assign.Support_Emp_Id;
      });
 
      const ticketObservables = assignments.map(assign =>
        this.ticketSvc.getTicket(assign.ticketId)
      );
 
      forkJoin(ticketObservables).subscribe({
        next: tickets => {
          this.tickets = tickets.filter(
            (t, i, arr) => arr.findIndex(tt => tt.ticketId === t.ticketId) === i
          );
 
          this.loadAssignedTicketsForSupporter();
        },
        error: err => console.error(err),
      });
    },
    error: err => this.errMsg = err.error,
  });
}
 
    else {
      this.ticketSvc.getAllTickets().subscribe({
        next: data => {
          this.tickets = data;
          this.loadAssignmentsForTickets();
        },
        error: err => this.errMsg = err.error,
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
      error: err => this.errMsg = err.error,
    });
  }
 
  showTicket() {
    this.ticketSvc.getTicket(this.ticket.ticketId).subscribe({
      next: data => {
        this.ticket = data;
        this.isStatusEnabled = true;
        this.errMsg = '';
      },
      error: () => {
        this.errMsg = 'Ticket not found';
        this.isStatusEnabled = false;
      }
    });
  }
 
  addTicket() {
    this.ticketSvc.addTicket(this.ticket).subscribe({
      next: () => {
        alert('Ticket added');
        this.showAllTickets();
        this.newTicket();
      },
      error: err => this.errMsg = err.error,
    });
  }
 
  updateTicket() {
    this.ticketSvc.updateTicket(this.ticket.ticketId, this.ticket).subscribe({
      next: () => {
        alert('Ticket updated');
        this.showAllTickets();
      },
      error: err => this.errMsg = err.error,
    });
  }
 
  deleteTicket() {
    this.ticketSvc.deleteTicket(this.ticket.ticketId).subscribe({
      next: () => {
        alert('Ticket deleted');
        this.showAllTickets();
        this.newTicket();
      },
      error: err => this.errMsg = err.error,
    });
  }
 
private applyRoleFilter(data: Ticket[]): Ticket[] {
  if (this.role === 'User') {
    return data.filter(t => t.empId === this.EmpId);
  }
  return data; 
}

  private setTickets(data: Ticket[]) {
  if (this.role === 'User') {
    this.tickets = data.filter(
      t => t.empId === this.EmpId
    );

  } else {
    this.tickets = data;
  }
  this.loadAssignmentsForTickets();
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
      error: err => this.errMsg = err.error,
    });
  }
 
  showTicketsByStatus() {
  this.ticketSvc.getTicketsByStatus(this.ticket.status).subscribe({
    next: data => {

      if (this.role === 'User') {
        this.tickets = data.filter(t => t.empId === this.EmpId);
      }

      else {
        this.tickets = data;
      }

      this.loadAssignmentsForTickets();
    },
    error: err => this.errMsg = err.error,
  });
}
showTicketsByType() {
  if (!this.ticket.ticketTypeId) {
    this.errMsg = 'Select Ticket Type';
    return;
  }

  this.ticketSvc.getTicketsByType(this.ticket.ticketTypeId).subscribe({
    next: data => {

      if (this.role === 'User') {
        this.tickets = data.filter(t => t.empId === this.EmpId);
      } else {
        this.tickets = data;
      }

      this.loadAssignmentsForTickets();
    },
    error: err => this.errMsg = err.error,
  });
}

}