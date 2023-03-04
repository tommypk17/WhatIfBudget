import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { ConfirmationService, MessageService } from 'primeng/api';
import { InvestmentService } from '../../../../services/investment.service';
import { SharedService } from '../../../../services/shared.service';
import { Investment } from '../../../../shared/models/investment';

@Component({
  selector: 'app-investment-listing',
  templateUrl: './investment-listing.component.html',
  styleUrls: ['./investment-listing.component.scss']
})
export class InvestmentListingComponent implements OnInit {
  @Input('investments') model: Investment[] = [];

  @Output('deleted') deleted: EventEmitter<void> = new EventEmitter();
  @Output('updated') updated: EventEmitter<void> = new EventEmitter();

  editModal: boolean = false;
  editInvestment: Investment | undefined;

  constructor(private confirmationService: ConfirmationService, private messageService: MessageService, private investmentService: InvestmentService, private sharedService: SharedService) { }

  ngOnInit(): void {

  }

  delete(event: Event, investment: Investment) {
    this.confirmationService.confirm({
      target: event.target!,
      message: 'Delete this item?',
      accept: () => {
        this.investmentService.deleteInvestment(investment).subscribe((res: Investment) => {
          if (res) {
            this.refreshTable();
            this.deleted.emit();
          }
        });
      }
    });
  }

  edit(event: Event, investment: Investment) {
    this.editInvestment = Object.assign(new Investment(), investment);
    this.editModal = true
  }

  editComplete(): void {
    this.editInvestment = undefined;
    this.editModal = false;
    this.updated.emit();
    this.refreshTable();
  }

  refreshTable(): void {
    this.investmentService.getInvestmentsByGoalId(this.sharedService.budget.investmentGoalId ?? 0).subscribe((res: Investment[]) => {
      if (res) this.model = res;
    });
  }

}
