import { HttpClient } from '@angular/common/http';
import { Component, inject, OnInit } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { globalModules } from '../GlobalModules';

interface WeatherForecast {
  date: string;
  temperatureC: number;
  temperatureF: number;
  summary: string;
}

@Component({
  selector: 'nutrifit-root',
  imports: [RouterOutlet, globalModules],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss',
})
export class AppComponent implements OnInit {
  public forecasts: WeatherForecast[] = [];

  private readonly _httpClient: HttpClient = inject(HttpClient);

  public ngOnInit(): void {
    this.getForecasts();
  }

  private getForecasts(): void {
    this._httpClient.get<WeatherForecast[]>('/weatherforecast').subscribe(
      (result) => {
        this.forecasts = result;
      },
      (error) => {
        console.error(error);
      }
    );
  }
}
