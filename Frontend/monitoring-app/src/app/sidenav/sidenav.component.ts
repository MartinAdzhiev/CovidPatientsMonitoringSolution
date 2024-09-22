import { CommonModule } from '@angular/common';
import { Component, Output, EventEmitter, Input } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatListModule } from '@angular/material/list';
import { MatSidenavContainer, MatSidenavContent, MatSidenavModule } from '@angular/material/sidenav';
import { MatToolbarModule } from '@angular/material/toolbar';
import { RouterLink, RouterLinkActive, RouterOutlet } from '@angular/router';

@Component({
  selector: 'app-sidenav',
  standalone: true,
  imports: [MatToolbarModule, MatIconModule, MatSidenavModule, MatSidenavContainer, MatListModule, MatSidenavContent, RouterOutlet, MatButtonModule, CommonModule, RouterLink, RouterLinkActive],
  templateUrl: './sidenav.component.html',
  styleUrl: './sidenav.component.css'
})
export class SidenavComponent {
  @Input() extendedNav = false;

  @Output() extendedNavChange = new EventEmitter<boolean>();

  extendNav() {
    if (this.extendedNav == false) {
      this.extendedNav = true;
    }
    else {
      this.extendedNav = false;
    }
    this.extendedNavChange.emit(this.extendedNav);
    console.log(this.extendedNav)
  }
  handleKeydown(event: KeyboardEvent) {
    // Handle keyboard interactions
    if (event.key === 'Enter' || event.key === ' ') {
      // Simulate click for Enter or Space key
      (event.target as HTMLElement).click();
    }
  }
  openLink(): void {
    window.open('https://initgroup.com/', '_blank');
  }
}
