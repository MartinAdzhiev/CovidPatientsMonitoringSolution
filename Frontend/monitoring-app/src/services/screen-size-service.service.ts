// screen-size.service.ts
import { Injectable, HostListener } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ScreenSizeService {
  private screenWidthSubject: BehaviorSubject<number> = new BehaviorSubject<number>(window.innerWidth);
  screenWidth$: Observable<number> = this.screenWidthSubject.asObservable();

  @HostListener('window:resize', ['$event'])
  onResize(event: Event): void {
    this.screenWidthSubject.next(window.innerWidth);
  }
}
