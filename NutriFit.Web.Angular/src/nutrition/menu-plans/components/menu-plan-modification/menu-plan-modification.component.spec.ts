import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MenuPlanModificationComponent } from './menu-plan-modification.component';

describe('MenuPlanModificationComponent', () => {
  let component: MenuPlanModificationComponent;
  let fixture: ComponentFixture<MenuPlanModificationComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [MenuPlanModificationComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(MenuPlanModificationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
