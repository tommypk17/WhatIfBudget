import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { DynamicDialogConfig, DynamicDialogRef } from 'primeng/dynamicdialog';
import { BudgetService } from '../../services/budget.service';
import { Budget } from '../models/budget';

@Component({
  selector: 'app-budget-entry-form',
  templateUrl: './budget-entry-form.component.html',
  styleUrls: ['./budget-entry-form.component.scss']
})
export class BudgetEntryFormComponent implements OnInit {

  model: Budget = new Budget();

  constructor(private budgetService: BudgetService, private ref: DynamicDialogRef, private config: DynamicDialogConfig ) { }

  ngOnInit(): void {
    if (this.config.data.budget) this.model = this.config.data.budget as Budget;
  }

  onSubmit(event: NgForm): void {
    if (this.model.id && this.model.id > 0) {
      this.budgetService.updateBudget(this.model).subscribe((res: Budget) => {
        this.ref.close();
      });
    } else {
      this.budgetService.saveBudget(event.value as Budget).subscribe((res: Budget) => {
        this.ref.close();
      });
    }
  }

}
