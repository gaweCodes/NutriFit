import { ApplicationConfig, provideZoneChangeDetection } from '@angular/core';
import {
  PreloadAllModules,
  provideRouter,
  withPreloading,
} from '@angular/router';
import { routes } from './app.routes';
import { provideHttpClient, withInterceptors } from '@angular/common/http';
import { toasterProvider } from '../shared/services/toaster-service';
import { apiErrorInterceptor } from '../shared/interceptors/api-error-interceptor';
import { LoadingInterceptor } from '../shared/interceptors/loading-interceptor';
import { provideAnimations } from '@angular/platform-browser/animations';

export const appConfig: ApplicationConfig = {
  providers: [
    provideZoneChangeDetection({ eventCoalescing: true }),
    provideRouter(routes, withPreloading(PreloadAllModules)),
    provideHttpClient(
      withInterceptors([LoadingInterceptor, apiErrorInterceptor])
    ),
    provideAnimations(),
    toasterProvider,
  ],
};
