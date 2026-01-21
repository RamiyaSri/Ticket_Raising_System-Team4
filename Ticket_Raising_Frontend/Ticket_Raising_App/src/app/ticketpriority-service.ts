import { inject, Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { TicketPriority } from '../Models/TicketPriority';
 
@Injectable({
  providedIn: 'root',
})
export class TicketPriorityService {
 
  http: HttpClient = inject(HttpClient);
  httpOptions;
  token;
  baseUrl: string = "http://localhost:5175/api/TicketPriorities/";   
 
  constructor() {
    this.token = sessionStorage.getItem("token");
    this.httpOptions = {
      headers: new HttpHeaders({
        'Authorization': 'Bearer ' + this.token
      })
    };
  }

  getAllPriorities(): Observable<TicketPriority[]> {
    return this.http.get<TicketPriority[]>(this.baseUrl, this.httpOptions);
  }

  addPriority(priority: TicketPriority): Observable<TicketPriority> {
    return this.http.post<TicketPriority>(this.baseUrl, priority, this.httpOptions);
  }

  getPriority(priorityId: string): Observable<TicketPriority> {
    return this.http.get<TicketPriority>(this.baseUrl + priorityId, this.httpOptions);
  }

  updatePriority(priorityId: string, priority: TicketPriority): Observable<TicketPriority> {
    return this.http.put<TicketPriority>(this.baseUrl + priorityId, priority, this.httpOptions);
  }
 
  deletePriority(priorityId: string): Observable<any> {
    return this.http.delete(this.baseUrl + priorityId, this.httpOptions);
  }
 
  getPriorityByLevel(priorityLevel: string): Observable<TicketPriority> {
    return this.http.get<TicketPriority>(
      this.baseUrl + "level/" + priorityLevel,
      this.httpOptions
    );
  }
}