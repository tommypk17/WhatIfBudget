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
  @Output('added') added: EventEmitter<void> = new EventEmitter();
  @Output('updated') updated: EventEmitter<void> = new EventEmitter();

  @Input('savingGoal') model: SavingGoal = new SavingGoal();

  constructor(private mortgageService: SavingGoalService, private sharedService: SharedService) { }

  ngOnInit(): void {
  }

  onSubmit(event: NgForm): void {
    let savingGoal: SavingGoal = event.value as SavingGoal;
    savingGoal.id = this.model.id;
    if (savingGoal.id && savingGoal.id > 0) {
      this.mortgageService.updateSavingGoal(savingGoal).subscribe((res: SavingGoal) => {
        this.updated.emit();
        this.model = new SavingGoal();
      });
    } else {
      this.mortgageService.saveSavingGoal(savingGoal).subscribe((res: SavingGoal) => {
        this.added.emit();
        this.model = new SavingGoal();
      });
    }
  }
}
