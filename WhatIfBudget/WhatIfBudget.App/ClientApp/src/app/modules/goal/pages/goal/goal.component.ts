import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { BudgetService } from '../../../../services/budget.service';
import { InvestmentService } from '../../../../services/investment.service';
import { SharedService } from '../../../../services/shared.service';
import { AdditionalContributions, Budget } from '../../../../shared/models/budget';
import { Investment } from '../../../../shared/models/investment';

@Component({
  selector: 'app-goal',
  templateUrl: './goal.component.html',
  styleUrls: ['./goal.component.scss']
})
export class GoalComponent implements OnInit {

  private _budget: Budget | undefined = this.sharedService.budget;
  availableMonthlyNet: number = 0;
  availableFreeCash: number | undefined = 0;
  debtContribution: number | undefined = 0;
  mortgageContribution: number | undefined = 0;
  savingContribution: number | undefined = 0;
  investmentContribution: number | undefined = 0;
  isDebtDisabled: boolean = false;
  isMortgageDisabled: boolean = false;
  isSavingDisabled: boolean = false;
  isInvestmentDisabled: boolean = false;

  @Output('updated') updated: EventEmitter<void> = new EventEmitter();
  @Input('contributions') model: AdditionalContributions = new AdditionalContributions();

  constructor(private sharedService: SharedService, private budgetService: BudgetService, private investmentService: InvestmentService) { }

  ngOnInit(): void {
    this.sharedService.budgetLoadedEmit.subscribe(() => {
      if (this.sharedService.budgetLoaded) this._budget = this.sharedService.budget;
    });
    if (this.sharedService.budget.id) {
      this.budgetService.getNetAvailable(this.sharedService.budget.id).subscribe((res: number) => {
        if (res !== null) this.availableMonthlyNet = res;
      });
      this.budgetService.getAvailableFreeCash(this.sharedService.budget.id).subscribe((res: number) => {
        if (res !== null) this.availableFreeCash = res;
      });
      this.budgetService.getAdditionalContributions(this.sharedService.budget.id).subscribe((res: AdditionalContributions) => {
        if (res) {
          this.model = res;
          this.debtContribution = res.debtGoal;
          this.mortgageContribution = res.mortgageGoal;
          this.savingContribution = res.savingGoal;
          this.investmentContribution = res.investmentGoal;
          this.checkDisables(this.debtContribution!, this.investmentContribution!, this.mortgageContribution!, this.savingContribution!);
        }
      });
    }
  }

  sliderEvent() {
    let goals: AdditionalContributions = this.model as AdditionalContributions;
    if ((goals.debtGoal! + goals.investmentGoal! + goals.mortgageGoal! + goals.savingGoal!) <= this.availableFreeCash!) {
      this.checkDisables(goals.debtGoal!, goals.investmentGoal!, goals.mortgageGoal!, goals.savingGoal!);
      this.budgetService.updateAdditionalContributions(this.sharedService.budget.id!, goals).subscribe((res: AdditionalContributions) => {
        this.sharedService.reloadCharts();
        this.budgetService.getNetAvailable(this.sharedService.budget.id!).subscribe((res: number) => {
          if (res !== null) this.availableMonthlyNet = res;
        });
        this.debtContribution = res.debtGoal;
        this.mortgageContribution = res.mortgageGoal;
        this.savingContribution = res.savingGoal;
        this.investmentContribution = res.investmentGoal;
      });
    }
    else {
      this.budgetService.getAdditionalContributions(this.sharedService.budget.id!).subscribe((res: AdditionalContributions) => {
        if (res) {
          this.model = res;
        }
      });
    }
  }

  checkDisables(debtGoal: number, investmentGoal: number, mortgageGoal: number, savingGoal: number): void {
    if ((debtGoal! + investmentGoal! + mortgageGoal! + savingGoal!) == this.availableFreeCash!) {
        if (debtGoal == 0) {
          this.isDebtDisabled = true;
        }
        if (mortgageGoal == 0) {
          this.isMortgageDisabled = true;
        }
        if (savingGoal == 0) {
          this.isSavingDisabled = true;
        }
        if (investmentGoal == 0) {
          this.isInvestmentDisabled = true;
        }
      }
      else {
        this.isDebtDisabled = false;
        this.isMortgageDisabled = false;
        this.isSavingDisabled = false;
        this.isInvestmentDisabled = false;
      }
  }
}
