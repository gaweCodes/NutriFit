import { Routes } from '@angular/router';

export const routes: Routes = [
  { path: '', redirectTo: 'dashboard', pathMatch: 'full' },
  {
    path: 'dashboard',
    loadComponent: () =>
      import('../dashboard/dashboard.component').then(
        (m) => m.DashboardComponent
      ),
  },
  {
    path: 'menu-plans',
    loadChildren: () =>
      import('../nutrition/menu-plans/menu-plan.routes').then(
        (m) => m.menuPlanRoutes
      ),
  },
  {
    path: 'recipes',
    loadChildren: () =>
      import('../nutrition/recipes/recipe.routes').then((m) => m.recipeRoutes),
  },
  {
    path: 'workouts',
    loadComponent: () =>
      import(
        '../workout/workouts/components/workout-overview/workout-overview.component'
      ).then((m) => m.WorkoutOverviewComponent),
  },
  {
    path: 'exercises',
    loadComponent: () =>
      import(
        '../workout/exercises/components/exercise-overview/exercise-overview.component'
      ).then((m) => m.ExerciseOverviewComponent),
  },
];
