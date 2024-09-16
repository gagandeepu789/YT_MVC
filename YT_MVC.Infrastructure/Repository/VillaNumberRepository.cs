﻿using Microsoft.EntityFrameworkCore;
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
    public class VillaNumberRepository :Repository<VillaNumber>,IVillaNumberRepository
    {
        private readonly ApplicationDbContext _db;
        public VillaNumberRepository(ApplicationDbContext db):base(db) 

        {
            _db = db;
        }


        public void Update(VillaNumber entity)
        {
           _db.VillaNumbers.Update(entity);
        }
    }
}
