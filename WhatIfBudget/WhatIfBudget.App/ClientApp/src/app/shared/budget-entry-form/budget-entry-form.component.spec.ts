import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BudgetCreationFormComponent } from './budget-creation-form.component';

describe('BudgetCreationFormComponent', () => {
  let component: BudgetCreationFormComponent;
  let fixture: ComponentFixture<BudgetCreationFormComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ BudgetCreationFormComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(BudgetCreationFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
