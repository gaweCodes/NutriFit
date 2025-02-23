import { Routes } from '@angular/router';
import { DashboardComponent } from '../dashboard/dashboard.component';
import { MenuPlanOverviewComponent } from '../nutrition/menu-plans/components/menu-plan-overview/menu-plan-overview.component';
import { MenuPlanCreationComponent } from '../nutrition/menu-plans/components/menu-plan-creation/menu-plan-creation.component';
import { MenuPlanDetailComponent } from '../nutrition/menu-plans/components/menu-plan-detail/menu-plan-detail.component';
import { MenuPlanModificationComponent } from '../nutrition/menu-plans/components/menu-plan-modification/menu-plan-modification.component';
import { MenuPlanService } from '../nutrition/menu-plans/services/menu-plan-service';
import { RecipeOverviewComponent } from '../nutrition/recipes/components/recipe-overview/recipe-overview.component';
import { RecipeCreationComponent } from '../nutrition/recipes/components/recipe-creation/recipe-creation.component';
import { RecipeDetailComponent } from '../nutrition/recipes/components/recipe-detail/recipe-detail.component';
import { RecipeModificationComponent } from '../nutrition/recipes/components/recipe-modification/recipe-modification.component';
import { RecipeService } from '../nutrition/recipes/services/recipe-service';
import { WorkoutOverviewComponent } from '../workout/workouts/components/workout-overview/workout-overview.component';
import { ExerciseOverviewComponent } from '../workout/exercises/components/exercise-overview/exercise-overview.component';

export const routes: Routes = [
  { path: '', redirectTo: 'dashboard', pathMatch: 'full' },
  { path: 'dashboard', component: DashboardComponent },
  {
    path: 'menu-plans',
    providers: [MenuPlanService],
    children: [
      { path: '', component: MenuPlanOverviewComponent },
      { path: 'create', component: MenuPlanCreationComponent },
      { path: ':id', component: MenuPlanDetailComponent },
      { path: ':id/edit', component: MenuPlanModificationComponent },
    ],
  },
  {
    path: 'recipes',
    providers: [RecipeService],
    children: [
      { path: '', component: RecipeOverviewComponent },
      { path: 'create', component: RecipeCreationComponent },
      { path: ':id', component: RecipeDetailComponent },
      { path: ':id/edit', component: RecipeModificationComponent },
    ],
  },
  { path: 'recipes/create', component: RecipeCreationComponent },
  { path: 'recipes/:id', component: RecipeDetailComponent },
  { path: 'recipes/:id/edit', component: RecipeModificationComponent },
  { path: 'workouts', component: WorkoutOverviewComponent },
  { path: 'exercises', component: ExerciseOverviewComponent },
];
