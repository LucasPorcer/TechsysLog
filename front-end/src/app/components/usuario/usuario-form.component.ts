import { Component } from '@angular/core';
import { UsuarioModel } from 'src/app/models/usuario/usuarioModel';
import { ApiService } from 'src/app/services/api.service';

@Component({
  selector: 'app-usuario-form',
  templateUrl: './usuario-form.component.html',
  styleUrls: ['./usuario-form.component.css']
})
export class UsuarioFormComponent {
  usuario: UsuarioModel = {
    name: '',
    email: '',
    password: ''
  };

  constructor(private apiService: ApiService) {}

  cadastrarUsuario() {
    this.apiService.cadastrarUsuario(this.usuario).subscribe(res => {
      console.log('Usuário cadastrado com sucesso!', res);
    });
  }
}
