using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CMS.IDAL;

namespace CMSDAL
{
    public class RepositoryFactory
    {
        public static InterfaceUserRepository UserRepository
        {
            get
            {
                return new UserRepository();
            }
        }
    }
}
