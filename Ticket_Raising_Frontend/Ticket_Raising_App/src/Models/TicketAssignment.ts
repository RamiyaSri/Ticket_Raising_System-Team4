export class TicketAssignment {
    public assignmentId: string;
    public ticketId: string;
    public Support_Emp_Id: string;
    public assignmentDate: Date;
 
    constructor(
        assignmentId: string = "",
        ticketId: string = "",
        Support_Emp_Id: string = "",
        assignmentDate: Date = new Date()
    ) {
        this.assignmentId = assignmentId;
        this.ticketId = ticketId;
        this.Support_Emp_Id = Support_Emp_Id;
        this.assignmentDate = assignmentDate;
    }
}