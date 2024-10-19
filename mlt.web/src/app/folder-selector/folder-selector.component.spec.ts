import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FolderSelectorComponent } from './folder-selector.component';

describe('FolderSelectorComponent', () => {
  let component: FolderSelectorComponent;
  let fixture: ComponentFixture<FolderSelectorComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [FolderSelectorComponent]
    });
    fixture = TestBed.createComponent(FolderSelectorComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
