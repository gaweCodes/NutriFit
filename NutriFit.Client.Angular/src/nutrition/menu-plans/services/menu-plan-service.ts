import { inject, Injectable } from '@angular/core';
import { MenuPlanCreationDto } from '../dtos/menu-plan-creation';
import { MenuPlanDto } from '../dtos/menu-plan';
import { MenuPlanOverviewDto } from '../dtos/menu-plan-overview';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';

@Injectable()
export class MenuPlanService {
  private readonly httpClient = inject(HttpClient);
  private readonly BaseUrl = 'api/menuplans';

  public createMenuPlan(
    menuPlanToCreate: MenuPlanCreationDto
  ): Observable<string> {
    return this.httpClient.post<string>(this.BaseUrl, menuPlanToCreate);
  }

  public getMenuPlans(): Observable<MenuPlanOverviewDto[]> {
    return this.httpClient.get<MenuPlanOverviewDto[]>(this.BaseUrl);
  }

  public getMenuPlan(id: string): Observable<MenuPlanDto> {
    return this.httpClient.get<MenuPlanDto>(this.BaseUrl + '/' + id);
  }

  public updateMenuPlan(
    id: string,
    menuPlanToUpdate: MenuPlanDto
  ): Observable<string> {
    return this.httpClient.put<string>(
      this.BaseUrl + '/' + id,
      menuPlanToUpdate
    );
  }

  public deleteMenuPlan(id: string): Observable<void> {
    return this.httpClient.delete<void>(this.BaseUrl + '/' + id);
  }
}
