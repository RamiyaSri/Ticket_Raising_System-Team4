import { inject, Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { TicketType } from '../Models/TicketType';
 
@Injectable({
  providedIn: 'root',
})
export class TicketTypeService {
 
  http: HttpClient = inject(HttpClient);
  httpOptions;
  token;
  baseUrl: string = "https://ticketportalapi-grcke7cnhccuc9fd.canadacentral-01.azurewebsites.net/api/TicketTypes/";   
 
  constructor() {
    this.token = sessionStorage.getItem("token");
    this.httpOptions = {
      headers: new HttpHeaders({
        'Authorization': 'Bearer ' + this.token
      })
    };
  }
 
  getAllTicketTypes(): Observable<TicketType[]> {
    return this.http.get<TicketType[]>(this.baseUrl, this.httpOptions);
  }
 
  addTicketType(ticketType: TicketType): Observable<TicketType> {
    return this.http.post<TicketType>(this.baseUrl, ticketType, this.httpOptions);
  }
 
  getTicketType(ticketTypeId: string): Observable<TicketType> {
    return this.http.get<TicketType>(this.baseUrl + ticketTypeId, this.httpOptions);
  }
 
  updateTicketType(ticketTypeId: string, ticketType: TicketType): Observable<TicketType> {
    return this.http.put<TicketType>(this.baseUrl + ticketTypeId, ticketType, this.httpOptions);
  }
 
  deleteTicketType(ticketTypeId: string): Observable<any> {
    return this.http.delete(this.baseUrl + ticketTypeId, this.httpOptions);
  }
 
  getTicketTypesByPriority(priorityId: string): Observable<TicketType[]> {
    return this.http.get<TicketType[]>(
      this.baseUrl + "priority/" + priorityId,
      this.httpOptions
    );
  }
}