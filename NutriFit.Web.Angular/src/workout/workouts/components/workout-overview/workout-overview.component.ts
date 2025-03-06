import { Component } from '@angular/core';
import { globalModules } from '../../../../shared/GlobalModules';

@Component({
  selector: 'nutrifit-workout-overview',
  imports: [globalModules],
  templateUrl: './workout-overview.component.html',
  styleUrl: './workout-overview.component.scss',
})
export class WorkoutOverviewComponent {}
