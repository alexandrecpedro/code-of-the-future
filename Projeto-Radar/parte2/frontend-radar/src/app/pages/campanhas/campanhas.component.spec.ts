import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CampanhasComponent } from './campanhas.component';

describe('CampanhasComponent', () => {
  let component: CampanhasComponent;
  let fixture: ComponentFixture<CampanhasComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CampanhasComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CampanhasComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
