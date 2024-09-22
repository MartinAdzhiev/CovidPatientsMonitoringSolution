export interface PatientMeasureREsponse {
    deviceId?: number;
    id: number;
    embg: string;
    name: string;
    measureType: string;
    minThreshold: number;
    maxThreshold: number;
    latestDate: string;
    latestTime: string;
    value: number;
  }