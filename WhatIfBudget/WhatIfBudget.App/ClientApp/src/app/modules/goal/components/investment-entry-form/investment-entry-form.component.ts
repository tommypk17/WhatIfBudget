import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { NgForm } from '@angular/forms';
import { InvestmentService } from '../../../../services/investment.service'
import { SharedService } from '../../../../services/shared.service';
import { Investment } from '../../../../shared/models/investment';

@Component({
  selector: 'app-investment-entry-form',
  templateUrl: './investment-entry-form.component.html',
  styleUrls: ['./investment-entry-form.component.scss']
})
export class InvestmentEntryFormComponent implements OnInit {
  @Output('added') added: EventEmitter<void> = new EventEmitter();

  model: Investment = new Investment();

  constructor(private investmentService: InvestmentService, private sharedService: SharedService) { }

  ngOnInit(): void {
  }

  onSubmit(event: NgForm): void {
    let investment: Investment = event.value as Investment;
    investment.goalId = this.sharedService.budget.investmentGoalId;
    this.investmentService.saveInvestments(investment).subscribe((res: Investment) => {
      this.added.emit();
      this.model = new Investment();
    });
  }

}
