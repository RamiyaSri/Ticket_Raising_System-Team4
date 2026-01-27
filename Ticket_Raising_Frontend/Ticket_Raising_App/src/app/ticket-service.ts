import { inject, Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Ticket } from '../Models/Ticket';
 
@Injectable({
  providedIn: 'root',
})
export class TicketService {
 
  http: HttpClient = inject(HttpClient);
  httpOptions: { headers: HttpHeaders };
  token: string | null;
  baseUrl: string = "https://ticketportalapi-grcke7cnhccuc9fd.canadacentral-01.azurewebsites.net/api/Tickets/";
 
  constructor() {
    this.token = sessionStorage.getItem("token");
    this.httpOptions = {
      headers: new HttpHeaders({
        'Authorization': 'Bearer ' + this.token
      })
    };
  }

  getAllTickets(): Observable<Ticket[]> {
    return this.http.get<Ticket[]>(this.baseUrl, this.httpOptions);
  }

  getTicket(ticketId: string): Observable<Ticket> {
    return this.http.get<Ticket>(this.baseUrl + ticketId, this.httpOptions);
  }

  addTicket(ticket: Ticket): Observable<Ticket> {
    return this.http.post<Ticket>(this.baseUrl, ticket, this.httpOptions);
  }

  updateTicket(ticketId: string, ticket: Ticket): Observable<Ticket> {
    return this.http.put<Ticket>(this.baseUrl + ticketId, ticket, this.httpOptions);
  }
 
  deleteTicket(ticketId: string): Observable<any> {
    return this.http.delete(this.baseUrl + ticketId, this.httpOptions);
  }
 
  getTicketsByEmployee(empId: string): Observable<Ticket[]> {
    return this.http.get<Ticket[]>(this.baseUrl + 'ByEmployee/' + empId, this.httpOptions);
  }

  getTicketsByStatus(status: string): Observable<Ticket[]> {
    return this.http.get<Ticket[]>(this.baseUrl + 'ByStatus/' + status, this.httpOptions);
  }
 
  getTicketsByType(ticketTypeId: string): Observable<Ticket[]> {
    return this.http.get<Ticket[]>(this.baseUrl + 'ByType/' + ticketTypeId, this.httpOptions);
  }
}