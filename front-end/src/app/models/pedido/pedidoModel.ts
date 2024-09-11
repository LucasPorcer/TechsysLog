import { OrderStatusEnum } from "src/app/enums/OrderStatusEnum";
import { EnderecoModel } from "../entrega/enderecoModel";

export class PedidoModel {
  number!: number;
  description!: string;
  value!: number;
  status!: OrderStatusEnum;
  orderAddress?: EnderecoModel;
}
