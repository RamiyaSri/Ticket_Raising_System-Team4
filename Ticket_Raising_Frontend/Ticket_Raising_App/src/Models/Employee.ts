export class Employee {
 
    public empId: string;

    public firstName: string;

    public lastName: string;

    public email: string;

    public password: string;

    public role: string;

    public phoneNumber: string;
 
    constructor(

        empId: string = "",

        firstName: string = "",

        lastName: string = "",

        email: string = "",

        password: string = "",

        role: string = "",

        phoneNumber: string = ""

    ) {

        this.empId = empId;

        this.firstName = firstName;

        this.lastName = lastName;

        this.email = email;

        this.password = password;

        this.role = role;

        this.phoneNumber = phoneNumber;

    }

}

 