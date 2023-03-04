import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { NgForm } from '@angular/forms';
import { InvestmentGoalService } from '../../../../services/investment.goal.service'
import { SharedService } from '../../../../services/shared.service';
import { InvestmentGoal } from '../../../../shared/models/investment-goal';

@Component({
  selector: 'app-investment-goal-entry-form',
  templateUrl: './investment-goal-entry-form.component.html',
  styleUrls: ['./investment-goal-entry-form.component.scss']
})
export class InvestmentGoalEntryFormComponent implements OnInit {
  @Output('updated') updated: EventEmitter<void> = new EventEmitter();

  @Input('investmentGoal') model: InvestmentGoal = new InvestmentGoal();

  constructor(private investmentGoalService: InvestmentGoalService, private sharedService: SharedService) { }

  ngOnInit(): void {
  }

  onSubmit(event: NgForm): void {
    this.investmentGoalService.saveInvestmentGoal(this.model as InvestmentGoal).subscribe((res: InvestmentGoal) => {
      this.updated.emit();
    });
  }

}
