import { ChangeDetectionStrategy, Component, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterOutlet, RouterLink, RouterLinkActive } from '@angular/router';

interface MenuItem {
  id: string;
  label: string;
  icon: string;
  route?: string;
  items?: MenuItem[];
  expanded?: boolean;
}

@Component({
  selector: 'app-template',
  templateUrl: './template.component.html',
  styleUrls: ['./template.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush,
  imports: [CommonModule, RouterOutlet, RouterLink, RouterLinkActive]
})
export class TemplateComponent {
  protected readonly sidebarAbierto = signal(true);
  protected readonly menuExpandido = signal<string | null>(null);

  protected readonly menuItems: MenuItem[] = [
 {
      id: 'mantenimiento',
      label: 'Mantenimiento',
      icon: 'wrench',
      items: [
        { id: 'padre', label: 'Padre', icon: 'users', route: 'padre' },
        { id: 'hijo', label: 'Hijo', icon: 'shield', route: 'hijo' },
        { id: 'permisos', label: 'Permisos', icon: 'lock', route: 'permisos' },
        { id: 'estado-cliente', label: 'Estado Cliente', icon: 'lock', route: 'estado-cliente' },
        { id: 'juego-recreativo', label: 'Juego Recreativo', icon: 'lock', route: 'juego-recreativo' },
        { id: 'mascota', label: 'Mascota', icon: 'lock', route: 'mascota' }
      ]
    },
    {
      id: 'reporteria',
      label: 'Reportería',
      icon: 'bar-chart',
      items: [
        { id: 'reportes-ventas', label: 'Reportes de Ventas', icon: 'trending-up', route: 'reportes-ventas' },
        { id: 'reportes-estudiantes', label: 'Estudiantes', icon: 'book', route: 'estudiantes' },
        { id: 'reportes-asistencia', label: 'Asistencia', icon: 'calendar', route: 'asistencia' }
      ]
    },
    {
      id: 'accesos',
      label: 'Gestión de Accesos',
      icon: 'key',
      items: [
        { id: 'roles-permisos', label: 'Roles y Permisos', icon: 'settings', route: 'roles-permisos' },
        { id: 'auditoría', label: 'Auditoría', icon: 'activity', route: 'auditoria' }
      ]
    },
    {
      id: 'otros',
      label: 'Otros',
      icon: 'more-horizontal',
      items: [
        { id: 'configuración', label: 'Configuración', icon: 'sliders', route: 'configuracion' },
        { id: 'backups', label: 'Backups', icon: 'save', route: 'backups' }
      ]
    }
  ];

  protected ToggleSidebar(): void {
    this.sidebarAbierto.update(v => !v);
  }

  protected ToggleMenu(menuId: string): void {
    this.menuExpandido.update(id => (id === menuId ? null : menuId));
  }

  protected EstaMenuExpandido(menuId: string): boolean {
    return this.menuExpandido() === menuId;
  }
}
