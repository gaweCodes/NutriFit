import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { globalModules } from '../shared/GlobalModules';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet, globalModules],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss',
})
export class AppComponent {}
