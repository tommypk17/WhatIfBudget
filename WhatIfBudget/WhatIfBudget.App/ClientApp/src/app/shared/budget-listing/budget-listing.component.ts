import { Component, OnInit } from '@angular/core';
import { DialogService, DynamicDialogRef } from 'primeng/dynamicdialog';
import { BudgetService } from '../../services/budget.service';
import { SharedService } from '../../services/shared.service';
import { BudgetEntryFormComponent } from '../budget-entry-form/budget-entry-form.component';
import { Budget } from '../models/budget';

@Component({
  selector: 'app-budget-listing',
  templateUrl: './budget-listing.component.html',
  styleUrls: ['./budget-listing.component.scss']
})
export class BudgetListingComponent implements OnInit {
  private _budgets: Budget[] = [];
  selected: Budget | undefined;

  constructor(private sharedService: SharedService, private budgetService: BudgetService, private ref: DynamicDialogRef, private dialogService: DialogService) { }

  ngOnInit(): void {
    this.budgetService.getBudgets().subscribe((res: Budget[]) => {
      this._budgets = res;
    });
  }

  get budgets(): Budget[] {
    return this._budgets;
  }

  loadBudget(): void {
    if (this.selected) {
      this.sharedService.budget = this.selected;
      this.ref.close(this.selected);
    }
  }

  editBudget(): void {
    const ref = this.dialogService.open(BudgetEntryFormComponent, {
      header: 'Edit Budget',
      data: {budget: this.selected}
    });
    ref.onClose.subscribe(() => {
      this.budgetService.getBudgets().subscribe((res: Budget[]) => {
        this._budgets = res;
      });
    });
  }
}
