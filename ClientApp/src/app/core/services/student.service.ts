import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, shareReplay } from 'rxjs';
import { Student } from '../models/student.model';
import { environment } from '../../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class StudentService {
  private readonly apiUrl = `${environment.apiUrl}/Studenti`;
  private studentInfo$: Observable<Student> | null = null;

  constructor(private http: HttpClient) {}

  getStudentInfo(): Observable<Student> {
    if (!this.studentInfo$) {
      this.studentInfo$ = this.http.get<Student>(`${this.apiUrl}/details`).pipe(
        shareReplay(1)
      );
    }
    return this.studentInfo$;
  }

  clearCache(): void {
    this.studentInfo$ = null;
  }
}
