import { Component, inject } from '@angular/core';
import { globalModules } from '../../GlobalModules';
import { NonNullableFormBuilder, Validators } from '@angular/forms';

@Component({
  selector: 'nutrifit-recipes',
  templateUrl: './recipes.component.html',
  styleUrl: './recipes.component.scss',
  imports: [globalModules],
})
export class RecipesComponent {
  private readonly formBuilder = inject(NonNullableFormBuilder);
  public readonly recipeForm = this.formBuilder.group({
    name: ['', { validators: [Validators.required, Validators.minLength(1)] }],
  });
}
