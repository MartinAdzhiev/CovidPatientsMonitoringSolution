import { Injectable } from '@angular/core';
import * as signalR from '@microsoft/signalr';
import { BehaviorSubject, Observable } from 'rxjs';
import { environment } from '../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class LivedataService {
  private liveDatReadingaHubConnection: signalR.HubConnection | undefined;
  private liveWarningDataHubConnection: signalR.HubConnection | undefined;
  private patienrReadingDataSubject = new BehaviorSubject<any>(null);

  constructor() {
    this.startLiveSensorDataConnection();
    this.startLiveWarningDataConnection();
  }

  private startLiveSensorDataConnection(): void {
    const url = `http://localhost:5155/liveDataReading`
    this.liveDatReadingaHubConnection = new signalR.HubConnectionBuilder()
      .withUrl(url)
      .configureLogging(signalR.LogLevel.Information)
      .build();

    this.liveDatReadingaHubConnection
      .start()
      .then(() => console.log('Connected to LiveSensorDataHub'))
      .catch((err) => console.error('Error connecting to LiveSensorDataHub:', err));
    
      this.liveDatReadingaHubConnection?.on('ReceivePatientReadingData', (data: any) => {
      this.patienrReadingDataSubject.next(data);
    });
  }

  private startLiveWarningDataConnection(): void {
    const url = `http://localhost:5155/liveWarning`
    this.liveWarningDataHubConnection = new signalR.HubConnectionBuilder()
      .withUrl(url)
      .configureLogging(signalR.LogLevel.Information)
      .build();

    this.liveWarningDataHubConnection
      .start()
      .then(() => console.log('Connected to WarningHub'))
      .catch((err) => console.error('Error connecting to WarningHub:', err));
  }

  stopConnections(): void {
    this.liveDatReadingaHubConnection?.stop().catch(err => console.error('Error stopping LiveSensorDataHub connection:', err));
    this.liveWarningDataHubConnection?.stop().catch(err => console.error('Error stopping WarningHub connection:', err));
  }

  getPatientReadingData(): Observable<any> {
    return this.patienrReadingDataSubject.asObservable();
  }

  getPatientReadingDataConnection(): signalR.HubConnection | undefined {
    return this.liveDatReadingaHubConnection;
  }

  getWarningDataConnection(): signalR.HubConnection | undefined {
    return this.liveWarningDataHubConnection;
  }
}