// Classe CreateOrderCommand
export class CreateOrderCommand {
    number: number;
    description: string;
    value: number;
    orderAddress: OrderAddress;
  
    constructor(
      number: number,
      description: string,
      value: number,
      orderAddress: OrderAddress
    ) {
      this.number = number;
      this.description = description;
      this.value = value;
      this.orderAddress = orderAddress;
    }
  }
  
  export class OrderAddress {
    id: string;
    cep: string;
    rua: string;
    numero: string;
    bairro: string;
    cidade: string;
    estado: string;
  
    constructor(
      id: string,
      cep: string,
      rua: string,
      numero: string,
      bairro: string,
      cidade: string,
      estado: string
    ) {
      this.id = id;
      this.cep = cep;
      this.rua = rua;
      this.numero = numero;
      this.bairro = bairro;
      this.cidade = cidade;
      this.estado = estado;
    }
  }
  