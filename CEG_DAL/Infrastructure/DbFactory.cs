using CEG_DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEG_DAL.Infrastructure
{
    public class DbFactory
    {
        private MyDBContext _dbContext;

        private DbFactory()
        {

        }

        private static DbFactory instance = null;
        private static readonly Object objectLock = new Object();
        public static DbFactory Instance
        {
            get
            {
                lock (objectLock)
                {
                    if (instance == null)
                    {
                        instance = new DbFactory();
                    }
                    return instance;
                }
            }
        }

        public MyDBContext InitDbContext()
        {
            if (_dbContext == null)
            {
                _dbContext = new MyDBContext();
            }
            return _dbContext;
        }
    }
}
