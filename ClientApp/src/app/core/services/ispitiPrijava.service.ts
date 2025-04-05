import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs/internal/Observable';
import { DostupniIspiti, IspitPrijava } from '../models/ispitPrijava.model';


@Injectable({
  providedIn: 'root'
})
export class IspitiService {

  private apiUrl = 'https://localhost:7185/api'; // Replace with your API URL

  constructor(private http: HttpClient) { }

  // student-subjects.service.ts
  getStudentInfo(): Observable<IspitPrijava> {
      return this.http.get<IspitPrijava>(
        `${this.apiUrl}/PrijavaIspita/student-data`
      );
    }

    getAvailableExams(): Observable<DostupniIspiti[]> {
      return this.http.get<DostupniIspiti[]>(
        `${this.apiUrl}/PrijavaIspita/available-exams`
      );
    }

    registerForExam(ispitId: number): Observable<any> {
      return this.http.post(`${this.apiUrl}/PrijavaIspita/register`, ispitId)
    }
}
