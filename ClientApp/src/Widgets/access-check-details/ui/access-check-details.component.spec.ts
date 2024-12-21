import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AccessCheckDetailsComponent } from './access-check-details.component';

describe('AccessCheckDetailsComponent', () => {
  let component: AccessCheckDetailsComponent;
  let fixture: ComponentFixture<AccessCheckDetailsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [AccessCheckDetailsComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(AccessCheckDetailsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
