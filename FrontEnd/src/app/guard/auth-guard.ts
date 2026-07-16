import { CanActivateFn, Router } from '@angular/router';
import { inject } from '@angular/core';
import { AuthService } from '../views/login/services/auth.service';

export const authGuard: CanActivateFn = (route, state) => {
  const authService = inject(AuthService);
  const router = inject(Router);

  return authService.EstaAutenticado().then((estaAutenticado) => {
    if (estaAutenticado) {
      return true;
    }

    void router.navigate(['/'], { queryParams: { returnUrl: state.url } });
    return false;
  });
};
