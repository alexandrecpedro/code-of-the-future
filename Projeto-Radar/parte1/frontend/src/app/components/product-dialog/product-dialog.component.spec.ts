import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ProductDialogComponent } from './product-dialog.component';

describe('ProductDialogComponent', () => {
  let component: ProductDialogComponent;
  let fixture: ComponentFixture<ProductDialogComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ProductDialogComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ProductDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
