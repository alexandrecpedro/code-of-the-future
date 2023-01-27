import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CarrinhoNotificacaoComponent } from './carrinho-notificacao.component';

describe('CarrinhoNotificacaoComponent', () => {
  let component: CarrinhoNotificacaoComponent;
  let fixture: ComponentFixture<CarrinhoNotificacaoComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CarrinhoNotificacaoComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CarrinhoNotificacaoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
