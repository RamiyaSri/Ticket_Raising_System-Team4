import { Component, inject, signal } from '@angular/core';
import { RouterOutlet,RouterLink, RouterLinkActive } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { AuthService } from './auth-service';
 
@Component({
  selector: 'app-root',
  imports: [RouterOutlet, RouterLink, CommonModule, RouterLinkActive, FormsModule],
  templateUrl: './app.html',
  styleUrl: './app.css'
})
export class App {
  protected readonly title = signal('Ticket_Raising_App');
  AuthSvc: AuthService = inject(AuthService);
  
  username;
  role;
    constructor() {
        this.username=sessionStorage.getItem("username");
        this.role = sessionStorage.getItem("role");
        this.findout();
  }
  findout(){
    console.log("Username from app.ts: "+this.username);
    console.log("Role from app.ts: "+this.role);
    console.log(this.AuthSvc.empNameSignal,this.AuthSvc.empRoleSignal,this.AuthSvc.empIdSignal);
  }
 
}
