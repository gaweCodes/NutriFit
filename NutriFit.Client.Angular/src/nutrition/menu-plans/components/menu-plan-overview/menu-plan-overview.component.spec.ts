import { ComponentFixture, TestBed } from '@angular/core/testing';
import { MenuPlanOverviewComponent } from './menu-plan-overview.component';

describe('MenuPlanOverviewComponent', () => {
  let component: MenuPlanOverviewComponent;
  let fixture: ComponentFixture<MenuPlanOverviewComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [MenuPlanOverviewComponent],
    }).compileComponents();

    fixture = TestBed.createComponent(MenuPlanOverviewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
