import { Component, Input, OnInit } from '@angular/core';
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
            this.messageService.add({ severity: 'success', summary: 'Investment Deleted' });
            this.refreshTable();
          }
          else this.messageService.add({ severity: 'error', summary: 'Investment Delete Failed' });
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

    this.refreshTable();
  }

  refreshTable(): void {
    this.investmentService.getInvestments().subscribe((res: Investment[]) => {
      if (res) this.model = res;
    });
  }

}
