import { Routes } from '@angular/router';
import { DashboardComponent } from '../dashboard/dashboard.component';
import { MenuPlansOverviewComponent } from '../nutrition/menu-plans/components/menu-plans-overview/menu-plans-overview.component';
import { RecipesOverviewComponent } from '../nutrition/recipes/components/recipes-overview/recipes-overview.component';
import { RecipeCreationComponent } from '../nutrition/recipes/components/recipe-creation/recipe-creation.component';
import { RecipeDetailComponent } from '../nutrition/recipes/components/recipe-detail/recipe-detail.component';
import { WorkoutsOverviewComponent } from '../workout/workouts/components/workouts-overview/workouts-overview.component';
import { ExercisesOverviewComponent } from '../workout/exercises/components/exercises-overview/exercises-overview.component';
import { RecipeService } from '../nutrition/recipes/services/recipe-service';
import { RecipeModificationComponent } from '../nutrition/recipes/components/recipe-modification/recipe-modification.component';

export const routes: Routes = [
  { path: '', redirectTo: 'dashboard', pathMatch: 'full' },
  { path: 'dashboard', component: DashboardComponent },
  { path: 'menu-plans', component: MenuPlansOverviewComponent },
  {
    path: 'recipes',
    providers: [RecipeService],
    children: [
      { path: '', component: RecipesOverviewComponent },
      { path: 'create', component: RecipeCreationComponent },
      { path: ':id', component: RecipeDetailComponent },
      { path: ':id/edit', component: RecipeModificationComponent },
    ],
  },
  { path: 'recipes/create', component: RecipeCreationComponent },
  { path: 'recipes/:id', component: RecipeDetailComponent },
  { path: 'recipes/:id/edit', component: RecipeModificationComponent },
  { path: 'workouts', component: WorkoutsOverviewComponent },
  { path: 'exercises', component: ExercisesOverviewComponent },
];
