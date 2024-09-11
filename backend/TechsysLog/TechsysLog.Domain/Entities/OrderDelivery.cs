namespace TechsysLog.Domain.Entities
{
    public class OrderDelivery
    {
        public OrderDelivery()
        {
        }

        public OrderDelivery(string orderId, int orderNumber)
        {
            OrderId = orderId;
            OrderNumber = orderNumber;
            Date = DateTime.Now;    
        }

        public string Id { get; set; }
        public string OrderId { get; set; }
        public int OrderNumber { get; set; }
        public DateTime Date { get; set; }
    }
}
