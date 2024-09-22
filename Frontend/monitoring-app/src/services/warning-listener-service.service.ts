import { Injectable, OnDestroy } from '@angular/core';
import { LivedataService } from './livedata.service';
import { WarningNotificationService } from './warning-notification-service.service';
import { PatientMeasureService } from './patient-measure.service';
import { Patient } from '../models/patient.model';
import { CommunicationService } from '../services/communication-service.service'; 

@Injectable({
  providedIn: 'root'
})
export class WarningListenerService implements OnDestroy {

  constructor(
    private liveDataService: LivedataService,
    private warningNotificationService: WarningNotificationService,
    private patientService: PatientMeasureService,
    private communicationService: CommunicationService
  ) {
    this.listenForWarnings();
  }

  private listenForWarnings(): void {
    const warningConnection = this.liveDataService.getWarningDataConnection();
    warningConnection?.on('ReceiveWarningData', (data: any) => {
      console.log('Received warning data:', data);
      this.communicationService.triggerFetchData();
      if (data) {
        this.patientService.getPatientById(data.patientMeasureId).subscribe({
          next: (patient: Patient) => {
            let message = 'Immediately check your patient: ' + patient.name;
            let route = `/warnings`;
            this.warningNotificationService.showWarning(
              message,
              route
            );
          },
          error: (error) => {
            console.error('Error fetching warning data:', error);
          }
        });
      }
    });
  }

  ngOnDestroy(): void {
    this.liveDataService.stopConnections();
  }
}
