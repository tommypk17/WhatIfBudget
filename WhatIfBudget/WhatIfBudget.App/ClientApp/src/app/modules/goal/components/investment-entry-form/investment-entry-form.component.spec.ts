import { ComponentFixture, TestBed } from '@angular/core/testing';

import { InvestmentEntryFormComponent } from './investment-entry-form.component';

describe('InvestmentEntryFormComponent', () => {
  let component: InvestmentEntryFormComponent;
  let fixture: ComponentFixture<InvestmentEntryFormComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ InvestmentEntryFormComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(InvestmentEntryFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
