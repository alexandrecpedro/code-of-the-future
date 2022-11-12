import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DetailContactComponent } from './detail-contact.component';

describe('DetailContactComponent', () => {
  let component: DetailContactComponent;
  let fixture: ComponentFixture<DetailContactComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ DetailContactComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(DetailContactComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
