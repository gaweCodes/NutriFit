import { Component, inject } from '@angular/core';
import { globalModules } from '../../GlobalModules';
import { LoadingService } from '../../services/loading-service';

@Component({
  selector: 'nutrifit-loading-spinner',
  imports: [globalModules],
  templateUrl: './loading-spinner.component.html',
  styleUrl: './loading-spinner.component.scss',
})
export class LoadingSpinnerComponent {
  private loadingService = inject(LoadingService);
  public loading$ = this.loadingService.loading$;
}
