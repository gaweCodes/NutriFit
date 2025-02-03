import { Component } from '@angular/core';
import { globalModules } from '../../GlobalModules';

@Component({
  selector: 'nutrifit-exercises',
  imports: [globalModules],
  templateUrl: './exercises.component.html',
  styleUrl: './exercises.component.scss',
})
export class ExercisesComponent {}
