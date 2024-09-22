import { Component } from '@angular/core';
import { PatientMeasureREsponse } from '../../models/patientMeasureRespons.model';
import { PatientMeasureService } from '../../services/patient-measure.service';
import { EventEmitter, Output } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Patient } from '../../models/patient.model';

@Component({
  selector: 'app-patient-measure-config',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './patient-measure-config.component.html',
  styleUrl: './patient-measure-config.component.css'
})
export class PatientMeasureConfigComponent {
  deviceId!: number;
  patientMeasureId!: number;
  selectedPatientMeasure!: Patient;
  selectedSensorData: any = { minThreshold: 0, maxThreshold: 0, room: '' };
  isNotSelected: boolean = true;
  isLoading: boolean = true;

  @Output() stateChanged = new EventEmitter<void>(); 

  constructor(private router: Router, private patientMeasureService: PatientMeasureService, private route: ActivatedRoute){}

  handleKeydown(event: KeyboardEvent) {
    if (event.key === 'Enter' || event.key === ' ') {
      (event.target as HTMLElement).click();
    }
  }

  ngOnInit() {
    this.isLoading = true;
    this.route.paramMap.subscribe(params => {
      this.deviceId = +params.get('deviceId')!;
      this.patientMeasureId = +params.get('patientMeasureId')!;
  
      if (this.patientMeasureId) {
        this.loadPatientMeasure(this.patientMeasureId);
        this.isNotSelected = false; 
      } else {
        this.isNotSelected = true;
      }
    });
  }

  loadPatientMeasure(patientMeasureId: number): void {
    this.patientMeasureService.getPatientById(patientMeasureId).subscribe(patient => {
      this.selectedPatientMeasure = patient;
      this.isNotSelected = false; 
      this.isLoading = false;
    });
  }
   
  navigateBack(){
    this.router.navigate(['/patient-info', this.deviceId, this.patientMeasureId])
  }
  
  updateThresholds(patientMeasureId: number, min_Threshold: number, max_Threshold: number){
    const body = {
      minThreshold: Number(min_Threshold.toFixed(1)),
      maxThreshold: Number(max_Threshold.toFixed(1))
    };
    this.patientMeasureService.updateThresholds(patientMeasureId, body).subscribe();
  }
}