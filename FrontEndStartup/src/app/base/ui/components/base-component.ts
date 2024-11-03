import { Directive, Input, OnInit } from '@angular/core';
import { BaseComponentInteractor } from '../interactors/base-component-intercator';

@Directive()
export class BaseComponent<Intercator extends BaseComponentInteractor> implements OnInit {
  
  @Input() interactor!: BaseComponentInteractor;

  constructor() {}

  ngOnInit(): void {
    this.interactor.setControl(this);
  }
}
