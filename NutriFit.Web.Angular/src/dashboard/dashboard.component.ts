import { Component } from '@angular/core';
import { globalModules } from '../GlobalModules';
import { LayoutCardComponent } from '../shared/components/layout-card/layout-card.component';

@Component({
  selector: 'nutrifit-dashboard',
  imports: [globalModules, LayoutCardComponent],
  templateUrl: './dashboard.component.html',
  styleUrl: './dashboard.component.scss',
})
export class DashboardComponent {}
