import { Component, inject } from '@angular/core';

import { CommonModule } from '@angular/common';

import { FormsModule } from '@angular/forms';
 
import { TicketService } from '../ticket-service';

import { Ticket } from '../../Models/Ticket';
 
import { EmployeeService } from '../employee-service';

import { Employee } from '../../Models/Employee';
 
import { TicketTypeService } from '../tickettype-service';

import { TicketType } from '../../Models/TicketType';
 
@Component({

  selector: 'app-ticket-component',

  standalone: true,

  imports: [CommonModule, FormsModule],

  templateUrl: './ticket-component.html',

  styleUrls: ['./ticket-component.css'],

})

export class TicketComponent {
 
  ticketSvc = inject(TicketService);

  employeeSvc = inject(EmployeeService);

  ticketTypeSvc = inject(TicketTypeService);
 


  ticket: Ticket = new Ticket();

  tickets: Ticket[] = [];
 
  employees: Employee[] = [];

  ticketTypes: TicketType[] = [];
 
  errMsg: string = '';
 
  constructor() {


    this.loadEmployees();

    this.loadTicketTypes();

    this.showAllTickets();

  }
 
  loadEmployees() {

    this.employeeSvc.getAllEmployees().subscribe({

      next: data => this.employees = data,

      error: err => this.errMsg = err.error

    });

  }
 
  loadTicketTypes() {

    this.ticketTypeSvc.getAllTicketTypes().subscribe({

      next: data => this.ticketTypes = data,

      error: err => this.errMsg = err.error

    });

  }
 
  newTicket() {

    this.ticket = new Ticket();

    this.errMsg = '';

  }
 
  showAllTickets() {

    this.ticketSvc.getAllTickets().subscribe({

      next: data => this.tickets = data,

      error: err => this.errMsg = err.error

    });

  }
 
  addTicket() {

    this.ticketSvc.addTicket(this.ticket).subscribe({

      next: () => {

        alert('Ticket added');

        this.showAllTickets();

        this.newTicket();

      },

      error: err => this.errMsg = err.error

    });

  }
 
  showTicket() {

    this.ticketSvc.getTicket(this.ticket.ticketId).subscribe({

      next: data => this.ticket = data,

      error: err => this.errMsg = err.error

    });

  }
 
  updateTicket() {

    this.ticketSvc.updateTicket(this.ticket.ticketId, this.ticket).subscribe({

      next: () => {

        alert('Ticket updated');

        this.showAllTickets();

      },

      error: err => this.errMsg = err.error

    });

  }
 
  deleteTicket() {

    this.ticketSvc.deleteTicket(this.ticket.ticketId).subscribe({

      next: () => {

        alert('Ticket deleted');

        this.showAllTickets();

        this.newTicket();

      },

      error: err => this.errMsg = err.error

    });

  }
 
  showTicketsByEmployee() {

    if (!this.ticket.empId) {

      this.errMsg = 'Select Employee';

      return;

    }
 
    this.ticketSvc.getTicketsByEmployee(this.ticket.empId).subscribe({

      next: data => this.tickets = data,

      error: err => this.errMsg = err.error

    });

  }
 
  showTicketsByStatus() {

    this.ticketSvc.getTicketsByStatus(this.ticket.status).subscribe({

      next: data => this.tickets = data,

      error: err => this.errMsg = err.error

    });

  }
 
  showTicketsByType() {

    if (!this.ticket.ticketTypeId) {

      this.errMsg = 'Select Ticket Type';

      return;

    }
 
    this.ticketSvc.getTicketsByType(this.ticket.ticketTypeId).subscribe({

      next: data => this.tickets = data,

      error: err => this.errMsg = err.error

    });

  }

}

 