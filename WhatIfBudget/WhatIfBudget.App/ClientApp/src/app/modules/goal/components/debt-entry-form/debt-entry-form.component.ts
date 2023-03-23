import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { NgForm } from '@angular/forms';
import { DebtService } from '../../../../services/debt.service';
import { SharedService } from '../../../../services/shared.service';
import { Debt } from '../../../../shared/models/debt';

@Component({
  selector: 'app-debt-entry-form',
  templateUrl: './debt-entry-form.component.html',
  styleUrls: ['./debt-entry-form.component.scss']
})
export class DebtEntryFormComponent implements OnInit {
  @Output('added') added: EventEmitter<void> = new EventEmitter();
  @Output('updated') updated: EventEmitter<void> = new EventEmitter();

  @Input('debt') model: Debt = new Debt();

  constructor(private debtService: DebtService, private sharedService: SharedService) { }

  ngOnInit(): void {
  }

  onSubmit(event: NgForm): void {
    let debt: Debt = event.value as Debt;
    debt.id = this.model.id;
    debt.goalId = this.sharedService.budget.debtGoalId;
    if (debt.id && debt.id > 0) {
      this.debtService.updateDebt(debt).subscribe((res: Debt) => {
        this.updated.emit();
        this.model = new Debt();
      });
    } else {
      this.debtService.saveDebt(debt).subscribe((res: Debt) => {
        this.added.emit();
        this.model = new Debt();
      });
    }
  }

}
