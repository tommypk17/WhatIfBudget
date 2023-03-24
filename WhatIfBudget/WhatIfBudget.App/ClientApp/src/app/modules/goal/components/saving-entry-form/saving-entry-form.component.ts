import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { NgForm } from '@angular/forms';
import { SavingGoalService } from '../../../../services/saving.goal.service';
import { SharedService } from '../../../../services/shared.service';
import { SavingGoal } from '../../../../shared/models/saving';

@Component({
  selector: 'app-saving-entry-form',
  templateUrl: './saving-entry-form.component.html',
  styleUrls: ['./saving-entry-form.component.scss']
})
export class SavingEntryFormComponent implements OnInit {
  @Output('updated') updated: EventEmitter<void> = new EventEmitter();

  @Input('savingGoal') model: SavingGoal = new SavingGoal();

  constructor(private savingGoalService: SavingGoalService, private sharedService: SharedService) { }

  ngOnInit(): void {
    this.savingGoalService.getSavingGoal(this.sharedService.budget.savingGoalId!).subscribe((res: SavingGoal) => {
      this.model = res;
    })
  }

  onSubmit(event: NgForm): void {
    this.savingGoalService.saveSavingGoal(this.model as SavingGoal).subscribe((res: SavingGoal) => {
      this.updated.emit();
    });
  }
}
