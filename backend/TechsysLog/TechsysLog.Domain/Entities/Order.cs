using TechsysLog.Domain.Enums;

namespace TechsysLog.Domain.Entities
{
    public class Order
    {
        public Order()
        {
        }

        public Order(int number, string description, decimal value, OrderAddress orderAddress)
        {
            Number = number;
            Description = description;
            Value = value;
            OrderAddress = orderAddress;
        }

        public string Id { get; set; }
        public int Number { get; set; }
        public string Description { get; set; }
        public decimal Value { get; set; }
        public OrderStatusEnum Status { get; set; }
        public OrderAddress OrderAddress { get; set; }

        public void SetOpenStatus()
        {
            Status = OrderStatusEnum.ABERTO;
        }

        public void SetAsDelivered()
        {
            Status = OrderStatusEnum.ENTREGUE;
        }


    }
}
