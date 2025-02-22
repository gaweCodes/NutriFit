import { Component, inject } from '@angular/core';
import { NonNullableFormBuilder, Validators } from '@angular/forms';
import { tap } from 'rxjs';
import { Router } from '@angular/router';

import { globalModules } from '../../../../GlobalModules';
import { RecipeCreationDto } from '../../dtos/recipe-creation';
import { RecipeService } from '../../services/recipe-service';

@Component({
  selector: 'nutrifit-recipe-creation',
  imports: [globalModules],
  templateUrl: './recipe-creation.component.html',
  styleUrl: './recipe-creation.component.scss',
})
export class RecipeCreationComponent {
  private readonly formBuilder = inject(NonNullableFormBuilder);
  private readonly recipeService = inject(RecipeService);
  private readonly router = inject(Router);

  public readonly recipeForm = this.formBuilder.group({
    name: ['', { validators: [Validators.required] }],
  });

  public createRecipe(): void {
    const recipeToCreate: RecipeCreationDto = this.recipeForm.getRawValue();
    this.recipeService
      .createRecipe(recipeToCreate)
      .pipe(tap((id) => this.router.navigateByUrl('/recipes/' + id)))
      .subscribe();
  }
}
