import { InjectionToken } from '@angular/core';
import { LocalStoreConfig } from './local-store-configuration';

export const LocalStores = new InjectionToken<LocalStoreConfig>('LocalStores');
