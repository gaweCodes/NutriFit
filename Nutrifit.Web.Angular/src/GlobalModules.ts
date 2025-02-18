import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { ReactiveFormsModule } from '@angular/forms';
import { LayoutCardComponent } from './shared/components/layout-card/layout-card.component';
import { LinkComponent } from './shared/components/link/link.component';
import { ButtonComponent } from './shared/components/button/button.component';

export const globalModules = [
  CommonModule,
  RouterModule,
  ReactiveFormsModule,
  ReactiveFormsModule,
  LayoutCardComponent,
  LinkComponent,
  ButtonComponent,
];
