import { ComponentFixture, TestBed } from '@angular/core/testing';
import { MenuPlansOverviewComponent } from './menu-plans-overview.component';

describe('MenuPlansOverviewComponent', () => {
  let component: MenuPlansOverviewComponent;
  let fixture: ComponentFixture<MenuPlansOverviewComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [MenuPlansOverviewComponent],
    }).compileComponents();

    fixture = TestBed.createComponent(MenuPlansOverviewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
