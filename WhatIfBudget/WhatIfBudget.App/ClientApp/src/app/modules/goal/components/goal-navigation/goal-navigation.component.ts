import { Component, OnInit } from '@angular/core';
import { MenuItem } from 'primeng/api';

@Component({
  selector: 'app-goal-navigation',
  templateUrl: './goal-navigation.component.html',
  styleUrls: ['./goal-navigation.component.scss']
})
export class GoalNavigationComponent implements OnInit {

  constructor() { }

  items: MenuItem[] = [];

  ngOnInit() {
    this.items = [
      { label: 'Summary', icon: 'pi pi-fw pi-home', routerLink: ['','goals', 'summary'] },
      { label: 'Savings', icon: 'pi pi-fw pi-calendar', routerLink: ['', 'goals', 'savings'] },
      { label: 'Debts', icon: 'pi pi-fw pi-pencil', routerLink: '/goals/debts' },
      { label: 'Mortgages', icon: 'pi pi-fw pi-file', routerLink: '/goals/mortgages' },
      { label: 'Investments', icon: 'pi pi-fw pi-cog', routerLink: '/goals/investments' }
    ];
  }

}
