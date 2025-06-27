import {
  ApplicationConfig,
  provideZoneChangeDetection,
  isDevMode,
} from '@angular/core';
import {
  PreloadAllModules,
  provideRouter,
  withPreloading,
} from '@angular/router';
import { routes } from './app.routes';
import { provideServiceWorker } from '@angular/service-worker';
import { toasterProvider } from '../shared/services/toaster-service';
import { provideAnimations } from '@angular/platform-browser/animations';
import { provideHttpClient, withInterceptors } from '@angular/common/http';
import { LoadingInterceptor } from '../shared/interceptors/loading-interceptor';
import { apiErrorInterceptor } from '../shared/interceptors/api-error-interceptor';
import { LocalStores } from '../shared/interfaces/local-stores-injection-token';

export const appConfig: ApplicationConfig = {
  providers: [
    provideZoneChangeDetection({ eventCoalescing: true }),
    provideRouter(routes, withPreloading(PreloadAllModules)),
    provideHttpClient(
      withInterceptors([LoadingInterceptor, apiErrorInterceptor])
    ),
    provideAnimations(),
    toasterProvider,
    provideServiceWorker('ngsw-worker.js', {
      enabled: !isDevMode(),
      registrationStrategy: 'registerWhenStable:30000',
    }),
    { provide: LocalStores, useValue: { recipes: 'key' } },
  ],
};
