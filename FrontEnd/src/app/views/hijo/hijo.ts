import { Component, input, output, signal } from '@angular/core';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-hijo',
  imports: [FormsModule],
  templateUrl: './hijo.html',
  styleUrls: ['./hijo.scss'],
})
export class Hijo {


  cantidad = input(0);
  personaEmmit = output<any>();

  data:any = signal({ id: 0, nombre: '', edad: 0 });



  isnertarPersona(){
    debugger;
    console.log("hizo clic en el boton");
    

    console.log(this.data());
    console.log("enviar datos al padre");
    
    this.personaEmmit.emit(this.data());

    this.data.set({ id: 0, nombre: '', edad: 0 });

    console.log(this.data());
  }

}
