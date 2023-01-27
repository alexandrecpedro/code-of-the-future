import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ListaCampanhasComponent } from './lista-campanhas.component';

describe('ListaCampanhasComponent', () => {
  let component: ListaCampanhasComponent;
  let fixture: ComponentFixture<ListaCampanhasComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ListaCampanhasComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ListaCampanhasComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
