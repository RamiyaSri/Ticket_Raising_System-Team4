import { inject, Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { TicketComment } from '../Models/TicketComment';
 
@Injectable({ providedIn: 'root' })
export class TicketCommentService {
 
  private http = inject(HttpClient);
  private readonly baseUrl = 'http://localhost:5175/api/TicketComments/';
 
  private getAuthHeaders(): HttpHeaders {
    const token = sessionStorage.getItem('token') || '';
    return new HttpHeaders({
      'Authorization': `Bearer ${token}`,
      'Content-Type': 'application/json'
    });
  }
 
  getAllComments(): Observable<TicketComment[]> {
    return this.http.get<TicketComment[]>(
      this.baseUrl,
      { headers: this.getAuthHeaders() }
    );
  }
 
  getComment(commentId: string): Observable<TicketComment> {
    return this.http.get<TicketComment>(
      this.baseUrl + encodeURIComponent(commentId),
      { headers: this.getAuthHeaders() }
    );
  }
 
  addComment(comment: TicketComment): Observable<TicketComment> {
    return this.http.post<TicketComment>(
      this.baseUrl,
      comment,
      { headers: this.getAuthHeaders() }
    );
  }
 
  updateComment(commentId: string, comment: TicketComment): Observable<TicketComment> {
    return this.http.put<TicketComment>(
      this.baseUrl + encodeURIComponent(commentId),
      comment,
      { headers: this.getAuthHeaders() }
    );
  }
 
  deleteComment(commentId: string): Observable<any> {
    return this.http.delete<any>(
      this.baseUrl + encodeURIComponent(commentId),
      { headers: this.getAuthHeaders() }
    );
  }
 
  getCommentsByEmployee(empId: string): Observable<TicketComment[]> {
    return this.http.get<TicketComment[]>(
      this.baseUrl + 'employee/' + encodeURIComponent(empId),
      { headers: this.getAuthHeaders() }
    );
  }
 
  getCommentsBySupportEmployee(supportEmpId: string): Observable<TicketComment[]> {
    return this.http.get<TicketComment[]>(
      this.baseUrl + 'supportEmployee/' + encodeURIComponent(supportEmpId),
      { headers: this.getAuthHeaders() }
    );
  }
 
  getCommentsByTicket(ticketId: string): Observable<TicketComment[]> {
    return this.http.get<TicketComment[]>(
      this.baseUrl + 'ticket/' + encodeURIComponent(ticketId),
      { headers: this.getAuthHeaders() }
    );
  }
}