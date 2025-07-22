using EcommerceAutoPart.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAutoPart.DataAccess.Repository.IRepository
{
    public interface IAutoPartRepository : IRepository<AutoPart>
    {
        void Update(AutoPart obj);
    }
}
