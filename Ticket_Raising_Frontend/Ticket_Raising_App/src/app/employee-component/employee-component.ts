import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { EmployeeService } from '../employee-service';
import { Employee } from '../../Models/Employee';
 
@Component({
  selector: 'app-employee-component',
  imports: [CommonModule, FormsModule],
  templateUrl: './employee-component.html',
  styleUrl: './employee-component.css',
})
export class EmployeeComponent {
 
  EmployeeSvc: EmployeeService = inject(EmployeeService);
  userName = sessionStorage.getItem("username");
  EmpID = sessionStorage.getItem("empId");
  role =sessionStorage.getItem("role");
  employee: Employee;
  employees: Employee[];
  errMsg: string;
  ToEdit: boolean = false;
 
  constructor() {
    this.employees = [];
    this.errMsg = "";
    this.employee = new Employee();
    this.showAllEmployees();
  }

  UpdateUserDetails(){
    this.ToEdit = true;
    if (this.EmpID){
      this.EmployeeSvc.getEmployee(this.EmpID).subscribe({
        next: (response: any) => {
          this.employee = response;
          this.errMsg = "";
        },
        error: (err) => this.errMsg = err.error
      });
  }
  }

  cancelUpdateUserDetails(){
    this.ToEdit = false;
    this.newEmployee();
  }
 

  newEmployee() {
    this.employee = new Employee();
    this.errMsg = "";
  }
 

  showAllEmployees() {
    this.EmployeeSvc.getAllEmployees().subscribe({
      next: (response: any) => {
        if(this.role == "User"){
          this.employees = response.filter((emp: Employee) => emp.empId === this.EmpID);
        }
        else{
        this.employees = response;
        }
        this.errMsg = "";
      },
      error: (err) => this.errMsg = err.error
    });
  }
 
  showEmployee() {
    this.EmployeeSvc.getEmployee(this.employee.empId).subscribe({
      next: (response: any) => {
        this.employee = response;
        this.errMsg = "";
      },
      error: (err) => this.errMsg = err.error
    });
  }
 
  updateEmployee() {
    this.EmployeeSvc.updateEmployee(this.employee.empId, this.employee).subscribe({
      next: () => {
        alert("Employee details updated");
        this.errMsg = "";
        this.showAllEmployees();
        this.ToEdit = false;
      },
      error: (err) => this.errMsg = err.error
    });
  }
 

  deleteEmployee() {
    this.EmployeeSvc.deleteEmployee(this.employee.empId).subscribe({
      next: () => {
        alert("Employee deleted");
        this.errMsg = "";
        this.showAllEmployees();
        this.newEmployee();
      },
      error: (err) => this.errMsg = err.error
    });
  }
}