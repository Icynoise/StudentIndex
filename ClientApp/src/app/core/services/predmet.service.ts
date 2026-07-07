import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs/internal/Observable';
import { environment } from '../../../environments/environment';
import { Predmet } from '../models/predmet.model';
import { QueryOptions, buildQueryParams } from '../models/query-options.model';

@Injectable({
  providedIn: 'root'
})
export class PredmetService {

  private readonly apiUrl = `${environment.apiUrl}/Predmeti`;

  constructor(private http: HttpClient) { }

  getStudentSubjects(semesterId: number, options?: QueryOptions): Observable<Predmet[]> {
    const params = buildQueryParams(options, new HttpParams().set('semesterId', semesterId));
    return this.http.get<Predmet[]>(`${this.apiUrl}/moji-predmeti`, { params });
  }
}
