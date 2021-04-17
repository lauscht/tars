import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MarkdownboxComponent } from './markdownbox.component';

describe('MarkdownboxComponent', () => {
  let component: MarkdownboxComponent;
  let fixture: ComponentFixture<MarkdownboxComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ MarkdownboxComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(MarkdownboxComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
