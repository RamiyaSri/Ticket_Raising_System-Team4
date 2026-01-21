import { Routes } from '@angular/router';
import { TicketComponent } from './ticket-component/ticket-component';
import { TicketTypeComponent } from './tickettype-component/tickettype-component';
import { TicketPriorityComponent } from './ticketpriority-component/ticketpriority-component';  
export const routes: Routes = [
    { path: 'ticket', component: TicketComponent },
    { path: 'tickettype', component: TicketTypeComponent },
    { path: 'ticketpriority', component: TicketPriorityComponent }
];
