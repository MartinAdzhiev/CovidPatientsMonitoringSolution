import { Component } from '@angular/core';
import { WarningSerivce } from '../../services/warning.service';
import { Observable } from 'rxjs';
import { CommonModule } from '@angular/common';
import { TableModule } from 'primeng/table';
import { Warning } from '../../models/warning.model';
import { CommunicationService } from '../../services/communication-service.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-warnings',
  standalone: true,
  imports: [CommonModule, TableModule],
  templateUrl: './warnings.component.html',
  styleUrl: './warnings.component.css'
})
export class WarningsComponent {

warnings$: Observable<Warning[]> | undefined;

constructor(private warningService: WarningSerivce, private communicationService: CommunicationService, private router: Router) { }

ngOnInit(): void {
  this.getWarnings();
  this.communicationService.fetchDataTriggered$.subscribe(() => {
    this.getWarnings();
  });
}

getWarnings(): void {
  this.warnings$ = this.warningService.getWarnings();
}

constructWarningMessage(warning: Warning): string {
  const patientMeasureResponse = warning.patientMeasureResponse;
  const patientMeasureType = patientMeasureResponse.measureType;
  const patient = patientMeasureResponse.name;
  const embg = patientMeasureResponse.embg;
  const value = warning.value;
  const maxThreshold = warning.currentMaxThreshold;
  const minThreshold = warning.currentMinThreshold;


    if (value > maxThreshold) {
      return `${patientMeasureType} of ${patient} with embg[${embg}] is ${value} and is above maxThreshold which is ${maxThreshold}`;
    } else if (value < minThreshold) {
      return `${patientMeasureType} of ${patient} with embg[${embg}] is ${value} and is below minThreshold which is ${minThreshold}`;
    }
    return " ";
}

deleteWarning(warningId: number): void {
    this.warningService.deleteWarning(warningId).subscribe({
      next: () => {
        this.getWarnings();
      }
    });
}

navigateToWarning(warning: Warning): void {
  this.router.navigate(['/patient-info', warning.patientMeasureResponse.deviceId, warning.patientMeasureResponse.id]);
}
}
