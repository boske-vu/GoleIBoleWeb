import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { RegistrationCommandComponent } from './registration-command.component';

describe('RegistrationCommandComponent', () => {
  let component: RegistrationCommandComponent;
  let fixture: ComponentFixture<RegistrationCommandComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ RegistrationCommandComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(RegistrationCommandComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
