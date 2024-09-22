import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Patient } from '../models/patient.model';
import { Observable } from 'rxjs';
import { environment } from '../environments/environment';


@Injectable({
  providedIn: 'root'
})
export class PatientMeasureService {


  constructor(private http: HttpClient) { }

  getSensors(): Observable<Patient[]>{
    let value = `${environment.apiUrl}/patient/all`;
    return this.http.get<Patient[]>(value);
  }

  getPatientById(patientId: number): Observable<Patient>{
    let value = `${environment.apiUrl}/patient/${patientId}`;
    return this.http.get<Patient>(value);
  }

  updateThresholds(patientId: number, body: {minThreshold: number, maxThreshold: number}){
    let value = `${environment.apiUrl}/patient/${patientId}/updateThreshold`;
    return this.http.put(value, body);
  }

}
