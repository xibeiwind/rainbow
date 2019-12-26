import { Component, ElementRef, HostListener, Input, OnInit, ViewChild } from '@angular/core';

@Component({
  selector: 'app-img-previewable',
  templateUrl: './img-previewable.component.html',
  styleUrls: ['./img-previewable.component.scss']
})
export class ImgPreviewableComponent implements OnInit {

  @Input()
  src: string;

  protected showPreview: boolean = false;

  @ViewChild('thumbnail', { static: true })
  protected thumbnail: ElementRef;

  @ViewChild('previewImg', { static: true })
  protected preview: ElementRef;

  constructor() { }

  ngOnInit() {

  }

  @HostListener('mouseenter')
  onMouseEnter() {
    this.showPreview = true;
  }

  @HostListener('mouseleave')
  onMouseLeave() {
    this.showPreview = false;
  }

}
