import { Component, inject } from '@angular/core';
import { globalModules } from '../../../../GlobalModules';
import { RecipeService } from '../../services/recipe-service';
import { RecipeOverviewDto } from '../../dtos/recipe-overview';
import { Observable, shareReplay } from 'rxjs';

@Component({
  selector: 'nutrifit-recipes-overview',
  templateUrl: './recipes-overview.component.html',
  styleUrl: './recipes-overview.component.scss',
  imports: [globalModules],
  providers: [RecipeService],
})
export class RecipesOverviewComponent {
  private readonly recipeService = inject(RecipeService);

  public readonly recipes$: Observable<RecipeOverviewDto[]> = this.recipeService
    .getRecipes()
    .pipe(shareReplay(1));
}
