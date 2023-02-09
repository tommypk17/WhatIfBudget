import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { HomeRoutingModule } from './home-routing.module';
import { FormsModule } from '@angular/forms';
import { HomeComponent } from './pages/home/home.component';
import { LoginComponent } from './pages/login/login.component';
import { LoginBlockComponent } from './components/login-block/login-block.component';

import { CardModule } from 'primeng/card';
import { ButtonModule } from 'primeng/button';
import { InputNumberModule } from 'primeng/inputnumber';
import { DropdownModule } from 'primeng/dropdown';

import { ExampleFormComponent } from './components/example-form/example-form.component';

@NgModule({
  declarations: [
    HomeComponent,
    LoginComponent,
    LoginBlockComponent,
    ExampleFormComponent
  ],
  imports: [
    CommonModule,
    HomeRoutingModule,
    FormsModule,

    CardModule,
    ButtonModule,
    InputNumberModule,
    DropdownModule
  ]
})
export class HomeModule { }
