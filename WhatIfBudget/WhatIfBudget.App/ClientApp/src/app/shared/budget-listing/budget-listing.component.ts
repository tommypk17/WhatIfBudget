import { Component, OnInit } from '@angular/core';
import { DynamicDialogRef } from 'primeng/dynamicdialog';
import { BudgetService } from '../../services/budget.service';
import { Budget } from '../models/budget';

@Component({
  selector: 'app-budget-listing',
  templateUrl: './budget-listing.component.html',
  styleUrls: ['./budget-listing.component.scss']
})
export class BudgetListingComponent implements OnInit {
  private _budgets: Budget[] = [];
  selected: Budget | undefined;

  constructor(private budgetService: BudgetService, private ref: DynamicDialogRef) { }

  ngOnInit(): void {
    this.budgetService.getBudgets().subscribe((res: Budget[]) => {
      this._budgets = res;
    });
  }

  get budgets(): Budget[] {
    return this._budgets;
  }

  loadBudget(): void {
    this.ref.close(this.selected);
  }
}
