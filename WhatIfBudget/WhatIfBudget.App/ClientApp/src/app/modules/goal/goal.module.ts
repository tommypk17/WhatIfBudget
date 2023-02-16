import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { GoalRoutingModule } from './goal-routing.module';
import { GoalComponent } from './pages/goal/goal.component';
import { CardModule } from 'primeng/card';
import { ButtonModule } from 'primeng/button';
import { SliderModule } from 'primeng/slider';

@NgModule({
  declarations: [
    GoalComponent
  ],
  imports: [
    CommonModule,
    GoalRoutingModule,

    CardModule,
    ButtonModule,
    SliderModule
  ]
})
export class GoalModule { }
