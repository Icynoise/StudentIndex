import { Component } from '@angular/core';
import { CommonModule } from '@angular/common'; 
import { FormsModule } from '@angular/forms';
import { LoginRequest } from './login.model';
import { AuthService } from '../../core/services/auth.service';
import { Router } from '@angular/router';


@Component({
  selector: 'app-login',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent {
  email: string = '';
  password: string = '';
  error: string = '';

  constructor(private authService: AuthService, private router: Router) {}

  login() {
    this.error = ''; // Reset error
    const request: LoginRequest = {
      email: this.email,
      password: this.password
    };
    this.authService.login(request).subscribe({
      next: (response) => {
        this.authService.setToken(response.token);
        this.router.navigate(['/pocetna']);
      },
      error: (err) => {
        this.error = err.error?.message || 'Login failed. Please try again.';
        console.error('Login error:', err);
      }
    });
  }
}