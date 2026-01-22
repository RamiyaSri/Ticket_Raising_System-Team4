import { inject, Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { TicketPriority } from '../Models/TicketPriority';
 
@Injectable({ providedIn: 'root' })
export class TicketPriorityService {
 
  private http = inject(HttpClient);
  private readonly baseUrl = 'http://localhost:5175/api/TicketPriorities/';
 
  private getAuthHeaders(): HttpHeaders {
    const token = sessionStorage.getItem('token') || '';
    return new HttpHeaders({
      'Authorization': `Bearer ${token}`,
      'Content-Type': 'application/json'
    });
  }
 
  getAllPriorities(): Observable<TicketPriority[]> {
    return this.http.get<TicketPriority[]>(
      this.baseUrl,
      { headers: this.getAuthHeaders() }
    );
  }
 
  getPriority(priorityId: string): Observable<TicketPriority> {
    return this.http.get<TicketPriority>(
      this.baseUrl + encodeURIComponent(priorityId),
      { headers: this.getAuthHeaders() }
    );
  }
 
  addPriority(priority: TicketPriority): Observable<TicketPriority> {
    return this.http.post<TicketPriority>(
      this.baseUrl,
      priority,
      { headers: this.getAuthHeaders() }
    );
  }
 
  updatePriority(priorityId: string, priority: TicketPriority): Observable<TicketPriority> {
    return this.http.put<TicketPriority>(
      this.baseUrl + encodeURIComponent(priorityId),
      priority,
      { headers: this.getAuthHeaders() }
    );
  }
 
  deletePriority(priorityId: string): Observable<any> {
    return this.http.delete<any>(
      this.baseUrl + encodeURIComponent(priorityId),
      { headers: this.getAuthHeaders() }
    );
  }
 
getPriorityByLevel(level: string): Observable<TicketPriority[]> {
  return this.http.get<TicketPriority[]>(
    `${this.baseUrl}level/${level}`,
    { headers: this.getAuthHeaders() }
  );
}

}