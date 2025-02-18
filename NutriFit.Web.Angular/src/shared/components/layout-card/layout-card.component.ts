import { Component, Input } from '@angular/core';

@Component({
  selector: 'nutrifit-layout-card',
  imports: [],
  templateUrl: './layout-card.component.html',
  styleUrl: './layout-card.component.scss',
})
export class LayoutCardComponent {
  @Input() public pageTitle!: string;
}
