import { Component, inject } from '@angular/core';
import { TicketCommentService } from '../ticketcomment-service';
import { TicketComment } from '../../Models/TicketComment';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Employee } from '../../Models/Employee';
import { EmployeeService } from '../employee-service';
import { Ticket } from '../../Models/Ticket';
import { TicketService } from '../ticket-service';
 
@Component({
  selector: 'app-ticket-comment-component',
  imports: [CommonModule, FormsModule],
  templateUrl: './ticketcomment-component.html',
  styleUrl: './ticketcomment-component.css',
})
export class TicketCommentComponent {
 
  ticketCommentSvc: TicketCommentService=inject(TicketCommentService);
  employeeSvc: EmployeeService = inject(EmployeeService);
  ticketSVCC: TicketService = inject(TicketService);
  ticketComments: TicketComment[];
  tickets: Ticket[] = [];
  ticketComment: TicketComment;
  errMsg: string;
  userEmp: Employee[] | undefined;
  SupportEmp: Employee[] | undefined;
  empId: string;
  supportEmpId: string;
  ticketId: string;
 
  constructor() {
    this.ticketComments = [];
    this.ticketComment = new TicketComment("", "", "", "", "", new Date());
    this.errMsg = "";
    this.empId = "";
    this.supportEmpId = "";
    this.ticketId = "";
    //this.ticketComment.ticketId = ;
    
    

    this.ticketSVCC.getAllTickets().subscribe({
      next: (data) => {
        this.tickets = data.filter((t:any) => t.empId === sessionStorage.getItem("empId")) },
      error: err => this.errMsg = err.error
    });

    this.employeeSvc.getAllEmployees().subscribe({
          next: data => {this.userEmp = data.filter(e => e.role === "User")
            this.ticketComment.empId = sessionStorage.getItem("empId") || "";
          } ,
          error: err => this.errMsg = err.error
        });

    this.employeeSvc.getAllEmployees().subscribe({
          next: data => this.SupportEmp = data.filter(e => e.role === "Supporter") ,
          error: err => this.errMsg = err.error
        });

        
 
    this.showAllComments();
  }
 
  showAllComments() {
    this.ticketCommentSvc.getAllComments().subscribe({
      next: (response: any) => {
        this.ticketComments = response;
        console.log(response);
        this.errMsg = "";
      },
      error: (err) => {
        this.errMsg = err.message;
        console.log(err);
      }
    });
  }
 
  saveComment() {
    this.ticketCommentSvc.addComment(this.ticketComment).subscribe({
      next: (response: any) => {
        alert("New Comment added");
        this.errMsg = "";
        this.showAllComments();
      },
      error: (err) => this.errMsg = err.error
    });
  }
 
  newComment() {
    this.ticketComment = new TicketComment("", "", "", "", "", new Date());
  }
 
  showComment() {
    this.ticketCommentSvc.getComment(this.ticketComment.commentId).subscribe({
      next: (response: any) => {
        this.ticketComment = response;
        this.errMsg = "";
      },
      error: (err) => this.errMsg = err.error
    });
  }
 
  updateComment() {
    this.ticketCommentSvc.updateComment(this.ticketComment.commentId, this.ticketComment).subscribe({
      next: (response: any) => {
        alert("Comment updated successfully");
        this.errMsg = "";
        this.showAllComments();
      },
      error: (err) => this.errMsg = err.error
    });
  }
 
  deleteComment() {
    this.ticketCommentSvc.deleteComment(this.ticketComment.commentId).subscribe({
      next: (response: any) => {
        alert("Comment deleted successfully");
        this.errMsg = "";
        this.showAllComments();
      },
      error: (err) => this.errMsg = err.error
    });
  }
 
  showCommentsByEmployee() {
    this.ticketCommentSvc.getCommentsByEmployee(this.empId).subscribe({
      next: (response: any) => {
        this.ticketComments = response;
        this.errMsg = "";
      },
      error: (err) => this.errMsg = err.error
    });
  }
 
  showCommentsBySupportEmployee() {
    this.ticketCommentSvc.getCommentsBySupportEmployee(this.supportEmpId).subscribe({
      next: (response: any) => {
        this.ticketComments = response;
        this.errMsg = "";
      },
      error: (err) => this.errMsg = err.error
    });
  }
 
  showCommentsByTicket() {
    this.ticketCommentSvc.getCommentsByTicket(this.ticketId).subscribe({
      next: (response: any) => {
        this.ticketComments = response;
        this.errMsg = "";
      },
      error: (err) => this.errMsg = err.error
    });
  }
}