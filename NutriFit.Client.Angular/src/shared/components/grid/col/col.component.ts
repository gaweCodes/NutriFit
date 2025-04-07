import { Component, Input } from '@angular/core';

@Component({
  selector: 'nutrifit-col',
  templateUrl: './col.component.html',
  styleUrl: './col.component.scss',
})
export class ColComponent {
  @Input() public columnWidth!:
    | 1
    | 2
    | 3
    | 4
    | 5
    | 6
    | 7
    | 8
    | 9
    | 10
    | 11
    | 12;
}
