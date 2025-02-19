import { Component, Input, OnInit } from '@angular/core';
import { RowComponent } from '../grid/row/row.component';

@Component({
  selector: 'nutrifit-layout-card',
  imports: [RowComponent],
  templateUrl: './layout-card.component.html',
  styleUrl: './layout-card.component.scss',
})
export class LayoutCardComponent implements OnInit {
  @Input() public pageTitle!: string;

  public ngOnInit(): void {
    document.title = this.pageTitle + ' | NutriFit';
  }
}
