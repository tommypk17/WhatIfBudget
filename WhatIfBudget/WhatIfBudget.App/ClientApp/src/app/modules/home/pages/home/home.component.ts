import { Component, OnInit } from '@angular/core';
import { SharedService } from '../../../../services/shared.service';
import { Budget } from '../../../../shared/models/budget';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {

  private _budget: Budget | undefined = this.sharedService.budget;

  constructor(private sharedService: SharedService) { }

  ngOnInit(): void {
    this.sharedService.budgetLoadedEmit.subscribe(() => {
      if (this.sharedService.budgetLoaded) this._budget = this.sharedService.budget;
    })
  }

  get budget(): Budget | undefined {
    return this._budget;
  }

}
