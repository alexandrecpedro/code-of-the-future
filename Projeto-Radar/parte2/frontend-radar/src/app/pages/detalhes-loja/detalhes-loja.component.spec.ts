import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DetalhesLojaComponent } from './detalhes-loja.component';

describe('DetalhesLojaComponent', () => {
  let component: DetalhesLojaComponent;
  let fixture: ComponentFixture<DetalhesLojaComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ DetalhesLojaComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(DetalhesLojaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
