/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { EstadoClienteService } from './EstadoCliente.service';

describe('Service: EstadoCliente', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [EstadoClienteService]
    });
  });

  it('should ...', inject([EstadoClienteService], (service: EstadoClienteService) => {
    expect(service).toBeTruthy();
  }));
});
