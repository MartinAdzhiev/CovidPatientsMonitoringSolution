export interface Patient {
    id: number;
    embg: string;
    name: string;
    measureType: string;
    minThreshold: number;
    maxThreshold: number,
    deviceId: number
  }