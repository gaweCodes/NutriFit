import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MenuPlanDetailComponent } from './menu-plan-detail.component';

describe('MenuPlanDetailComponent', () => {
  let component: MenuPlanDetailComponent;
  let fixture: ComponentFixture<MenuPlanDetailComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [MenuPlanDetailComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(MenuPlanDetailComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
