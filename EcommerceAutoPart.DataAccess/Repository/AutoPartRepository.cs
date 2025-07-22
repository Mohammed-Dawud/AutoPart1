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
    public class AutoPartRepository : Repository<AutoPart>, IAutoPartRepository
    {
        private ApplicationDbContext _db;

        public AutoPartRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        public void Update(AutoPart obj)
        {
            _db.tblAutoParts.Update(obj);

        }
    }
}
