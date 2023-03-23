import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { ConfirmationService, MessageService } from 'primeng/api';
import { DebtService } from '../../../../services/debt.service';
import { SharedService } from '../../../../services/shared.service';
import { Debt } from '../../../../shared/models/debt';

@Component({
  selector: 'app-debt-listing',
  templateUrl: './debt-listing.component.html',
  styleUrls: ['./debt-listing.component.scss']
})
export class DebtListingComponent implements OnInit {
  @Input('debts') model: Debt[] = [];

  @Output('deleted') deleted: EventEmitter<void> = new EventEmitter();
  @Output('updated') updated: EventEmitter<void> = new EventEmitter();

  editModal: boolean = false;
  editDebt: Debt | undefined;

  constructor(private confirmationService: ConfirmationService, private messageService: MessageService, private debtService: DebtService, private sharedService: SharedService) { }

  ngOnInit(): void {

  }

  delete(event: Event, debt: Debt) {
    this.confirmationService.confirm({
      target: event.target!,
      message: 'Delete this item?',
      accept: () => {
        this.debtService.deleteDebt(debt).subscribe((res: Debt) => {
          if (res) {
            this.refreshTable();
            this.deleted.emit();
          }
        });
      }
    });
  }

  edit(event: Event, debt: Debt) {
    this.editDebt = Object.assign(new Debt(), debt);
    this.editModal = true
  }

  editComplete(): void {
    this.editDebt = undefined;
    this.editModal = false;
    this.updated.emit();
    this.refreshTable();
  }

  refreshTable(): void {
    this.debtService.getDebtsByGoalId(this.sharedService.budget.debtGoalId ?? 0).subscribe((res: Debt[]) => {
      if (res) this.model = res;
    });
  }
}
