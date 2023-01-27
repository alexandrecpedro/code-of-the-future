import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FormComprarProdutoComponent } from './form-comprar-produto.component';

describe('FormComprarProdutoComponent', () => {
  let component: FormComprarProdutoComponent;
  let fixture: ComponentFixture<FormComprarProdutoComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FormComprarProdutoComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(FormComprarProdutoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
