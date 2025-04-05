import { Component } from '@angular/core';
import { Router, RouterOutlet, NavigationEnd } from '@angular/router';
import { NavMenuComponent } from './shared/components/nav-menu/nav-menu.component';
import { AuthService } from './core/services/auth.service';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, NavMenuComponent, CommonModule],
  template: `
    <div class="app-container">
      <app-nav-menu *ngIf="shouldShowNav()"></app-nav-menu>
      <div class="content-container">
        <router-outlet></router-outlet>
      </div>
    </div>
  `,
  styleUrls: ['./app.component.scss'] // Use external CSS file
})
export class AppComponent {
  constructor(private authService: AuthService, private router: Router) {
    router.events.subscribe(event => {
      if (event instanceof NavigationEnd) {
      }
    });
  }

  shouldShowNav(): boolean {
    const currentUrl = this.router.url;
    return this.authService.isLoggedIn() && currentUrl !== '/login';
  }
}