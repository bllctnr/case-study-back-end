using Entities.Abstract;

namespace Entities
{
    public class OrderProduct : IEntity
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int OrderAmount { get; set; }
        public decimal Price { get; set; }
    }
}
