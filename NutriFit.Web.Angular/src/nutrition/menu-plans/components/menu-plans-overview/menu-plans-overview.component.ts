import { Component } from '@angular/core';
import { globalModules } from '../../../../GlobalModules';
import { AppComponent } from '../../../../app/app.component';

@Component({
  selector: 'nutrifit-menu-plans-overview',
  imports: [globalModules, AppComponent],
  templateUrl: './menu-plans-overview.component.html',
  styleUrl: './menu-plans-overview.component.scss',
})
export class MenuPlansOverviewComponent {
  public columns = ['Zeitraum', 'Snacking?'];
}
