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
  baseUrl: string = "https://ticketportalapi-grcke7cnhccuc9fd.canadacentral-01.azurewebsites.net/";

  getAllAssignments(): Observable<TicketAssignment[]> {
    return this.http.get<TicketAssignment[]>(this.baseUrl + "api/TicketAssignments/", this.httpOptions);
  }

  getAssignment(assignmentId: string): Observable<TicketAssignment> {
    return this.http.get<TicketAssignment>(`${this.baseUrl}api/TicketAssignments/${assignmentId}`, this.httpOptions);
  }

  addAssignment(assignment: TicketAssignment): Observable<TicketAssignment> {
    return this.http.post<TicketAssignment>(`${this.baseUrl}api/TicketAssignments/`, assignment, this.httpOptions);
  }

  updateAssignment(assignmentId: string, assignment: TicketAssignment): Observable<TicketAssignment> {
    return this.http.put<TicketAssignment>(`${this.baseUrl}api/TicketAssignments/${assignmentId}`, assignment, this.httpOptions);
  }

  deleteAssignment(assignmentId: string): Observable<any> {
    return this.http.delete(`${this.baseUrl}api/TicketAssignments/${assignmentId}`, this.httpOptions);
  }

  getAssignmentsByTicket(ticketId: string): Observable<TicketAssignment[]> {
    return this.http.get<TicketAssignment[]>(`${this.baseUrl}GetAssignments/ByTicketId/${ticketId}`, this.httpOptions);
  }

  getAssignmentsBySupportEmployee(empId: string): Observable<TicketAssignment[]> {
    return this.http.get<TicketAssignment[]>(`${this.baseUrl}GetAssignments/BySupportEmpId/${empId}`, this.httpOptions);
  }
}
