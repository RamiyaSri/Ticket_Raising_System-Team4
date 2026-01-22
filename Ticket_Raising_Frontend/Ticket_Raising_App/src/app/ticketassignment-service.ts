import { inject, Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { TicketAssignment } from '../Models/TicketAssignment';

@Injectable({ providedIn: 'root' })
export class TicketAssignmentService {
  http: HttpClient = inject(HttpClient);
  token: string | null = sessionStorage.getItem("token");
  httpOptions = {
    headers: new HttpHeaders({ 'Authorization': 'Bearer ' + this.token })
  };
  baseUrl: string = "http://localhost:5175/api/TicketAssignments";

  getAllAssignments(): Observable<TicketAssignment[]> {
    return this.http.get<TicketAssignment[]>(this.baseUrl, this.httpOptions);
  }

  getAssignment(assignmentId: string): Observable<TicketAssignment> {
    return this.http.get<TicketAssignment>(`${this.baseUrl}/${assignmentId}`, this.httpOptions);
  }

  addAssignment(assignment: TicketAssignment): Observable<TicketAssignment> {
    return this.http.post<TicketAssignment>(this.baseUrl, assignment, this.httpOptions);
  }

  updateAssignment(assignmentId: string, assignment: TicketAssignment): Observable<TicketAssignment> {
    return this.http.put<TicketAssignment>(`${this.baseUrl}/${assignmentId}`, assignment, this.httpOptions);
  }

  deleteAssignment(assignmentId: string): Observable<any> {
    return this.http.delete(`${this.baseUrl}/${assignmentId}`, this.httpOptions);
  }

  getAssignmentsByTicket(ticketId: string): Observable<TicketAssignment[]> {
    return this.http.get<TicketAssignment[]>(`${this.baseUrl}/ByTicket/${ticketId}`, this.httpOptions);
  }

  getAssignmentsBySupportEmployee(empId: string): Observable<TicketAssignment[]> {
    return this.http.get<TicketAssignment[]>(`${this.baseUrl}/BySupportEmployee/${empId}`, this.httpOptions);
  }
}
