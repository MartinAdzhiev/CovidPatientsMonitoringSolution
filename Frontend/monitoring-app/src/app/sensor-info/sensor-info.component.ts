import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { CommonModule } from '@angular/common';
import { PatientMeasureService } from '../../services/patient-measure.service';
import { Device } from '../../models/device.model';
import { DeviceService } from '../../services/device.service';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { FormControl, FormGroup, ReactiveFormsModule } from '@angular/forms';
import {  MatSelectModule, MatSelect } from '@angular/material/select';
import { Measure } from '../../models/measure.model';
import { catchError, of, Subscription } from 'rxjs';
import { DataReadingService } from '../../services/data-reading.service';
import { LivedataService } from '../../services/livedata.service';
import { Patient } from '../../models/patient.model';
import { PatientMeasureREsponse } from '../../models/patientMeasureRespons.model';
import { DataReading } from '../../models/dataReading.model';



@Component({
  selector: 'app-sensor-info',
  standalone: true,
  imports: [CommonModule, MatDatepickerModule,
    MatNativeDateModule,
    MatFormFieldModule,
    MatInputModule,
    ReactiveFormsModule, MatSelectModule],
  templateUrl: './sensor-info.component.html',
  styleUrls: ['./sensor-info.component.css']
})
export class SensorInfoComponent implements OnInit {
  
  patientId!: number;
  deviceId!: number;
  patient!: Patient;
  patientMeasureResponse!: PatientMeasureREsponse;
  device!: Device;
  dataReading!: DataReading[];
  lastTen!: DataReading[];
  isLoading: boolean = true;
  measure: Measure[] = [];  
  liveDataReading: PatientMeasureREsponse[] = [];

  liveData = true;
  monitorDataType = 0;
  iconBackgroundColor = "transparent";
  subscription: Subscription | undefined;
  counter: number = 10;


  dateRangeForm = new FormGroup({
    start: new FormControl(new Date()),
    end: new FormControl(new Date())
  });

  constructor(private route: ActivatedRoute, private router: Router, private patientService: PatientMeasureService, private deviceService: DeviceService, private dataReadingService: DataReadingService, private liveDataService: LivedataService) {}

  handleKeydown(event: KeyboardEvent) {
    if (event.key === 'Enter' || event.key === ' ') {
      (event.target as HTMLElement).click();
    }
  }
  
  ngOnInit(): void {
    this.isLoading = true;
    this.route.paramMap.subscribe(params => {
      this.deviceId = +params.get('deviceId')!;
      this.patientId = +params.get('patientId')!;
      console.log(this.patientId);
      this.isLoading = true;
    });

    this.patientMeasureResponse = {
      id: 0,
      embg: '',
      name: '',
      measureType: '',
      minThreshold: 0,
      maxThreshold: 0,
      latestDate: '',
      latestTime: '',
      value: 0
    };
  
    this.patientService.getPatientById(this.patientId).subscribe((value: Patient) => {
      this.patient = value;
      console.log(this.patient);
      this.isLoading = false;
    });
  
    this.deviceService.getDeviceId(this.deviceId).subscribe((value: Device) => {
      this.device = value;
    });

    this.dataReadingService.getLastTen(this.patientId).subscribe(data => {
      this.lastTen = data;
    })
  
    this.subscription = this.liveDataService.getPatientReadingData().subscribe(patientReadingData => {
      if (patientReadingData) {
        console.log(patientReadingData);
        if (patientReadingData.patientMeasureId === this.patientId) {
          const newReading: PatientMeasureREsponse = {
            id: patientReadingData.patientMeasureId,
            embg: patientReadingData.embg,
            name: patientReadingData.name,
            measureType: patientReadingData.measureType,
            minThreshold: patientReadingData.minThreshold,
            maxThreshold: patientReadingData.maxThreshold,
            latestDate: new Date(patientReadingData.time).toISOString().split('T')[0], 
            latestTime: new Date(patientReadingData.time).toTimeString().split(' ')[0],
            value: patientReadingData.value
          };
          if (this.counter > 0)
            {
              this.lastTen.pop();
              --this.counter; 
            }
          this.liveDataReading.unshift(newReading);
          //console.log(this.liveDataReading);
        }
      }
    });

    // this.measureService.getMeasures().subscribe((measures: Measure[]) => {
    //   this.measure = measures.filter(m => m.sensorId === this.patientId);
    //   this.measureData = this.measure.slice().sort((a, b) => new Date(b.dateTime).getTime() - new Date(a.dateTime).getTime());
    // });

    this.setIntervalClick({ value: 1440 } as MatSelect);
    
  }
  

  ngOnDestroy(): void {
    if (this.subscription) {
      this.subscription.unsubscribe();
    }
  }

  changeRealTimeData(): void {
    this.liveData = !this.liveData;
  }

