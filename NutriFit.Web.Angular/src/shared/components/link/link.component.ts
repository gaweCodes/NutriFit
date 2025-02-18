import { Component, Input } from '@angular/core';
import { globalModules } from '../../../GlobalModules';

@Component({
  selector: 'nutrifit-link',
  imports: [globalModules],
  templateUrl: './link.component.html',
  styleUrl: './link.component.scss',
})
export class LinkComponent {
  @Input() public appearance!: 'button' | 'text';
  @Input() public color!: 'primary' | 'secondary';
  @Input() public text!: string;
  @Input() public link!: string;
}
