import { Routes } from '@angular/router';
import { DeviceComponent } from './devices/devices.component';
import { SensorInfoComponent } from './sensor-info/sensor-info.component'; 
import { WarningsComponent } from './warnings/warnings.component';
import { PatientMeasureConfigComponent } from './patient-measure-config/patient-measure-config.component';



export const routes: Routes = [
    {path: '', redirectTo: '/devices', pathMatch: 'full'}, 
    {path: 'devices', component: DeviceComponent},
    {path: 'patient-info/:deviceId/:patientId', component: SensorInfoComponent },
    {path: 'warnings', component: WarningsComponent},
    {path: 'patientMeasure-config/:deviceId/:patientMeasureId', component: PatientMeasureConfigComponent }
];