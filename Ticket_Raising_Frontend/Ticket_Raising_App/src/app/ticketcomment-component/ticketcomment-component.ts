import { Component, inject } from '@angular/core';
import { TicketCommentService } from '../ticketcomment-service';
import { TicketComment } from '../../Models/TicketComment';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Employee } from '../../Models/Employee';
import { EmployeeService } from '../employee-service';
import { Ticket } from '../../Models/Ticket';
import { TicketService } from '../ticket-service';
import { TicketAssignment } from '../../Models/TicketAssignment';
import { TicketAssignmentService } from '../ticketassignment-service';
import { tick } from '@angular/core/testing';
 
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
  assignmentSvc: TicketAssignmentService = inject(TicketAssignmentService);

  myticketComments: TicketComment[];
  assignedComments: TicketComment[];
  assignedEmployee: string; 
  assignedTickets: TicketAssignment[];
  ticketComments: TicketComment[];
  tickets: Ticket[] = [];
  replyComment: TicketComment;
  ticketComment: TicketComment;
  errMsg: string;
  userEmp: Employee[] | undefined;
  SupportEmp: Employee[] | undefined;
  empId: string;
  supportEmpId: string;
  curUser: string  | null;
  ticketId: string;
  role: string;
  emo: TicketComment;
  showTicketForm: boolean = false;
  showReplyForm: boolean = false;
 
  constructor() {
    this.myticketComments = [];
    this.assignedComments = [];
    this.ticketComments = [];
    this.assignedTickets = [];
    
    this.assignedEmployee = "";
    this.replyComment = new TicketComment("", "", "", "", "", new Date());
    this.ticketComment = new TicketComment("", "", "", "", "", new Date());
    this.errMsg = "";
    this.curUser = sessionStorage.getItem("empId");
    this.supportEmpId = "";
    this.empId = "";
    this.ticketId = "";
    this.role = "";
    this.emo = new TicketComment("", "", "", "", "", new Date());
    //this.ticketComment.ticketId = ;
    
    
    this.assignmentSvc.getAllAssignments().subscribe({
      next: (res: any)=> {
        this.assignedTickets = res.filter((t: TicketAssignment) => t.Support_Emp_Id == this.curUser);
        //console.log(this.assignedTickets);
        
      }
    })

    this.ticketSVCC.getAllTickets().subscribe({
      next: (data) => {
        this.tickets = data.filter((t:any) => t.empId === sessionStorage.getItem("empId")) },
      error: err => this.errMsg = err.error
    });

    this.employeeSvc.getAllEmployees().subscribe({
          next: data => {
            this.ticketComment.empId = sessionStorage.getItem("empId") || "";
            this.userEmp = data.filter(e => e.role === "User" && e.empId != this.ticketComment.empId);
          } ,
          error: err => this.errMsg = err.error
        });

    this.employeeSvc.getAllEmployees().subscribe({
          next: data => this.SupportEmp = data.filter(e => e.role === "Supporter") ,
          error: err => this.errMsg = err.error
        });

    this.showAllComments();
  }


  OnShowAddComment() {
    this.showTicketForm = true;
  }
  OnCancelAddComment() {
    this.showTicketForm = false;
  }

  OnShowReplyComment() {
    this.showReplyForm = true;
  }
  OnCancelReplyComment() {
    this.showReplyForm = false;
  }

  onChange(empId: string,flag: boolean) {
    // if(!flag){
      this.assignmentSvc.getAssignmentsByTicket(empId).subscribe({
      next: (res: any) => {
        if (!flag){
        this.ticketComment.support_Emp_Id = res.length > 0 ? res[0].Support_Emp_Id : null;
      }
      console.log("SUpportUser Id",res,this.supportEmpId);
      },
      error: (err: any) => {alert(err.error); this.newComment();}
      });

      console.log(this.ticketComment);
      this.ticketSVCC.getTicket(this.replyComment.ticketId).subscribe({
        next: (res: Ticket) => {this.replyComment.support_Emp_Id = res.empId || '';
          console.log("replyComment",this.replyComment);
        },
        error: (err: any) => {alert(err.err); this.newComment}
      });
  }

  showAllComments() {
    this.ticketCommentSvc.getAllComments().subscribe({
      next: (response: any) => {
        this.myticketComments = response.filter((tc: TicketComment) => tc.empId == this.curUser|| tc.support_Emp_Id == this.curUser);
          console.log(this.myticketComments);
          
        this.errMsg = "";
      },
      error: (err) => {
        this.errMsg = err.message;
        console.log(err);
      }
    });
  }
  
  replyForAComment() {
    this.replyComment.empId = this.curUser || '';
    this.ticketCommentSvc.addComment(this.replyComment).subscribe({
      next: (res: any) => {
        alert("New Comment Added");
         this.errMsg = "";
         this.showAllComments();
         this.replyComment =  new TicketComment("","","","","",new Date());
      },
      error: (err) => {this.errMsg = err.err; alert("Can't duplicate Comment Id");}
    })

    
  }

  saveComment() {

    console.log(this.ticketComment);
    this.ticketComment.empId = this.curUser || '';

    this.ticketCommentSvc.addComment(this.ticketComment).subscribe({
      next: (response: any) => {
        alert("New Comment added");
        this.errMsg = "";
        this.showAllComments();
        this.newComment();
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
 
  updateComment(comments: TicketComment,Id: string) {
    this.ticketComment.empId = this.curUser || '' ;
    this.ticketCommentSvc.updateComment(Id, comments).subscribe({
      next: (response: any) => {
        alert("Comment updated successfully");
        this.errMsg = "";
        this.showAllComments();
      },
      error: (err) => this.errMsg = err.error
    });
  }
 
  deleteComment(Id : string) {
    this.ticketCommentSvc.deleteComment(Id).subscribe({
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
    this.ticketCommentSvc.getCommentsByTicket(this.ticketComment.ticketId).subscribe({
      next: (response: any) => {
        this.myticketComments = response;
        if (this.ticketId)
        this.errMsg = "";
      },
      error: (err) => this.errMsg = err.error
    });
  }
}
