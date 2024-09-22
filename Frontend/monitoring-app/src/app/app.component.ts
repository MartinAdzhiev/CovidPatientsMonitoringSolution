import { Component, Input, ViewChild} from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { MatGridListModule } from '@angular/material/grid-list';
import { HeaderComponent } from './header/header.component';
import { SidenavComponent } from './sidenav/sidenav.component';
import { CommonModule } from '@angular/common';
import { WarningListenerService } from '../services/warning-listener-service.service';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, MatGridListModule, HeaderComponent, SidenavComponent, CommonModule],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {
  title = 'Angular Frontend Demo Application';

  extendedNav = false;

  constructor(private warningListenerService: WarningListenerService) {
    // WarningListenerService is initialized and listening for warnings
  }

  handleExtendedNavChange(value: boolean) {
    this.extendedNav = value;
  }
}
