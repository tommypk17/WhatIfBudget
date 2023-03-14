import { Component, OnInit } from '@angular/core';
import { MortgageService } from '../../../../services/mortgage.service';
import { SharedService } from '../../../../services/shared.service';

@Component({
  selector: 'app-mortgage',
  templateUrl: './mortgage.component.html',
  styleUrls: ['./mortgage.component.scss']
})
export class MortgageComponent implements OnInit {

  constructor(private mortgageService: MortgageService, private sharedService: SharedService) { }

  ngOnInit(): void {
  }

}
