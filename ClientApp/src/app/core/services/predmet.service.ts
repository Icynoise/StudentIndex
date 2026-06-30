import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs/internal/Observable';
import { environment } from '../../../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class PredmetService {

  private readonly apiUrl = `${environment.apiUrl}/Predmeti`;

  constructor(private http: HttpClient) { }

  // student-subjects.service.ts
  getStudentSubjects(semesterId: number): Observable<any[]> {
    return this.http.get<any[]>(
      `${this.apiUrl}/moji-predmeti?semesterId=${semesterId}`
    );
  }
}
