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
  tap,
} from 'rxjs';
import { RecipeDto } from '../../dtos/recipe';
import { ActivatedRoute, Router } from '@angular/router';
import { AppComponent } from '../../../../app/app.component';

@Component({
  selector: 'nutrifit-recipe-detail',
  imports: [globalModules, AppComponent],
  templateUrl: './recipe-detail.component.html',
  styleUrl: './recipe-detail.component.scss',
})
export class RecipeDetailComponent {
  private readonly recipeService = inject(RecipeService);
  private readonly route = inject(ActivatedRoute);
  private readonly router = inject(Router);
  private readonly id$ = this.route.paramMap.pipe(
    map((params) => params.get('id')),
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
