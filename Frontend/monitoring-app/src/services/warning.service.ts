import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { environment } from '../environments/environment';
import { Observable } from 'rxjs';
import { Warning } from '../models/warning.model';

@Injectable({
  providedIn: 'root'
})
export class WarningSerivce {

  constructor(private http: HttpClient) { }

  getWarnings(): Observable<Warning[]>{
    let value = `${environment.apiUrl}/warning/all`
    return this.http.get<Warning[]>(value);
  }

  getSystemStatus(){
    let value = `${environment.apiUrl}/warning/systemStatus`;
    return this.http.get(value);
  }

  deleteWarning(warningId: number){
    let value = `${environment.apiUrl}/warning/${warningId}/delete`;
    return this.http.delete(value);
  }

}
