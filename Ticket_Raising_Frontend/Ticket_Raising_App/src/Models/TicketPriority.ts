export class TicketPriority {
    public priorityId: string;
    public priorityLevel: string;
    public priorityDescription: string;
    public responseTime: number;
    public resolutionTime: number;
 
    constructor(
        priorityId: string = "",
        priorityLevel: string = "",
        priorityDescription: string = "",
        responseTime: number = 0,
        resolutionTime: number = 0
    ) {
        this.priorityId = priorityId;
        this.priorityLevel = priorityLevel;
        this.priorityDescription = priorityDescription;
        this.responseTime = responseTime;
        this.resolutionTime = resolutionTime;
    }
}