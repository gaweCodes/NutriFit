import { Routes } from '@angular/router';
import { MenuPlanService } from './services/menu-plan-service';

export const menuPlanRoutes: Routes = [
  {
    path: '',
    providers: [MenuPlanService],
    children: [
      {
        path: '',
        loadComponent: () =>
          import(
            './components/menu-plan-overview/menu-plan-overview.component'
          ).then((m) => m.MenuPlanOverviewComponent),
      },
      {
        path: 'create',
        loadComponent: () =>
          import(
            './components/menu-plan-creation/menu-plan-creation.component'
          ).then((m) => m.MenuPlanCreationComponent),
      },
      {
        path: ':id',
        loadComponent: () =>
          import(
            './components/menu-plan-detail/menu-plan-detail.component'
          ).then((m) => m.MenuPlanDetailComponent),
      },
      {
        path: ':id/edit',
        loadComponent: () =>
          import(
            './components/menu-plan-modification/menu-plan-modification.component'
          ).then((m) => m.MenuPlanModificationComponent),
      },
    ],
  },
];
