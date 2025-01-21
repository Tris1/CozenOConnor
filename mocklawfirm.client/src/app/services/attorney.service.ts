import { Injectable } from '@angular/core';
import { Attorney } from '../models/attorney';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AttorneyService {

  constructor(private http: HttpClient) { }

  public getAttorneys(): Observable<Attorney[]>{
    return this.http.get<Attorney[]>('api/Attorney/getAllAttorneys');
  }

  public addAttorney(attorney: Attorney): Observable<Attorney[]> {
    return this.http.post<Attorney[]>('api/Attorney/addAttorney', attorney);
  }

  public deleteAttorney(attorney: Attorney): Observable<Attorney[]> {
    return this.http.delete<Attorney[]>(`api/Attorney/removeAttorney/${attorney.id}`);
  }
}
