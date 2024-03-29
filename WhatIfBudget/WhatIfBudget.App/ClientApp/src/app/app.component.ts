import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { MsalBroadcastService } from '@azure/msal-angular';
import { EventMessage, EventType, InteractionStatus } from '@azure/msal-browser';
import { filter } from 'rxjs';
import { SharedService } from './services/shared.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  title = 'WhatIfBudget';

  constructor(private sharedService: SharedService, private msalBroadcastService: MsalBroadcastService, private router: Router) {}

  ngOnInit(): void {
    this.msalBroadcastService.msalSubject$
      .pipe(
        filter((msg: EventMessage) => msg.eventType === EventType.LOGIN_SUCCESS),
      )
      .subscribe((result: EventMessage) => {
        this.sharedService.loggedInEmit.emit();
        let redirect = sessionStorage.getItem('afterLogin');
        if (redirect) {
          sessionStorage.removeItem('afterLogin');
          this.router.navigate([redirect]);
        }
      });
  }
}
