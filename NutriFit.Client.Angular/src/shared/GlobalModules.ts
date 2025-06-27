import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { ReactiveFormsModule } from '@angular/forms';
import { LayoutCardComponent } from './ui-core/layout-card/layout-card.component';
import { LinkComponent } from './ui-core/link/link.component';
import { ButtonComponent } from './ui-core/button/button.component';
import { ImageCardComponent } from './ui-core/image-card/image-card.component';
import { RowComponent } from './ui-core/grid/row/row.component';
import { ColComponent } from './ui-core/grid/col/col.component';
import { ActionBarComponent } from './ui-core/action-bar/action-bar.component';
import { TableComponent } from './ui-core/table/table.component';
import { LoadingSpinnerComponent } from './ui-core/loading-spinner/loading-spinner.component';

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
  LoadingSpinnerComponent,
];
