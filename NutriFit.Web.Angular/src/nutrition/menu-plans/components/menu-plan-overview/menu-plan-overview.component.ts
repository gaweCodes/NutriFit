import { Component, inject } from '@angular/core';
import { globalModules } from '../../../../GlobalModules';
import { Observable, shareReplay } from 'rxjs';
import { MenuPlanService } from '../../services/menu-plan-service';
import { MenuPlanOverviewDto } from '../../dtos/menu-plan-overview';

@Component({
  selector: 'nutrifit-menu-plan-overview',
  imports: [globalModules],
  templateUrl: './menu-plan-overview.component.html',
  styleUrl: './menu-plan-overview.component.scss',
})
export class MenuPlanOverviewComponent {
  private readonly menuPlanService = inject(MenuPlanService);
  public columns = ['Zeitraum'];
  public menuPlans$: Observable<MenuPlanOverviewDto[]> = this.menuPlanService
    .getMenuPlans()
    .pipe(shareReplay(1));
}
