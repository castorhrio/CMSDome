using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace CMSDAL
{
    public class ContextFactory
    {
        public static CMSDbContext GetCurrentContext()
        {
            CMSDbContext _nContext = CallContext.GetData("CMSContext") as CMSDbContext;
            if (_nContext == null)
            {
                _nContext = new CMSDbContext();
                CallContext.SetData("CMSContext",_nContext);
            }

            return _nContext;
        }
    }
}
