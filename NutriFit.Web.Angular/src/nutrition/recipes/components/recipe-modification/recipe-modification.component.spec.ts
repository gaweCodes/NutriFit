import { ComponentFixture, TestBed } from '@angular/core/testing';
import { RecipeModificationComponent } from './recipe-modification.component';

describe('RecipeModificationComponent', () => {
  let component: RecipeModificationComponent;
  let fixture: ComponentFixture<RecipeModificationComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [RecipeModificationComponent],
    }).compileComponents();

    fixture = TestBed.createComponent(RecipeModificationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
