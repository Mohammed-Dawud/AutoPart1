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
    public class CarRepository : Repository<Car>, ICarRepository
    {
        private ApplicationDbContext _db;

        public CarRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        public void Update(Car obj)
        {
            _db.tblCars.Update(obj);

        }
    }
}
