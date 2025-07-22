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
    public class BrandRepository : Repository<Brand>, IBrandRepository
    {
        private ApplicationDbContext _db;

        public BrandRepository(ApplicationDbContext db): base(db)
        {
            _db = db;
        }
        public void Update(Brand obj)
        {
            _db.tblBrands.Update(obj);
       
        }
    }
}
