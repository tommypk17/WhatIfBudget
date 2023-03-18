import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SavingChartComponent } from './saving-chart.component';

describe('SavingChartComponent', () => {
  let component: SavingChartComponent;
  let fixture: ComponentFixture<SavingChartComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SavingChartComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(SavingChartComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
