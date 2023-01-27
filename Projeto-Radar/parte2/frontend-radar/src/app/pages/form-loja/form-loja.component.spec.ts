import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FormLojaComponent } from './form-loja.component';

describe('FormLojaComponent', () => {
  let component: FormLojaComponent;
  let fixture: ComponentFixture<FormLojaComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FormLojaComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(FormLojaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
