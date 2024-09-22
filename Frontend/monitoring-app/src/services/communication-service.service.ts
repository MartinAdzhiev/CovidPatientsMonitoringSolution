import { Injectable } from '@angular/core';
import { Subject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class CommunicationService {
  private fetchDataTrigger = new Subject<void>();

  fetchDataTriggered$ = this.fetchDataTrigger.asObservable();

  triggerFetchData() {
    this.fetchDataTrigger.next();
  }

}
