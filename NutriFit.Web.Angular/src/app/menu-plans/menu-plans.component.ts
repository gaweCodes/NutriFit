import { Component, inject, OnInit } from '@angular/core';
import { globalModules } from '../../GlobalModules';
import { HttpClient } from '@angular/common/http';

interface WeatherForecast {
  date: string;
  temperatureC: number;
  temperatureF: number;
  summary: string;
}

@Component({
  selector: 'nutrifit-menu-plans',
  imports: [globalModules],
  templateUrl: './menu-plans.component.html',
  styleUrl: './menu-plans.component.scss',
})
export class MenuPlansComponent implements OnInit {
  public forecasts: WeatherForecast[] = [];

  private readonly _httpClient: HttpClient = inject(HttpClient);

  public ngOnInit(): void {
    this.getForecasts();
  }

  private getForecasts(): void {
    this._httpClient.get<WeatherForecast[]>('/api/menuplans').subscribe(
      (result) => {
        this.forecasts = result;
      },
      (error) => {
        console.error(error);
      }
    );
  }
}
