import { inject, Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { TicketComment } from '../Models/TicketComment';
 
@Injectable({ providedIn: 'root' })
export class TicketCommentService {
 
  private http = inject(HttpClient);
  private readonly baseUrl = 'https://ticketportalapi-grcke7cnhccuc9fd.canadacentral-01.azurewebsites.net/';
 
  private getAuthHeaders(): HttpHeaders {
    const token = sessionStorage.getItem('token') || '';
    return new HttpHeaders({
      'Authorization': `Bearer ${token}`,
      'Content-Type': 'application/json'
    });
  }
 
  getAllComments(): Observable<TicketComment[]> {
    return this.http.get<TicketComment[]>(
      this.baseUrl + "api/TicketComments/",
      { headers: this.getAuthHeaders() }
    );
  }
 
  getComment(commentId: string): Observable<TicketComment> {
    return this.http.get<TicketComment>(
      this.baseUrl + "api/TicketComments/" + encodeURIComponent(commentId),
      { headers: this.getAuthHeaders() }
    );
  }
 
  addComment(comment: TicketComment): Observable<TicketComment> {
    return this.http.post<TicketComment>(
      this.baseUrl + "api/TicketComments/",
      comment,
      { headers: this.getAuthHeaders() }
    );
  }
 
  updateComment(commentId: string, comment: TicketComment): Observable<TicketComment> {
    return this.http.put<TicketComment>(
      this.baseUrl + "api/TicketComments/" + encodeURIComponent(commentId),
      comment,
      { headers: this.getAuthHeaders() }
    );
  }
 
  deleteComment(commentId: string): Observable<any> {
    return this.http.delete<any>(
      this.baseUrl + "api/TicketComments/" + encodeURIComponent(commentId),
      { headers: this.getAuthHeaders() }
    );
  }
 
  getCommentsByEmployee(empId: string): Observable<TicketComment[]> {
    return this.http.get<TicketComment[]>(
      this.baseUrl + 'GetComments/ByEmpId/' + encodeURIComponent(empId),
      { headers: this.getAuthHeaders() }
    );
  }
 
  getCommentsBySupportEmployee(supportEmpId: string): Observable<TicketComment[]> {
    return this.http.get<TicketComment[]>(
      this.baseUrl + 'GetComments/BySupportEmpId/' + encodeURIComponent(supportEmpId),
      { headers: this.getAuthHeaders() }
    );
  }
 
  getCommentsByTicket(ticketId: string): Observable<TicketComment[]> {
    return this.http.get<TicketComment[]>(
      this.baseUrl + "GetComments/ByTicketId/" + encodeURIComponent(ticketId),
      { headers: this.getAuthHeaders() }
    );
  }
}