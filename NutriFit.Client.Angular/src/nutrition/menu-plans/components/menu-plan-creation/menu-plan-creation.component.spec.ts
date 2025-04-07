import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MenuPlanCreationComponent } from './menu-plan-creation.component';

describe('MenuPlanCreationComponent', () => {
  let component: MenuPlanCreationComponent;
  let fixture: ComponentFixture<MenuPlanCreationComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [MenuPlanCreationComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(MenuPlanCreationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
