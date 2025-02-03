import { Routes } from '@angular/router';
import { DashboardComponent } from './dashboard/dashboard.component';
import { MenuPlansComponent } from './menu-plans/menu-plans.component';
import { RecipesComponent } from './recipes/recipes.component';
import { ExercisesComponent } from './exercises/exercises.component';
import { WorkoutsComponent } from './workouts/workouts.component';

export const routes: Routes = [
  { path: '', redirectTo: 'dashboard', pathMatch: 'full' },
  { path: 'dashboard', component: DashboardComponent },
  { path: 'menu-plans', component: MenuPlansComponent },
  { path: 'recipes', component: RecipesComponent },
  { path: 'workouts', component: WorkoutsComponent },
  { path: 'exercises', component: ExercisesComponent },
];
