import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BudgetEntryFormComponent } from './budget-entry-form.component';

describe('BudgetCreationFormComponent', () => {
  let component: BudgetEntryFormComponent;
  let fixture: ComponentFixture<BudgetEntryFormComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [BudgetEntryFormComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(BudgetEntryFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
