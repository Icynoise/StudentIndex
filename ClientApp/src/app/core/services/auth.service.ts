import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { catchError, Observable, tap, throwError } from 'rxjs';
import { LoginRequest, LoginResponse } from '../../features/login/login.model';


@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private apiUrl = 'https://localhost:7185/api/auth'; // Base URL
  private loginEndpoint = `${this.apiUrl}/login`; // Full endpoint
  private token: string | null = null;

  constructor(private http: HttpClient) {}

  login(request: LoginRequest): Observable<LoginResponse> {
    return this.http.post<LoginResponse>(this.loginEndpoint, request);
  }

  setToken(token: string): void {
    this.token = token;
    localStorage.setItem('token', token); // Store in localStorage for persistence
  }

  getToken(): string | null {
    if (!this.token) {
      this.token = localStorage.getItem('token'); // Retrieve from localStorage if not in memory
    }
    return this.token;
  }

  getRole(): string | null {
    const token = this.getToken();
    if (token) {
      const decodedToken = this.jwtHelper.decodeToken(token);
      return decodedToken['http://schemas.microsoft.com/ws/2008/06/identity/claims/role'] || null;
    }
    return null;
  }

  isLoggedIn(): boolean {
    return !!this.getToken(); // True if token exists, false if null
  }

  logout(): void {
    this.token = null;
    localStorage.removeItem('token');
  }
}