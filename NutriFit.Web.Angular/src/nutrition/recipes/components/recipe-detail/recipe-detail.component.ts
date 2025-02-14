import { Component, inject } from '@angular/core';
import { globalModules } from '../../../../GlobalModules';
import { RecipeService } from '../../services/recipe-service';
import {
  distinctUntilChanged,
  EMPTY,
  map,
  Observable,
  shareReplay,
  switchMap,
} from 'rxjs';
import { RecipeDetailDto } from '../../dtos/recipe-detail';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'nutrifit-recipe-detail',
  imports: [globalModules],
  templateUrl: './recipe-detail.component.html',
  styleUrl: './recipe-detail.component.scss',
  providers: [RecipeService],
})
export class RecipeDetailComponent {
  private readonly recipeService = inject(RecipeService);
  private readonly route = inject(ActivatedRoute);
  private readonly id$ = this.route.paramMap.pipe(
    map((params) => params.get('id')),
    distinctUntilChanged()
  );

  public readonly recipe$: Observable<RecipeDetailDto> = this.id$.pipe(
    switchMap((id) => (id ? this.recipeService.getRecipeDetail(id) : EMPTY)),
    shareReplay(1)
  );
}
