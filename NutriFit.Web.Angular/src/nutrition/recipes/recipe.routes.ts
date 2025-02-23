import { Routes } from '@angular/router';
import { RecipeService } from './services/recipe-service';

export const recipeRoutes: Routes = [
  {
    path: '',
    providers: [RecipeService],
    children: [
      {
        path: '',
        loadComponent: () =>
          import('./components/recipe-overview/recipe-overview.component').then(
            (m) => m.RecipeOverviewComponent
          ),
      },
      {
        path: 'create',
        loadComponent: () =>
          import('./components/recipe-creation/recipe-creation.component').then(
            (m) => m.RecipeCreationComponent
          ),
      },
      {
        path: ':id',
        loadComponent: () =>
          import('./components/recipe-detail/recipe-detail.component').then(
            (m) => m.RecipeDetailComponent
          ),
      },
      {
        path: ':id/edit',
        loadComponent: () =>
          import(
            './components/recipe-modification/recipe-modification.component'
          ).then((m) => m.RecipeModificationComponent),
      },
    ],
  },
];
