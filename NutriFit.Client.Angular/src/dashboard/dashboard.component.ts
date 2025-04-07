import { Component } from '@angular/core';
import { globalModules } from '../shared/GlobalModules';

@Component({
  selector: 'nutrifit-dashboard',
  imports: [globalModules],
  templateUrl: './dashboard.component.html',
  styleUrl: './dashboard.component.scss',
})
export class DashboardComponent {}
