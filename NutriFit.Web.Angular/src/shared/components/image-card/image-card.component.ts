import { Component, Input } from '@angular/core';
import { globalModules } from '../../../GlobalModules';

@Component({
  selector: 'nutrifit-image-card',
  imports: [globalModules],
  templateUrl: './image-card.component.html',
  styleUrl: './image-card.component.scss',
})
export class ImageCardComponent {
  @Input() public imgLink!: { text: string; href: string };
  @Input() public imgSrc!: { src: string; alt: string };
}
