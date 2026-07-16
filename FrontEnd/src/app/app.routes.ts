import { Routes } from '@angular/router';
import { authGuard } from './guard/auth-guard';

export const routes: Routes = [
  { path: '', loadComponent: () => import('./views/login/login.component').then(m => m.LoginComponent) },
  {
    path: 'dashboard',
    canActivate: [authGuard],
    loadComponent: () => import('./views/template/template.component').then(m => m.TemplateComponent),
    children: [
      { path: 'padre', loadComponent: () => import('./views/padre/padre').then(m => m.Padre) },
      { path: 'hijo', loadComponent: () => import('./views/hijo/hijo').then(m => m.Hijo) },
      { path: 'estado-cliente', loadComponent: () => import('./views/template/mantenimiento/estado-cliente/estado-cliente.component').then(m => m.EstadoClienteComponent)  }
    ]
  }
];
