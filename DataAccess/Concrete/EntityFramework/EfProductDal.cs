using DataAccess.Abstract;
using Entities;
using Entities.Dtos;
using System.Collections.Generic;
using System.Linq;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfProductDal : EfEntityRepositoryBase<Product, CaseStudyContext>, IProductDal
    {

    }
}
