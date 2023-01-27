import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UpdateFormComponent } from './update-form.component';

describe('UpdateFormComponent', () => {
  let component: UpdateFormComponent;
  let fixture: ComponentFixture<UpdateFormComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ UpdateFormComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(UpdateFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
