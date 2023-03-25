import { TestBed } from '@angular/core/testing';
import { DebtService } from './debt.service';

describe('MortgageService', () => {
  let service: DebtService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(DebtService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
