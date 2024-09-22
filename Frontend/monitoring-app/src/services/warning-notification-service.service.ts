import { Injectable } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { CommunicationService } from '../services/communication-service.service'; 
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class WarningNotificationService {

  constructor(
    private snackBar: MatSnackBar,
    private communicationService: CommunicationService,
    private router: Router
  ) {}

  showWarning(message: string, route: string): void {
    const snackBarRef = this.snackBar.open(message, 'Check Warnings', {
      duration: 5000
    });

    snackBarRef.onAction().subscribe(() => {
      this.router.navigate([route]);
    });

    this.communicationService.triggerFetchData();
  }
}
