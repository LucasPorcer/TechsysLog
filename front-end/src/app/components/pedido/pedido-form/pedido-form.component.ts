import { Component, OnInit } from '@angular/core';
import { CreateOrderCommand, OrderAddress } from 'src/app/models/Requests/createOrderRequest';
import { ApiService } from 'src/app/services/api.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-pedido-form',
  templateUrl: './pedido-form.component.html',
  styleUrls: ['./pedido-form.component.css']
})
export class PedidoFormComponent implements OnInit {
  pedido: any = {
    numeroPedido: '',
    descricao: '',
    valor: '',
    enderecoEntrega: {
      cep: '',
      rua: '',
      numero: '',
      bairro: '',
      cidade: '',
      estado: ''
    }
  };

  constructor(private apiService: ApiService, private router: Router) {}

  ngOnInit() {
    this.pedido.valor = ''; // Remove o valor inicial 'R$'
  }

  formatarValor(): void {
    let valor = this.pedido.valor.replace(/[^\d,]/g, ''); // Remove qualquer caractere que não seja número ou vírgula
    
    if (valor) {
      valor = valor.replace(/(\d)(?=(\d{3})+(?!\d))/g, '$1.'); // Adiciona pontos de milhar
      this.pedido.valor = valor;
    } else {
      this.pedido.valor = ''; // Deixa o valor vazio
    }
  }

  buscarEndereco() {
    if (this.pedido.enderecoEntrega.cep) {
      this.apiService.buscarEndereco(this.pedido.enderecoEntrega.cep).subscribe(endereco => {
        this.pedido.enderecoEntrega.rua = endereco.logradouro;
        this.pedido.enderecoEntrega.bairro = endereco.bairro;
        this.pedido.enderecoEntrega.cidade = endereco.localidade;
        this.pedido.enderecoEntrega.estado = endereco.uf;
      });
    } else {
      this.limparCamposEndereco();
    }
  }

  limparCamposEndereco() {
    this.pedido.enderecoEntrega.rua = '';
    this.pedido.enderecoEntrega.bairro = '';
    this.pedido.enderecoEntrega.cidade = '';
    this.pedido.enderecoEntrega.estado = '';
  }

  permitirSomenteNumeros(event: any): void {
    const input = event.target as HTMLInputElement;
    input.value = input.value.replace(/[^0-9]/g, '');
    this.pedido.numeroPedido = input.value;
  }

  cadastrarPedido() {
    if (this.pedido.valor) {
      const valorConvertido = parseFloat(this.pedido.valor.replace(/\./g, '').replace(',', '.').trim());
    
      if (!isNaN(valorConvertido)) {
        this.pedido.valor = valorConvertido;
      } else {
        console.error('Erro ao converter o valor');
      }
    } else {
      console.error('Valor está vazio ou inválido');
    }
    
    const enderecoRequest = new OrderAddress(
      '',
      this.pedido.enderecoEntrega.cep,
      this.pedido.enderecoEntrega.rua,
      this.pedido.enderecoEntrega.numero,
      this.pedido.enderecoEntrega.bairro,
      this.pedido.enderecoEntrega.cidade,
      this.pedido.enderecoEntrega.estado
    );
   
    const request = new CreateOrderCommand(
      this.pedido.numeroPedido,
      this.pedido.descricao,
      this.pedido.valor,
      enderecoRequest
    );
   
    this.apiService.cadastrarPedido(request).subscribe(res => {
      console.log('Pedido cadastrado com sucesso!', res);
      this.router.navigate(['/pedidos']);
    });
  }
}
