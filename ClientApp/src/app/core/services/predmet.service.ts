import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs/internal/Observable';

@Injectable({
  providedIn: 'root'
})
export class PredmetService {

  private apiUrl = 'https://localhost:7185/api'; // Replace with your API URL

  constructor(private http: HttpClient) { }

  // student-subjects.service.ts
  getStudentSubjects(semesterId: number): Observable<any[]> {
    return this.http.get<any[]>(
      `${this.apiUrl}/Predmeti/moji-predmeti?&semesterId=${semesterId}`
    );
  }
}
