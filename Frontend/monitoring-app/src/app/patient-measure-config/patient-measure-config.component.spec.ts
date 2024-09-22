import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PatientMeasureConfigComponent } from './patient-measure-config.component';

describe('PatientMeasureConfigComponent', () => {
  let component: PatientMeasureConfigComponent;
  let fixture: ComponentFixture<PatientMeasureConfigComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [PatientMeasureConfigComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(PatientMeasureConfigComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
