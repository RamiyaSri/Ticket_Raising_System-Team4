import { inject, Injectable } from '@angular/core';

import { HttpClient, HttpHeaders } from '@angular/common/http';

import { Observable } from 'rxjs';

import { Employee } from '../Models/Employee';
 
@Injectable({

  providedIn: 'root',

})

export class EmployeeService {
 
  http: HttpClient = inject(HttpClient);

  httpOptions: { headers: HttpHeaders };

  token: string | null;

  baseUrl: string = 'http://localhost:5175/api/Employees/'; 

  // change port if needed
 
  constructor() {

    this.token = sessionStorage.getItem('token');

    this.httpOptions = {

      headers: new HttpHeaders({

        'Authorization': 'Bearer ' + this.token

      })

    };

  }

  getAllEmployees(): Observable<Employee[]> {

    return this.http.get<Employee[]>(this.baseUrl, this.httpOptions);

  } 

  addEmployee(employee: Employee): Observable<Employee> {

    return this.http.post<Employee>(this.baseUrl, employee, this.httpOptions);

  }
 

  getEmployee(empId: string): Observable<Employee> {

    return this.http.get<Employee>(this.baseUrl + empId, this.httpOptions);

  }
 

  updateEmployee(empId: string, employee: Employee): Observable<Employee> {

    return this.http.put<Employee>(this.baseUrl + empId, employee, this.httpOptions);

  }
 
  deleteEmployee(empId: string): Observable<any> {

    return this.http.delete(this.baseUrl + empId, this.httpOptions);

  }
 

  login(empId: string, password: string): Observable<Employee> {

    const headers = new HttpHeaders({

      'Content-Type': 'application/json'

    });
 
    return this.http.post<Employee>(

      this.baseUrl + 'login?empId=' + empId + '&password=' + password,

      {},

      { headers }

    );

  }

}

 