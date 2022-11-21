import { ComponentFixture, TestBed } from '@angular/core/testing';

import { NotifyClientsComponent } from './notify-clients.component';

describe('NotifyClientsComponent', () => {
  let component: NotifyClientsComponent;
  let fixture: ComponentFixture<NotifyClientsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ NotifyClientsComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(NotifyClientsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
