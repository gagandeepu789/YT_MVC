using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YT_MVC.Domain.Entities;

namespace YT_MVC.Application.Common.Interfaces
{
    public interface IVillaNumberRepository: IRepository<VillaNumber>
    {
        void Update(VillaNumber entity);
    }
}
