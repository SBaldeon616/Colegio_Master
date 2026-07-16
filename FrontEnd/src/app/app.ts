import { Component, signal } from '@angular/core';
import { Padre } from './views/padre/padre';
import { Hijo } from './views/hijo/hijo';
import { RouterOutlet } from '@angular/router';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet],
  templateUrl: './app.html',
  styleUrls: ['./app.scss'] 
})
export class App {
  protected readonly title = signal('manejar');
}
