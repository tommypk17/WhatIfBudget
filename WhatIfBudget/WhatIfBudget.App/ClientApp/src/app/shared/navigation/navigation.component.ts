import { Component, OnInit } from '@angular/core';
import { MenuItem } from 'primeng/api';

@Component({
  selector: 'app-navigation',
  templateUrl: './navigation.component.html',
  styleUrls: ['./navigation.component.scss']
})
export class NavigationComponent implements OnInit {

  constructor() { }

  items: MenuItem[] = [];

  ngOnInit() {
    this.items = [
      { label: 'Start New Budget', icon: 'pi pi-fw pi-refresh' },
      { label: 'Save Current Budget', icon: 'pi pi-fw pi-save' },
      { label: 'Load Saved Budget', icon: 'pi pi-fw pi-download' },
      { label: 'Log Out', icon: 'pi pi-fw pi-sign-out' }
    ];
  }
}
