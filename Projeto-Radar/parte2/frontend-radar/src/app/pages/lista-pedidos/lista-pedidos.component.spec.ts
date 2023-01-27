import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ListaPedidosComponent } from './lista-pedidos.component';

describe('ListaPedidosComponent', () => {
  let component: ListaPedidosComponent;
  let fixture: ComponentFixture<ListaPedidosComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ListaPedidosComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ListaPedidosComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
