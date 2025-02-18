import { Component, Input } from '@angular/core';
import { NgClass, NgIf } from '@angular/common';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'nutrifit-link',
  imports: [NgClass, NgIf, RouterLink],
  templateUrl: './link.component.html',
  styleUrl: './link.component.scss',
})
export class LinkComponent {
  @Input() public appearance!: 'button' | 'text';
  @Input() public color!: 'primary' | 'secondary';
  @Input() public text!: string;
  @Input() public link!: string;
}
