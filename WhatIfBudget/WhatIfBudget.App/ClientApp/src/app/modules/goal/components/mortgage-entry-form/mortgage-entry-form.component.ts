import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { NgForm } from '@angular/forms';
import { MortgageService } from '../../../../services/mortgage.service';
import { SharedService } from '../../../../services/shared.service';
import { Mortgage } from '../../../../shared/models/mortgage';

@Component({
  selector: 'app-mortgage-entry-form',
  templateUrl: './mortgage-entry-form.component.html',
  styleUrls: ['./mortgage-entry-form.component.scss']
})
export class MortgageEntryFormComponent implements OnInit {
  @Output('updated') updated: EventEmitter<void> = new EventEmitter();

  @Input('mortgage') model: Mortgage = new Mortgage();

  constructor(private mortgageService: MortgageService, private sharedService: SharedService) { }

  ngOnInit(): void {
    this.mortgageService.getMortgageByGoalId(this.sharedService.budget.mortgageGoalId!).subscribe((res: Mortgage) => {
      this.model = res;
    })
  }

  onSubmit(event: NgForm): void {
    this.mortgageService.saveMortgages(this.model as Mortgage).subscribe((res: Mortgage) => {
      this.updated.emit();
    });
  }
}
