import { Component, Input } from '@angular/core';
import { LinkComponent } from '../link/link.component';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'nutrifit-image-card',
  imports: [LinkComponent, RouterLink],
  templateUrl: './image-card.component.html',
  styleUrl: './image-card.component.scss',
})
export class ImageCardComponent {
  @Input() public imgLink!: { text: string; href: string };
  @Input() public imgSrc!: { src: string; alt: string };
}
