import { ComponentFixture, TestBed } from '@angular/core/testing';


import { DeviceComponent } from './devices.component';
import { HttpClientModule } from '@angular/common/http';

describe('DeviceNavComponent', () => {
  let component: DeviceComponent;
  let fixture: ComponentFixture<DeviceComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [DeviceComponent, HttpClientModule]
    })
    .compileComponents();

    fixture = TestBed.createComponent(DeviceComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
