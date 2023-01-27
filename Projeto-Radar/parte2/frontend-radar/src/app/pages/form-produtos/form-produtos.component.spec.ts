import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FormProdutosComponent } from './form-produtos.component';

describe('FormProdutosComponent', () => {
  let component: FormProdutosComponent;
  let fixture: ComponentFixture<FormProdutosComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FormProdutosComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(FormProdutosComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
