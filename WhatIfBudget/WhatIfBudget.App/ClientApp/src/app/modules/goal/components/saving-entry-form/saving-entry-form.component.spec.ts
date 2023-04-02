import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SavingEntryFormComponent } from './saving-entry-form.component';

describe('SavingEntryFormComponent', () => {
  let component: SavingEntryFormComponent;
  let fixture: ComponentFixture<SavingEntryFormComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SavingEntryFormComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(SavingEntryFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
