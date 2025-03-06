import { Component, inject } from '@angular/core';
import { RecipeService } from '../../services/recipe-service';
import {
  distinctUntilChanged,
  EMPTY,
  filter,
  map,
  Observable,
  shareReplay,
  switchMap,
  tap,
} from 'rxjs';
import { RecipeDto } from '../../dtos/recipe';
import { ActivatedRoute, Router } from '@angular/router';
import { globalModules } from '../../../../shared/GlobalModules';

@Component({
  selector: 'nutrifit-recipe-detail',
  imports: [globalModules],
  templateUrl: './recipe-detail.component.html',
  styleUrl: './recipe-detail.component.scss',
})
export class RecipeDetailComponent {
  private readonly recipeService = inject(RecipeService);
  private readonly route = inject(ActivatedRoute);
  private readonly router = inject(Router);
  private readonly id$: Observable<string> = this.route.paramMap.pipe(
    map((params) => params.get('id')),
    filter((x) => x !== null),
    distinctUntilChanged(),
    shareReplay(1)
  );

  public readonly recipe$: Observable<RecipeDto> = this.id$.pipe(
    switchMap((id) => (id ? this.recipeService.getRecipe(id) : EMPTY)),
    shareReplay(1)
  );

  public deleteRecipe(): void {
    if (!confirm('Soll das rezept wirklich gelöscht werden?')) {
      return;
    }

    this.id$
      .pipe(
        switchMap((id) => (id ? this.recipeService.deleteRecipe(id) : EMPTY)),
        tap(() => this.router.navigateByUrl('/recipes'))
      )
      .subscribe();
  }
}
