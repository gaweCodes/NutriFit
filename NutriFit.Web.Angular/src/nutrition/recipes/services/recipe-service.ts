import { inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { RecipeCreationDto } from '../dtos/recipe-creation';
import { HttpClient } from '@angular/common/http';
import { RecipeDetailDto } from '../dtos/recipe-detail';
import { RecipeOverviewDto } from '../dtos/recipe-overview';

@Injectable()
export class RecipeService {
  private readonly httpClient = inject(HttpClient);
  private readonly BaseUrl = 'api/recipes';

  public createRecipe(recipeToCreate: RecipeCreationDto): Observable<string> {
    return this.httpClient.post<string>(this.BaseUrl, recipeToCreate);
  }

  public getRecipeDetail(id: string): Observable<RecipeDetailDto> {
    return this.httpClient.get<RecipeDetailDto>(this.BaseUrl + '/' + id);
  }

  public getRecipes(): Observable<RecipeOverviewDto[]> {
    return this.httpClient.get<RecipeOverviewDto[]>(this.BaseUrl);
  }
}
