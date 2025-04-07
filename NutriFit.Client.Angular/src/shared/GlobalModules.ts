import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { ReactiveFormsModule } from '@angular/forms';
import { LayoutCardComponent } from './components/layout-card/layout-card.component';
import { LinkComponent } from './components/link/link.component';
import { ButtonComponent } from './components/button/button.component';
import { ImageCardComponent } from './components/image-card/image-card.component';
import { RowComponent } from './components/grid/row/row.component';
import { ColComponent } from './components/grid/col/col.component';
import { ActionBarComponent } from './components/action-bar/action-bar.component';
import { TableComponent } from './components/table/table.component';
import { LoadingSpinnerComponent } from './components/loading-spinner/loading-spinner.component';

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
