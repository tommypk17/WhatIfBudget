import { Component, OnInit } from '@angular/core';
import { SavingGoalService } from '../../../../services/saving.goal.service';
import { SharedService } from '../../../../services/shared.service';
import { SavingGoal } from '../../../../shared/models/saving';

@Component({
  selector: 'app-saving',
  templateUrl: './saving.component.html',
  styleUrls: ['./saving.component.scss']
})
export class SavingComponent implements OnInit {
  savingGoal: SavingGoal = new SavingGoal();

  constructor(private savingGoalService: SavingGoalService, private sharedService: SharedService) { }

  ngOnInit(): void {
  }

  savingGoalUpdated(): void {
    this.updateTotals();
  }

  updateTotals(): void {
    this.savingGoalService.getSavingGoal(this.sharedService.budget.savingGoalId ?? 0).subscribe((res: SavingGoal) => {
      if (res) this.savingGoal = res;
    });
  }

}
