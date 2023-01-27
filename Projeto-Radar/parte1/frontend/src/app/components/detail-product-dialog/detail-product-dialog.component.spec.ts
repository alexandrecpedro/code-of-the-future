import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DetailProductDialogComponent } from './detail-product-dialog.component';

describe('DetailProductDialogComponent', () => {
  let component: DetailProductDialogComponent;
  let fixture: ComponentFixture<DetailProductDialogComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ DetailProductDialogComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(DetailProductDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
