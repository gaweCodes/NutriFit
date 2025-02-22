import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { ReactiveFormsModule } from '@angular/forms';
import { LayoutCardComponent } from './shared/components/layout-card/layout-card.component';
import { LinkComponent } from './shared/components/link/link.component';
import { ButtonComponent } from './shared/components/button/button.component';
import { ImageCardComponent } from './shared/components/image-card/image-card.component';
import { RowComponent } from './shared/components/grid/row/row.component';
import { ColComponent } from './shared/components/grid/col/col.component';
import { ActionBarComponent } from './shared/components/action-bar/action-bar.component';
import { TableComponent } from './shared/components/table/table.component';

export const globalModules = [
  CommonModule,
  RouterModule,
  ReactiveFormsModule,
  ReactiveFormsModule,
  LayoutCardComponent,
  LinkComponent,
  ButtonComponent,
  ImageCardComponent,
  RowComponent,
  ColComponent,
  ActionBarComponent,
  TableComponent,
];