  monitoringDataChange(type: number) {
    this.monitorDataType = type;
  }

  setIntervalClick(selectElement: MatSelect) {
    const selectedValue = selectElement.value;
    if (selectedValue !== undefined && selectedValue !== null) {

      const currentDate = new Date();
      const milliseconds = selectedValue * 60 * 1000;
      const startDate = new Date(currentDate.getTime() - milliseconds);
  
      const formattedStartDate = startDate.toISOString();
      console.log(formattedStartDate)

      this.dataReadingService.getFilterInterval(this.patientId, formattedStartDate).subscribe({
        next: (dataReadings: DataReading[]) => {
          console.log('Received DataReadings:', dataReadings);
          this.dataReading = dataReadings;
        },
        error:(error) => {
          console.error('Error receiving measures:', error);
          this.dataReading = [];
          return of([]); 
        }
      })
    }
  }

  setIntervalRangeClick() {
    const startDate = this.dateRangeForm.get('start')?.value;
    const endDate = this.dateRangeForm.get('end')?.value;
    if (startDate && endDate) {
      const start = new Date(startDate);
      const end = new Date(endDate);
  
      const startWithAddedHours = new Date(start.getTime() + 2 * 60 * 60 * 1000); 
      const endWithAddedHours = new Date(end.getTime() + 2 * 60 * 60 * 1000); 

      const formattedStartDate = this.formatDateForBackend(startWithAddedHours);
      const formattedEndDate = this.formatDateForBackend(endWithAddedHours);
  
      console.log('Selected Date Range with Added Hours:', formattedStartDate, formattedEndDate);

      this.dataReadingService.getFilterRange(this.patientId, formattedStartDate, formattedEndDate).pipe(
        catchError((error) => {
          console.error('Error receiving measures:', error);
          this.dataReading = [];
          return of([]); 
        })
      ).subscribe({
        next: (dataReadings: DataReading[]) => {
          console.log('Received DataReadings:', dataReadings);
          this.dataReading = dataReadings;
        }
      });
    }
  }

  formatDateForBackend(date: Date): string {
    return date.toISOString();
  }

  tempHumidityBackgroundUpdate(value: number, type: 'Temperature' | 'Humidity' | 'Pressure' | 'Battery' | 'Gas' | 'Ultrasound' | 'Accelerometer') {
    const thresholds = {
      Temperature: [
        { max: 0, color: '#ADE7F6' },
        { max: 20, color: '#8DDBEE' },
        { max: 40, color: '#F7D840' },
        { max: 60, color: '#F78F40' },
        { max: 80, color: '#FF3300' },
        { max: Infinity, color: '#DD2626' }
      ],
      Humidity: [
        { max: 20, color: '#D84E5B' },
        { max: 40, color: '#B4768D' },
        { max: 60, color: '#7EAACD' },
        { max: 80, color: '#4D6BDB' },
        { max: Infinity, color: '#1D3EBB' }
      ],
      Pressure: [
        { max: 350, color: '#86D9A5' },
        { max: 550, color: '#A8E384' },
        { max: 750, color: '#FDFC4A' },
        { max: 950, color: '#FD8825' },
        { max: Infinity, color: '#F60800' }
      ],
      Battery: [
        { max: 20, color: '#ED1B24' },
        { max: 40, color: '#FA9F1B' },
        { max: 60, color: '#78D966' },
        { max: 80, color: '#41C44E' },
        { max: Infinity, color: '#22962D' }
      ],
      Gas: [
        { max: 100, color: '#010098' },
        { max: 250, color: '#0102E4' },
        { max: 400, color: '#0267FF' },
        { max: 550, color: '#11FFE9' },
        { max: 700, color: '#FFEA08' },
        { max: 850, color: '#FB0000' },
        { max: Infinity, color: '#840302' }
      ],
      Ultrasound: [
        { max: 1, color: '#929292' },
        { max: 2, color: '#777676' },
        { max: 3, color: '#4A4A4A' },
        { max: 4, color: '#333232' },
        { max: Infinity, color: '#191919' }
      ],
      Accelerometer: [
        { max: -10, color: '#F49695' },
        { max: 0, color: '#F57271' },
        { max: 5, color: '#EE4F4E' },
        { max: 10, color: '#E22C2B' },
        { max: 15, color: '#CD0807' },
        { max: Infinity, color: '#B10100' }
      ]
    };
  
    const sensorThresholds = thresholds[type];
    if (sensorThresholds) {
      const match = sensorThresholds.find(threshold => value < threshold.max);
      if (match) {
        this.iconBackgroundColor = match.color;
      }
    }
  }
  

  navigateToDevices(){
    this.router.navigate(['/devices'])
  }

  navigateToConfiguration(deviceId: number, patientMeasureId: number){
    this.router.navigate(['/patientMeasure-config', deviceId, patientMeasureId]);
  }

}
 