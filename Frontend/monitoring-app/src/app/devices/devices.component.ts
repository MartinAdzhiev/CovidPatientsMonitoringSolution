import { Component, OnInit } from '@angular/core';
import { TableModule, TableRowCollapseEvent, TableRowExpandEvent } from 'primeng/table';
import { TagModule } from 'primeng/tag';
import { RatingModule } from 'primeng/rating';
import { CommonModule } from '@angular/common';
import { ButtonModule } from 'primeng/button';
import { ToastModule } from 'primeng/toast';
import { FormsModule } from '@angular/forms';
import { MessageService } from 'primeng/api';
import { ScrollPanelModule } from 'primeng/scrollpanel';
import { DeviceService } from '../../services/device.service';
import { Router } from '@angular/router';
import { Subscription } from 'rxjs';
import { LivedataService } from '../../services/livedata.service';
import { Device } from '../../models/device.model';
import { DataReadingService } from '../../services/data-reading.service';
import { DataReading } from '../../models/dataReading.model';

@Component({
    selector: 'app-device-nav',
    templateUrl: './devices.component.html',
    styleUrl: './devices.component.css',
    standalone: true,
    imports: [TableModule, TagModule, ToastModule, RatingModule, ButtonModule, CommonModule, FormsModule, ScrollPanelModule],
    providers: [MessageService]
})

export class DeviceComponent implements OnInit{
    data!: Device[];

    isLoading: boolean = true;
    expandedRows: { [key: number]: boolean } = {};
    subscription: Subscription | undefined;
    

    constructor( private messageService: MessageService, private deviceService: DeviceService, private router: Router, private liveDataService: LivedataService, private dataReadingService: DataReadingService) {}

    ngOnInit() {
      this.deviceService.getDevices().subscribe((value: Device[]) => {
        this.data = value
        this.dataReadingService.getLatest().subscribe((readings: DataReading[]) => {
          this.data.forEach(device => {
            device.patientMeasuresResponses.forEach(patientMeasureResponse => {
              readings.forEach(reading => {
                console.log(reading);
                if(patientMeasureResponse.id === reading.patientMeasureId) {
                  const dateObj = new Date(reading.dateTime);
                  patientMeasureResponse.latestDate = dateObj.toISOString().split('T')[0];
                  patientMeasureResponse.latestTime = dateObj.toTimeString().split(' ')[0];
                  patientMeasureResponse.value = reading.value;
                }
              })
            })
          })
        })
        this.isLoading = false;
      });

      
      
      this.subscription = this.liveDataService.getPatientReadingData().subscribe(patientReadingData => {
        if (patientReadingData) {
          console.log(patientReadingData)
          this.data.forEach(device => {
            device.patientMeasuresResponses.forEach(patientMeasureResponse => {
              if(patientMeasureResponse.id === patientReadingData.patientMeasureId){
                const dateObj = new Date(patientReadingData.time);
                patientMeasureResponse.latestDate = dateObj.toISOString().split('T')[0];
                patientMeasureResponse.latestTime = dateObj.toTimeString().split(' ')[0];
                patientMeasureResponse.value = patientReadingData.value;
              }
            });
        });
      }
      });


    }

    expandAll() {
      this.expandedRows = this.data.reduce((acc, p) => {
        acc[p.id] = true;
        return acc;
      }, {} as { [key: number]: boolean }); 
    }
  

  
  handleKeydown(event: KeyboardEvent) {
    if (event.key === 'Enter' || event.key === ' ') {
      (event.target as HTMLElement).click();
    }
  }
  
    collapseAll() {
        this.expandedRows = {};
    }
  
  onRowExpand(event: TableRowExpandEvent) {
    this.messageService.add({ severity: 'info', summary: 'Product Expanded', detail: event.data.name, life: 3000 });
  }

  onRowCollapse(event: TableRowCollapseEvent) {
    this.messageService.add({ severity: 'success', summary: 'Product Collapsed', detail: event.data.name, life: 3000 });
  }

  onRowClick(deviceId: number, patientId: number): void {
    this.router.navigate(['/patient-info', deviceId, patientId]);
  }
}

