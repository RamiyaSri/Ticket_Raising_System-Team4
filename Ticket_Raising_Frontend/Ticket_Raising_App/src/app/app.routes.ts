import { Routes } from '@angular/router';
import { TicketComponent } from './ticket-component/ticket-component';
import { TicketTypeComponent } from './tickettype-component/tickettype-component';
import { TicketPriorityComponent } from './ticketpriority-component/ticketpriority-component';  
import { EmployeeComponent } from './employee-component/employee-component';
import{TicketAssignmentComponent} from './ticketassignment-component/ticketassignment-component';
import { TicketCommentComponent } from './ticketcomment-component/ticketcomment-component';
import { LoginComponent } from './login-component/login-component';
import { RegisterComponent } from './register-component/register-component';
import { LogoutComponent } from './logout-component/logout-component';
import { HomeComponent } from './home-component/home-component';
import { userAccessGuard } from './user-access-guard';
export const routes: Routes = [
    {path: 'ticket', component: TicketComponent },
    {path: 'tickettype', component: TicketTypeComponent,canActivate:[userAccessGuard]},
    {path: 'ticketpriority', component: TicketPriorityComponent,canActivate:[userAccessGuard]},
    {path: 'employee', component: EmployeeComponent,canActivate:[userAccessGuard]},
    {path: 'ticketassignment', component: TicketAssignmentComponent,canActivate:[userAccessGuard]},
    {path: 'ticketcomment', component: TicketCommentComponent,canActivate:[userAccessGuard]},
    {path: 'login', component: LoginComponent},
    {path: 'register', component: RegisterComponent},
    {path: 'logout', component: LogoutComponent,canActivate:[userAccessGuard]},
    {path: '', component: HomeComponent,canActivate:[userAccessGuard]}
    
];
