import { Component, Input, OnInit } from '@angular/core';
import { ConfirmationService, MessageService } from 'primeng/api';
import { InvestmentGoalService } from '../../../../services/investment.goal.service';
import { SharedService } from '../../../../services/shared.service';
import { InvestmentGoal } from '../../../../shared/models/investment-goal';

@Component({
  selector: 'app-investment-goal-listing',
  templateUrl: './investment-goal-listing.component.html',
  styleUrls: ['./investment-goal-listing.component.scss']
})
export class InvestmentGoalListingComponent implements OnInit {
  @Input('investmentGoals') model: InvestmentGoal[] = [];

  editModal: boolean = false;
  editInvestmentGoal: InvestmentGoal | undefined;

  constructor(private confirmationService: ConfirmationService, private messageService: MessageService, private investmentGoalService: InvestmentGoalService, private sharedService: SharedService) { }

  ngOnInit(): void {

  }

  delete(event: Event, investmentGoal: InvestmentGoal) {
    this.confirmationService.confirm({
      target: event.target!,
      message: 'Delete this item?',
      accept: () => {
        this.investmentGoalService.deleteInvestmentGoal(investmentGoal).subscribe((res: InvestmentGoal) => {
          if (res) {
            this.messageService.add({ severity: 'success', summary: 'Investment Goal Deleted' });
            this.refreshTable();
          }
          else this.messageService.add({ severity: 'error', summary: 'Investment Goal Delete Failed' });
        });
      }
    });
  }

  edit(event: Event, investmentGoal: InvestmentGoal) {
    this.editInvestmentGoal = Object.assign(new InvestmentGoal(), investmentGoal);
    this.editModal = true
  }

  editComplete(): void {
    this.editInvestmentGoal = undefined;
    this.editModal = false;

    this.refreshTable();
  }

  refreshTable(): void {
    this.investmentGoalService.getInvestmentGoals().subscribe((res: InvestmentGoal[]) => {
      if (res) this.model = res;
    });
  }

}
