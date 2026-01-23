import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { EmployeeService } from '../employee-service';
import { Employee } from '../../Models/Employee';
 
@Component({
  selector: 'app-register',
  imports: [CommonModule, FormsModule],
  templateUrl: './register-component.html',
  styleUrl: './register-component.css',
})
export class RegisterComponent {
 
  EmployeeSvc: EmployeeService = inject(EmployeeService);
  router: Router = inject(Router);
 
  employee: Employee;
  errMsg: string;
 
  constructor() {
    this.employee = new Employee();
    this.errMsg = "";
    this.employee.role = "User";
  }
 
  register() {
    this.EmployeeSvc.addEmployee(this.employee).subscribe({
      next: (response: any) => {
        alert("Registration successful");
        this.errMsg = "";
        this.router.navigate(['/login']);
      },
      error: (err) => {
        this.errMsg = err.error || err.message;
      }
    });
  }
 
  resetForm() {
    this.employee = new Employee();
    this.errMsg = "";
  }
}