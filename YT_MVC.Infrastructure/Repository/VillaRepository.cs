using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using YT_MVC.Application.Common.Interfaces;
using YT_MVC.Domain.Entities;
using YT_MVC.Infrastructure.Data;

namespace YT_MVC.Infrastructure.Repository
{
    public class VillaRepository :Repository<Villa>,IVillaRepository
    {
        private readonly ApplicationDbContext _db;
        public VillaRepository(ApplicationDbContext db):base(db) 

        {
            _db = db;
        }

        public void Save()
        {
            _db.SaveChanges();  
        }

        public void Update(Villa entity)
        {
           _db.Villas.Update(entity);
        }
    }
}
