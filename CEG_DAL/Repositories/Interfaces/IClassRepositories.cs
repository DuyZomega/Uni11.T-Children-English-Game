﻿using CEG_DAL.Infrastructure;
using CEG_DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEG_DAL.Repositories.Interfaces
{
    public interface IClassRepositories : IRepositoryBase<Class>
    {
        Task<List<Class>> GetClassList();
        Task<List<Class>> GetClassListAdmin();
        Task<Class?> GetByIdNoTracking(int id);
        Task<List<Class>> GetClassListByTeacherId(int teacherId);
        Task<int> GetIdByClassId(int id);
    }
}
