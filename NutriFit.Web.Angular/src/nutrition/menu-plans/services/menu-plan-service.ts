import { inject, Injectable } from '@angular/core';
import { MenuPlanCreationDto } from '../dtos/menu-plan-creation';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';

@Injectable()
export class MenuPlanService {
  private readonly httpClient = inject(HttpClient);
  private readonly BaseUrl = 'api/menu-plans';

  public createMenuPlan(
    menuPlanToCreate: MenuPlanCreationDto
  ): Observable<string> {
    return this.httpClient.post<string>(this.BaseUrl, menuPlanToCreate);
  }
}
