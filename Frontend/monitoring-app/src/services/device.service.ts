import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Device } from '../models/device.model';
import { Observable } from 'rxjs';
import { environment } from '../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class DeviceService {



  constructor(private http: HttpClient) { }

  getDevices(): Observable<Device[]> {
    let value = `${environment.apiUrl}/device/all`
    return this.http.get<Device[]>(value);
  }

  getDeviceId(deviceId: number): Observable<Device>{
    let value = `${environment.apiUrl}/device/${deviceId}`;
    return this.http.get<Device>(value);
  }
}
