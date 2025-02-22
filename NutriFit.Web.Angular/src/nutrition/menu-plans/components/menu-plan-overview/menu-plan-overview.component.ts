import { Component } from '@angular/core';
import { globalModules } from '../../../../GlobalModules';
import { AppComponent } from '../../../../app/app.component';

@Component({
  selector: 'nutrifit-menu-plan-overview',
  imports: [globalModules, AppComponent],
  templateUrl: './menu-plan-overview.component.html',
  styleUrl: './menu-plan-overview.component.scss',
})
export class MenuPlanOverviewComponent {
  public columns = ['Zeitraum', 'Snacking?'];
}
