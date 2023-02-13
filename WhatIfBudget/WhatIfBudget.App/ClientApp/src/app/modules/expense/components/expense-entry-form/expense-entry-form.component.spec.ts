import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ExpenseEntryFormComponent } from './expense-entry-form.component';

describe('ExpenseEntryFormComponent', () => {
  let component: ExpenseEntryFormComponent;
  let fixture: ComponentFixture<ExpenseEntryFormComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ExpenseEntryFormComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ExpenseEntryFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
