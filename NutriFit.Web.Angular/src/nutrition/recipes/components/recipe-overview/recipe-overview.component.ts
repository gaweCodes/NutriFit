import { Component, inject } from '@angular/core';
import { RecipeService } from '../../services/recipe-service';
import { RecipeOverviewDto } from '../../dtos/recipe-overview';
import { Observable, shareReplay } from 'rxjs';
import { globalModules } from '../../../../shared/GlobalModules';

@Component({
  selector: 'nutrifit-recipe-overview',
  templateUrl: './recipe-overview.component.html',
  styleUrl: './recipe-overview.component.scss',
  imports: [globalModules],
})
export class RecipeOverviewComponent {
  private readonly recipeService = inject(RecipeService);

  public readonly recipes$: Observable<RecipeOverviewDto[]> = this.recipeService
    .getRecipes()
    .pipe(shareReplay(1));
}
