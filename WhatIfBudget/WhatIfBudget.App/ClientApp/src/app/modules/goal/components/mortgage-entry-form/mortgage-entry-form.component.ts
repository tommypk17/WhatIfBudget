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
  @Output('added') added: EventEmitter<void> = new EventEmitter();
  @Output('updated') updated: EventEmitter<void> = new EventEmitter();

  @Input('mortgage') model: Mortgage = new Mortgage();

  constructor(private mortgageService: MortgageService, private sharedService: SharedService) { }

  ngOnInit(): void {
  }

  onSubmit(event: NgForm): void {
    let mortgage: Mortgage = event.value as Mortgage;
    mortgage.id = this.model.id;
    if (mortgage.id && mortgage.id > 0) {
      this.mortgageService.updateInvestment(mortgage).subscribe((res: Mortgage) => {
        this.updated.emit();
        this.model = new Mortgage();
      });
    } else {
      this.mortgageService.saveMortgages(mortgage).subscribe((res: Mortgage) => {
        this.added.emit();
        this.model = new Mortgage();
      });
    }
  }
}
