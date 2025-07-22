using EcommerceAutoPart.DataAccess.Data;
using EcommerceAutoPart.DataAccess.Repository.IRepository;
using EcommerceAutoPart.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAutoPart.DataAccess.Repository
{
    public class OrderDetailRepository : Repository<OrderDetail>, IOrderDetailRepository
    {
        private ApplicationDbContext _db;

        public OrderDetailRepository(ApplicationDbContext db): base(db)
        {
            _db = db;
        }
        public void Update(OrderDetail obj)
        {
            _db.tblOrderDetails.Update(obj);
       
        }
    }
}
