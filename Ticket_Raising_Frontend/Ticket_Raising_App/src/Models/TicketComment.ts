export class TicketComment {
    public commentId: string;
    public ticketId: string;
    public empId: string;
    public support_Emp_Id?: string;
    public commentText: string;
    public commentDate: Date;
 
    constructor(
        commentId: string = "",
        ticketId: string = "",
        empId: string = "",
        support_Emp_Id: string = "",
        commentText: string = "",
        commentDate: Date = new Date()
    ) {
        this.commentId = commentId;
        this.ticketId = ticketId;
        this.empId = empId;
        this.support_Emp_Id = support_Emp_Id;
        this.commentText = commentText;
        this.commentDate = commentDate;
    }
}