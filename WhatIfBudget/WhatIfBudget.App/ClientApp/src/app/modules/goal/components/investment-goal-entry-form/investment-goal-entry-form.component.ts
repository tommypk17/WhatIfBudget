import { Component, EventEmitter, OnInit, Output } from '@angular/core';
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
  @Output('added') added: EventEmitter<void> = new EventEmitter();

  model: InvestmentGoal = new InvestmentGoal();

  constructor(private investmentGoalService: InvestmentGoalService, private sharedService: SharedService) { }

  ngOnInit(): void {
  }

  onSubmit(event: NgForm): void {
    this.investmentGoalService.saveInvestmentGoals(event.value as InvestmentGoal).subscribe((res: InvestmentGoal) => {
      this.added.emit();
      this.model = new InvestmentGoal();
    });
  }

}
