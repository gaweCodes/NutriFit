import { Component, inject } from '@angular/core';
import { globalModules } from '../../../../GlobalModules';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
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
import { ActivatedRoute, Router } from '@angular/router';
import { MenuPlanDto } from '../../dtos/menu-plan';

@Component({
  selector: 'nutrifit-menu-plan-modification',
  imports: [globalModules],
  templateUrl: './menu-plan-modification.component.html',
  styleUrl: './menu-plan-modification.component.scss',
})
export class MenuPlanModificationComponent {
  private readonly formBuilder = inject(FormBuilder);
  private readonly menuPlanService = inject(MenuPlanService);
  private readonly router = inject(Router);
  private readonly route = inject(ActivatedRoute);

  public readonly id$: Observable<string> = this.route.paramMap.pipe(
    map((params) => params.get('id')),
    filter((x) => x !== null),
    distinctUntilChanged(),
    shareReplay(1)
  );
  public readonly menuPlan$: Observable<MenuPlanDto> = this.id$.pipe(
    switchMap((id) => (id ? this.menuPlanService.getMenuPlan(id) : EMPTY)),
    tap((menuPlan) => this.menuPlanForm.setValue(menuPlan))
  );
  public readonly menuPlanForm: FormGroup = this.formBuilder.group({
    startDate: this.formBuilder.control<Date | null>(null, {
      validators: [Validators.required],
    }),
    endDate: this.formBuilder.control<Date | null>(null, {
      validators: [Validators.required],
    }),
  });

  public updateMenuPlan(): void {
    const menuPlanToUpdate: MenuPlanDto = this.menuPlanForm.getRawValue();
    this.id$
      .pipe(
        switchMap((id) =>
          id ? this.menuPlanService.updateMenuPlan(id, menuPlanToUpdate) : EMPTY
        ),
        tap((id) => this.router.navigateByUrl('/menu-plans/' + id))
      )
      .subscribe();
  }
}
