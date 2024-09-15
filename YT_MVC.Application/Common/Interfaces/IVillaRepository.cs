using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using YT_MVC.Domain.Entities;

namespace YT_MVC.Application.Common.Interfaces
{
    public interface IVillaRepository:IRepository<Villa>
    {
      
        void Update(Villa entity);
     
        void Save();

    }
}
