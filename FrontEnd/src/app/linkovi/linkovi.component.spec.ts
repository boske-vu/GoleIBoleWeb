import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { LinkoviComponent } from './linkovi.component';

describe('LinkoviComponent', () => {
  let component: LinkoviComponent;
  let fixture: ComponentFixture<LinkoviComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ LinkoviComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(LinkoviComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
