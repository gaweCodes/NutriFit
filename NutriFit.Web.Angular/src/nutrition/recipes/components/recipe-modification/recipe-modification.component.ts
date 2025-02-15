import { Component, inject } from '@angular/core';
import { NonNullableFormBuilder, Validators } from '@angular/forms';
import {
  distinctUntilChanged,
  EMPTY,
  map,
  Observable,
  shareReplay,
  switchMap,
  tap,
} from 'rxjs';
import { ActivatedRoute, Router } from '@angular/router';
import { globalModules } from '../../../../GlobalModules';
import { RecipeService } from '../../services/recipe-service';
import { RecipeDto } from '../../dtos/recipe';

@Component({
  selector: 'nutrifit-recipe-modification',
  imports: [globalModules],
  templateUrl: './recipe-modification.component.html',
  styleUrl: './recipe-modification.component.scss',
})
export class RecipeModificationComponent {
  private readonly formBuilder = inject(NonNullableFormBuilder);
  private readonly recipeService = inject(RecipeService);
  private readonly router = inject(Router);
  private readonly route = inject(ActivatedRoute);
  private readonly id$ = this.route.paramMap.pipe(
    map((params) => params.get('id')),
    distinctUntilChanged(),
    shareReplay(1)
  );

  private readonly recipe$: Observable<RecipeDto> = this.id$.pipe(
    switchMap((id) => (id ? this.recipeService.getRecipe(id) : EMPTY)),
    tap((recipe) => this.recipeForm.setValue(recipe))
  );

  public readonly recipeForm = this.formBuilder.group({
    name: ['', { validators: [Validators.required] }],
  });

  constructor() {
    this.recipe$.subscribe();
  }

  public updateRecipe(): void {
    const recipeToUpdate: RecipeDto = this.recipeForm.getRawValue();

    this.id$
      .pipe(
        switchMap((id) =>
          id ? this.recipeService.updateRecipe(id, recipeToUpdate) : EMPTY
        ),
        tap((x) => this.router.navigateByUrl('/recipes/' + x))
      )
      .subscribe();
  }
}
