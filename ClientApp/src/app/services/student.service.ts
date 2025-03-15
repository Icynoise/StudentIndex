import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs/internal/Observable';

@Injectable({
  providedIn: 'root'
})
export class StudentService {

  private apiUrl = 'https://localhost:7185/api/students'; // Replace with your API URL

  constructor(private http: HttpClient) { }

  getStudents(): Observable<any[]> {
    return this.http.get<any[]>(this.apiUrl);
  }

  getStudentSubjects(studentId: number, yearId: number, semesterId: number): Observable<any[]> {
    return this.http.get<any[]>(
      `${this.apiUrl}/${studentId}/moji-predmeti?yearId=${yearId}&semesterId=${semesterId}`
    );
  }

}
