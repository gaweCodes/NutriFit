import {
  EnvironmentProviders,
  inject,
  Injectable,
  Provider,
} from '@angular/core';
import { provideToastr, ToastrService } from 'ngx-toastr';

@Injectable({ providedIn: 'root' })
export class ToasterService {
  private toastrService = inject(ToastrService);
  public showError(message: string, title: string): void {
    this.toastrService.error(message, title, {
      timeOut: 0,
      extendedTimeOut: 0,
      closeButton: true,
    });
  }
}

export const toasterProvider: (Provider | EnvironmentProviders)[] = [
  provideToastr(),
  ToastrService,
];
