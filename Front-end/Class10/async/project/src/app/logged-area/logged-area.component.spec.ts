import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LoggedAreaComponent } from './logged-area.component';

describe('LoggedAreaComponent', () => {
  let component: LoggedAreaComponent;
  let fixture: ComponentFixture<LoggedAreaComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ LoggedAreaComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(LoggedAreaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
