export class Ticket {
    public ticketId: string;
    public empId: string | null;
    public subject: string;
    public description: string;
    public status: string;
    public ticketTypeId: string;
    public creationDate: Date | null;
    public resolutionDate: Date | null;
 
    constructor(
        ticketId: string = "",
        empId: string = "",
        subject: string = "",
        description: string = "",
        status: string = "",
        ticketTypeId: string = "",
        creationDate: Date | null = null,
        resolutionDate: Date | null = null
    ) {
        this.ticketId = ticketId;
        this.empId = empId;
        this.subject = subject;
        this.description = description;
        this.status = status;
        this.ticketTypeId = ticketTypeId;
        this.creationDate = creationDate;
        this.resolutionDate = resolutionDate;
    }
}