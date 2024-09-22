import { Patient } from "./patient.model";
import { PatientMeasureREsponse } from "./patientMeasureRespons.model";

export interface Device {
    id: number;
    name: string;
    patientMeasuresResponses: PatientMeasureREsponse[];
  }