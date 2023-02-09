import { ComponentFixture, TestBed } from '@angular/core/testing';

import { IncomeEntryFormComponent } from './income-entry-form.component';

describe('IncomeEntryFormComponent', () => {
  let component: IncomeEntryFormComponent;
  let fixture: ComponentFixture<IncomeEntryFormComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ IncomeEntryFormComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(IncomeEntryFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
