using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CMS.IDAL;
using CMS.Models;

namespace CMSDAL
{
    class UserRepository:BaseRepository<User>,InterfaceUserRepository
    {
    }
}
