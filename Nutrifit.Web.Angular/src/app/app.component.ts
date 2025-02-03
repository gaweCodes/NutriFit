import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { globalModules } from '../GlobalModules';

@Component({
  selector: 'nutrifit-root',
  imports: [RouterOutlet, globalModules],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss',
})
export class AppComponent {}
