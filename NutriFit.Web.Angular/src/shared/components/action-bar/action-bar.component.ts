import { Component } from '@angular/core';
import { RowComponent } from '../grid/row/row.component';

@Component({
  selector: 'nutrifit-action-bar',
  imports: [RowComponent],
  templateUrl: './action-bar.component.html',
  styleUrl: './action-bar.component.scss',
})
export class ActionBarComponent {}
