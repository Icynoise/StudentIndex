import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { finalize, Observable, shareReplay, tap } from 'rxjs';
import { JwtHelperService } from '@auth0/angular-jwt';
import { LoginRequest, LoginResponse } from '../../features/login/login.model';
import { environment } from '../../../environments/environment';
import { StudentService } from './student.service';

const TOKEN_KEY = 'token';
const REFRESH_TOKEN_KEY = 'refreshToken';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private readonly apiUrl = `${environment.apiUrl}/auth`;

  /** Dijeljeni in-flight refresh — sprječava paralelne refresh pozive kod više 401 odjednom. */
  private refreshInFlight$: Observable<LoginResponse> | null = null;

  constructor(
    private http: HttpClient,
    private jwtHelper: JwtHelperService,
    private studentService: StudentService
  ) {}

  login(request: LoginRequest): Observable<LoginResponse> {
    return this.http.post<LoginResponse>(`${this.apiUrl}/login`, request);
  }

  setSession(response: LoginResponse): void {
    localStorage.setItem(TOKEN_KEY, response.token);
    localStorage.setItem(REFRESH_TOKEN_KEY, response.refreshToken);
  }

  getToken(): string | null {
    return localStorage.getItem(TOKEN_KEY);
  }

  getRefreshToken(): string | null {
    return localStorage.getItem(REFRESH_TOKEN_KEY);
  }

  /**
   * Obnavlja access token pomoću refresh tokena (rotacija — backend izdaje novi par).
   * Više istovremenih poziva dijeli isti HTTP zahtjev.
   */
  refreshSession(): Observable<LoginResponse> {
    if (!this.refreshInFlight$) {
      this.refreshInFlight$ = this.http
        .post<LoginResponse>(`${this.apiUrl}/refresh`, { refreshToken: this.getRefreshToken() })
        .pipe(
          tap(response => this.setSession(response)),
          finalize(() => (this.refreshInFlight$ = null)),
          shareReplay(1)
        );
    }
    return this.refreshInFlight$;
  }

  getRole(): string | null {
    const token = this.getToken();
    if (!token) return null;
    const decoded = this.jwtHelper.decodeToken(token);
    return decoded['http://schemas.microsoft.com/ws/2008/06/identity/claims/role'] ?? null;
  }

  isLoggedIn(): boolean {
    const token = this.getToken();
    return !!token && !this.jwtHelper.isTokenExpired(token);
  }

  logout(): void {
    const refreshToken = this.getRefreshToken();
    if (refreshToken) {
      // Opozovi refresh token na serveru; lokalno stanje se čisti odmah.
      this.http.post(`${this.apiUrl}/logout`, { refreshToken }).subscribe({ error: () => {} });
    }
    this.clearSession();
  }

  clearSession(): void {
    localStorage.removeItem(TOKEN_KEY);
    localStorage.removeItem(REFRESH_TOKEN_KEY);
    this.studentService.clearCache();
  }
}
