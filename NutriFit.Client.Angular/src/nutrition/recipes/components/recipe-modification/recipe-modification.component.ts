import { Component, inject } from '@angular/core';
import { NonNullableFormBuilder, Validators } from '@angular/forms';
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
import { ActivatedRoute, Router } from '@angular/router';
import { RecipeService } from '../../services/recipe-service';
import { RecipeDto } from '../../dtos/recipe';
import { globalModules } from '../../../../shared/GlobalModules';

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

  public readonly id$: Observable<string> = this.route.paramMap.pipe(
    map((params) => params.get('id')),
    filter((x) => x !== null),
    distinctUntilChanged(),
    shareReplay(1)
  );
  public readonly recipe$: Observable<RecipeDto> = this.id$.pipe(
    switchMap((id) => (id ? this.recipeService.getRecipe(id) : EMPTY)),
    tap((recipe) => this.recipeForm.setValue(recipe))
  );

  public readonly recipeForm = this.formBuilder.group({
    name: ['', { validators: [Validators.required] }],
  });

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
