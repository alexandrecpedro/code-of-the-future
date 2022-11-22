import { TestBed } from '@angular/core/testing';

import { PedidosServicoService } from './pedidos-servico.service';

describe('PedidosServicoService', () => {
  let service: PedidosServicoService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(PedidosServicoService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
