using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos
{
    public class OrderWithProductsForAddNewOrderDto : IDto
    {
        public int CustomerId { get; set; }
        public List<OrderProductDto> OrderProducts { get; set; }

    }
}
