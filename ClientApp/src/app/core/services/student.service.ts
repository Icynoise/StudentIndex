import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs/internal/Observable';
import { Student } from '../models/student.model';

@Injectable({
  providedIn: 'root'
})
export class StudentService {

  private apiUrl = 'https://localhost:7185/api'; // Replace with your API URL

  constructor(private http: HttpClient) { }

  // student-subjects.service.ts
  getStudentInfo(): Observable<Student> {
    return this.http.get<Student>(
      `${this.apiUrl}/Studenti/details`
    );
  }
}
