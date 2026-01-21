import { Component, inject } from '@angular/core';
import { Router } from '@angular/router';
 
@Component({
  selector: 'app-logout-component',
  imports: [],
  templateUrl: './logout-component.html',
  styleUrl: './logout-component.css',
})
export class LogoutComponent {
  router: Router = inject(Router);
  constructor() {
    sessionStorage.clear();
  }
}
 