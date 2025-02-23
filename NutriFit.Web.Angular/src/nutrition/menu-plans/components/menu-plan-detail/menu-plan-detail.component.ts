import { Component, inject } from '@angular/core';
import { globalModules } from '../../../../GlobalModules';
import { ActivatedRoute, Router } from '@angular/router';
import { MenuPlanService } from '../../services/menu-plan-service';
import {
  distinctUntilChanged,
  EMPTY,
  filter,
  map,
  Observable,
  shareReplay,
  switchMap,
  tap,
} from 'rxjs';
import { MenuPlanDto } from '../../dtos/menu-plan';

@Component({
  selector: 'nutrifit-menu-plan-detail',
  imports: [globalModules],
  templateUrl: './menu-plan-detail.component.html',
  styleUrl: './menu-plan-detail.component.scss',
})
export class MenuPlanDetailComponent {
  private readonly menuPlanService = inject(MenuPlanService);
  private readonly route = inject(ActivatedRoute);
  private readonly router = inject(Router);
  private readonly id$: Observable<string> = this.route.paramMap.pipe(
    map((params) => params.get('id')),
    filter((x) => x !== null),
    distinctUntilChanged(),
    shareReplay(1)
  );

  public readonly menuPlan$: Observable<MenuPlanDto> = this.id$.pipe(
    switchMap((id) => (id ? this.menuPlanService.getMenuPlan(id) : EMPTY)),
    shareReplay(1)
  );

  public deleteMenuPlan(): void {
    if (!confirm('Soll der Menüplan wirklich gelöscht werden?')) {
      return;
    }

    this.id$
      .pipe(
        switchMap((id) =>
          id ? this.menuPlanService.deleteMenuPlan(id) : EMPTY
        ),
        tap(() => this.router.navigateByUrl('/menu-plans'))
      )
      .subscribe();
  }
}
