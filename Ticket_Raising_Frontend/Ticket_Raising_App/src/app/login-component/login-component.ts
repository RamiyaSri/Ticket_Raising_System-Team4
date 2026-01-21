import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { EmployeeService } from '../employee-service';
import { Employee } from '../../Models/Employee';
import { AuthService } from '../auth-service';
 
@Component({
  selector: 'app-login',
  imports: [CommonModule, FormsModule],
  templateUrl: './login-component.html',
  styleUrl: './login-component.css',
})
export class LoginComponent {
 
  EmployeeSvc: EmployeeService = inject(EmployeeService);
  router: Router = inject(Router);
 
  empId: string = "";
  password: string = "";
  errMsg: string = "";
 
  login() {
    this.EmployeeSvc.login(this.empId, this.password).subscribe({
      next: (employee: Employee) => {
        alert("Login successful");
        sessionStorage.setItem("username", employee.firstName);
        sessionStorage.setItem("empId", employee.empId);
        sessionStorage.setItem("role", employee.role);
 
        this.errMsg = "";
        this.router.navigate(['/']);   
      },
      error: (err) => {
        this.errMsg = err.error || "Invalid login credentials";
      }
    });
  }
}