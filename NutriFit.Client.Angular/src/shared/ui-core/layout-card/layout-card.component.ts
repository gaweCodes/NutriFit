import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'nutrifit-layout-card',
  templateUrl: './layout-card.component.html',
  styleUrl: './layout-card.component.scss',
})
export class LayoutCardComponent implements OnInit {
  @Input() public pageTitle!: string;

  public ngOnInit(): void {
    document.title = this.pageTitle + ' | NutriFit';
  }
}
