import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../environments/environment';
import { DataReading } from '../models/dataReading.model';

@Injectable({
  providedIn: 'root'
})
export class DataReadingService {

  constructor(private http: HttpClient) { }

  getFilterInterval(patientMeasureId: number, dateTime: string): Observable<DataReading[]>{
    let params = new HttpParams();

    params = params.set('patientMeasureId', patientMeasureId);
    params = params.set('dateTime', dateTime);

    let value = `${environment.apiUrl}/dataReading/filterDataReadingsAfterDate`;
    return this.http.get<DataReading[]>(value, { params });
  }

  getFilterRange(patientMeasureId: number, dateTimeFrom: string, dateTimeTo: string): Observable<DataReading[]>{
    let params = new HttpParams();

    params = params.set('patientMeasureId', patientMeasureId);
    params = params.set('dateTimeFrom', dateTimeFrom);
    params = params.set('dateTimeTo', dateTimeTo);

    let value = `${environment.apiUrl}/dataReading/filterMeasuresWithinRange`;
    return this.http.get<DataReading[]>(value, { params });
  }

  getLastTen(patientMeasureId: number): Observable<DataReading[]>{
    let value = `${environment.apiUrl}/dataReading/lastTen`;
    let params = new HttpParams();
    params = params.set('patientMeasureId', patientMeasureId);
    return this.http.get<DataReading[]>(value, {params});
  }

  getLatest(): Observable<DataReading[]>{
    let value = `${environment.apiUrl}/dataReading/latest`;
    return this.http.get<DataReading[]>(value);
  }
}
