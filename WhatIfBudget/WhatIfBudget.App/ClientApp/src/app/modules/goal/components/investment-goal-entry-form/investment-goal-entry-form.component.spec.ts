import { ComponentFixture, TestBed } from '@angular/core/testing';

import { InvestmentGoalEntryFormComponent } from './investment-goal-entry-form.component';

describe('InvestmentGoalEntryFormComponent', () => {
  let component: InvestmentGoalEntryFormComponent;
  let fixture: ComponentFixture<InvestmentGoalEntryFormComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ InvestmentGoalEntryFormComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(InvestmentGoalEntryFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
