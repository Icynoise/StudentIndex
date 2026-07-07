import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { DostupniIspiti, IspitPrijava } from '../models/ispitPrijava.model';
import { environment } from '../../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class IspitiService {
  private readonly apiUrl = `${environment.apiUrl}/PrijavaIspita`;

  constructor(private http: HttpClient) {}

  getStudentInfo(): Observable<IspitPrijava> {
    return this.http.get<IspitPrijava>(`${this.apiUrl}/student-data`);
  }

  getAvailableExams(): Observable<DostupniIspiti[]> {
    return this.http.get<DostupniIspiti[]>(`${this.apiUrl}/available-exams`);
  }

  registerForExam(ispitId: number): Observable<{ message: string }> {
    return this.http.post<{ message: string }>(`${this.apiUrl}/register`, { ispitId });
  }
}
