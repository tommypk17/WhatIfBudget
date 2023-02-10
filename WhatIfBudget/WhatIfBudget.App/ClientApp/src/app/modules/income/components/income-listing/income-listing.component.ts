import { Component, Input, OnInit } from '@angular/core';
import { KeyValue } from '@angular/common';
import { IncomeService } from '../../../../services/income.service';
import { Income } from '../../../../shared/models/income';
import { SharedService } from '../../../../services/shared.service';

@Component({
  selector: 'app-income-listing',
  templateUrl: './income-listing.component.html',
  styleUrls: ['./income-listing.component.scss']
})
export class IncomeListingComponent implements OnInit {
  @Input('incomes') model: Income[] = [];

  frequencies: KeyValue<number, string>[] = this.sharedService.frequencies;

  constructor(private incomeService: IncomeService, private sharedService: SharedService) { }

  ngOnInit(): void {

  }

  displayFrequencies(type: number): string {
    let typeString: string | undefined = this.frequencies.find(x => x.key == type)?.value;
    return typeString ? typeString : 'N/A';
  }

}
