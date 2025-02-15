import { inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { RecipeCreationDto } from '../dtos/recipe-creation';
import { HttpClient } from '@angular/common/http';
import { RecipeOverviewDto } from '../dtos/recipe-overview';
import { RecipeDto } from '../dtos/recipe';

@Injectable()
export class RecipeService {
  private readonly httpClient = inject(HttpClient);
  private readonly BaseUrl = 'api/recipes';

  public getRecipe(id: string): Observable<RecipeDto> {
    return this.httpClient.get<RecipeDto>(this.BaseUrl + '/' + id);
  }

  public getRecipes(): Observable<RecipeOverviewDto[]> {
    return this.httpClient.get<RecipeOverviewDto[]>(this.BaseUrl);
  }

  public createRecipe(recipeToCreate: RecipeCreationDto): Observable<string> {
    return this.httpClient.post<string>(this.BaseUrl, recipeToCreate);
  }

  public updateRecipe(
    id: string,
    recipeToUpdate: RecipeDto
  ): Observable<string> {
    return this.httpClient.put<string>(this.BaseUrl + '/' + id, recipeToUpdate);
  }
}
