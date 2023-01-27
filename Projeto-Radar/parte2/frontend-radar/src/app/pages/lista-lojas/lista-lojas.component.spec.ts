import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ListaLojasComponent } from './lista-lojas.component';

describe('ListaLojasComponent', () => {
  let component: ListaLojasComponent;
  let fixture: ComponentFixture<ListaLojasComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ListaLojasComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ListaLojasComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
