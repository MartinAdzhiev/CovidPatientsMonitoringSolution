import { ComponentFixture, TestBed } from '@angular/core/testing';
import { RouterTestingModule } from "@angular/router/testing";
import { SensorInfoComponent } from './sensor-info.component';
import { HttpClientModule} from '@angular/common/http';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import {NoopAnimationsModule} from "@angular/platform-browser/animations"

describe('SensorInfoComponent', () => {
  let component: SensorInfoComponent;
  let fixture: ComponentFixture<SensorInfoComponent>;
  let httpTestingController: HttpTestingController;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [SensorInfoComponent, RouterTestingModule, HttpClientModule,NoopAnimationsModule]
    })
    .compileComponents();

    fixture = TestBed.createComponent(SensorInfoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

});
