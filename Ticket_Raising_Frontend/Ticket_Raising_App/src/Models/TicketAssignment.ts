export class TicketAssignment {
    public assignmentId: string;
    public ticketId: string;
    public supportEmpId: string;
    public assignmentDate: Date;
 
    constructor(
        assignmentId: string = "",
        ticketId: string = "",
        supportEmpId: string = "",
        assignmentDate: Date = new Date()
    ) {
        this.assignmentId = assignmentId;
        this.ticketId = ticketId;
        this.supportEmpId = supportEmpId;
        this.assignmentDate = assignmentDate;
    }
}