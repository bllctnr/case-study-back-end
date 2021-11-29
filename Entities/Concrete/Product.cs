using Entities.Abstract;

namespace Entities
{
    public class Product : IEntity
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int StockAmount { get; set; }
    }
}
