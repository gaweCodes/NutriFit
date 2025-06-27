import { CommonModule } from '@angular/common';
import { Component, EventEmitter, Input, Output } from '@angular/core';

@Component({
  selector: 'nutrifit-button',
  templateUrl: './button.component.html',
  styleUrl: './button.component.scss',
  imports: [CommonModule],
})
export class ButtonComponent {
  @Input() public color!: 'primary' | 'secondary' | 'danger';
  @Input() public text!: string;
  @Input() public isDisabled!: boolean;
  @Output() public clicked = new EventEmitter();
}
