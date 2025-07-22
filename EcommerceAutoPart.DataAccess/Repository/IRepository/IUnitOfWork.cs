using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAutoPart.DataAccess.Repository.IRepository
{
    public interface IUnitOfWork
    {
        IBrandRepository Brand { get; }
        ICarRepository Car { get; }
        IAutoPartRepository AutoPart { get; }
        ICompanyRepository Company { get; }
        IShoppingCartRepository ShoppingCart { get; }
        IApplicationUserRepository ApplicationUser { get; }
        IOrderHeaderRepository OrderHeader { get; }
        IOrderDetailRepository OrderDetail { get; }
     
        void Save();
    }
}
