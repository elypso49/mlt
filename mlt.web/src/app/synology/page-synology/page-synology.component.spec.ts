import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PageSynologyComponent } from './page-synology.component';

describe('PageSynologyComponent', () => {
  let component: PageSynologyComponent;
  let fixture: ComponentFixture<PageSynologyComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [PageSynologyComponent]
    });
    fixture = TestBed.createComponent(PageSynologyComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
