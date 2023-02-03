import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LoginBlockComponent } from './login-block.component';

describe('LoginBlockComponent', () => {
  let component: LoginBlockComponent;
  let fixture: ComponentFixture<LoginBlockComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ LoginBlockComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(LoginBlockComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
