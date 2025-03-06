import { Component, inject } from '@angular/core';
import { tap } from 'rxjs';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';

import { MenuPlanCreationDto } from '../../dtos/menu-plan-creation';
import { MenuPlanService } from '../../services/menu-plan-service';
import { globalModules } from '../../../../shared/GlobalModules';

@Component({
  selector: 'nutrifit-menu-plan-creation',
  imports: [globalModules],
  templateUrl: './menu-plan-creation.component.html',
  styleUrl: './menu-plan-creation.component.scss',
})
export class MenuPlanCreationComponent {
  private readonly formBuilder = inject(FormBuilder);
  private readonly menuPlanService = inject(MenuPlanService);
  private readonly router = inject(Router);

  public readonly menuPlanForm: FormGroup = this.formBuilder.group({
    startDate: this.formBuilder.control<Date | null>(null, {
      validators: [Validators.required],
    }),
    endDate: this.formBuilder.control<Date | null>(null, {
      validators: [Validators.required],
    }),
  });

  public createMenuPlan(): void {
    const menuPlanToCreate: MenuPlanCreationDto =
      this.menuPlanForm.getRawValue();
    this.menuPlanService
      .createMenuPlan(menuPlanToCreate)
      .pipe(tap((id) => this.router.navigateByUrl('/menu-plans/' + id)))
      .subscribe();
  }
}
