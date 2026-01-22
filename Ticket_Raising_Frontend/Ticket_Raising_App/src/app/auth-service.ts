import { inject, Injectable, signal } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
 
 
@Injectable({
  providedIn: 'root',
})
 
export class AuthService {
  http: HttpClient = inject(HttpClient);
  baseUrl: string = "http://localhost:5175/api/Auth/";
  empNameSignal=signal<string|null>(null);
  empRoleSignal=signal<string|null>(null);
  empIdSignal=signal<string|null>(null);
  userName: string = "snraosir@mazenet.com";
  role: string = "Admin";
  secretKey: string = "My name is not important but my key is very important!";

  constructor(){
    this.empNameSignal.set(sessionStorage.getItem('username'));
    this.empRoleSignal.set(sessionStorage.getItem('role'));
    this.empIdSignal.set(sessionStorage.getItem('empId'));
  }
  getToken(): Observable<string> {
    return this.http.get(this.baseUrl + this.userName + "/" + this.role + "/" + this.secretKey, {
      responseType: "text"
    });
  }
  setLogin(empName: string, role: string, empId: string) {
    sessionStorage.setItem('username', empName);
    sessionStorage.setItem('role', role);
    sessionStorage.setItem('empId', empId);
    this.empNameSignal.set(empName);
    this.empRoleSignal.set(role);
    this.empIdSignal.set(empId);
  }
}

