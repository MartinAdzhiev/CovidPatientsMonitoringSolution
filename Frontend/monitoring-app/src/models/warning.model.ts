import { PatientMeasureREsponse } from "./patientMeasureRespons.model";

export interface Warning {
    id: number;
    dateTime: string;
    value: number;
    currentMinThreshold: number;
    currentMaxThreshold: number;
    patientMeasureResponse: PatientMeasureREsponse;
  }