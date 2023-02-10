import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ExpenseDetailSectionComponent } from './expense-detail-section.component';

describe('ExpenseDetailSectionComponent', () => {
  let component: ExpenseDetailSectionComponent;
  let fixture: ComponentFixture<ExpenseDetailSectionComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ExpenseDetailSectionComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ExpenseDetailSectionComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
