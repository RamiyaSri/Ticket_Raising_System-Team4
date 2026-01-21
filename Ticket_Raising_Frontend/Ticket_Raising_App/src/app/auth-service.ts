import { inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
 
 
@Injectable({
  providedIn: 'root',
})
 
export class AuthService {
  http: HttpClient = inject(HttpClient);
  baseUrl: string = "http://localhost:5175/api/Auth/";
  userName: string = "snraosir@mazenet.com";
  role: string = "Admin";
  secretKey: string = "My name is not important but my key is very important!";
 
  getToken(): Observable<string> {
    return this.http.get(this.baseUrl + this.userName + "/" + this.role + "/" + this.secretKey, {
      responseType: "text"
    });
  }
}