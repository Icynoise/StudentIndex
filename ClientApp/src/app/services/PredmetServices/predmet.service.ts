import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs/internal/Observable';

@Injectable({
  providedIn: 'root'
})
export class PredmetService {

  private apiUrl = 'https://localhost:7185/api/'; // Replace with your API URL

  constructor(private http: HttpClient) { }

  getStudents(): Observable<any[]> {
    return this.http.get<any[]>(this.apiUrl);
  }

  // student-subjects.service.ts
  getStudentSubjects(studentId: number, yearId: number, semesterId: number): Observable<any[]> {
    studentId = 5, yearId = 2, semesterId = 1
    return this.http.get<any[]>(
      `${this.apiUrl}/students/${studentId}/subjects?yearId=${yearId}&semesterId=${semesterId}`
    );
  }
}
