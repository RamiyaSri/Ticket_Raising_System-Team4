export class TicketType {
    public ticketTypeId: string;
    public typeName: string;
    public description: string;
    public priorityId: string;
 
    constructor(
        ticketTypeId: string = "",
        typeName: string = "",
        description: string = "",
        priorityId: string = ""
    ) {
        this.ticketTypeId = ticketTypeId;
        this.typeName = typeName;
        this.description = description;
        this.priorityId = priorityId;
    }
}