using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CMS.Models;

namespace CMSDAL
{
    public class CMSDbContext:DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<UserConfig> UserConfigs { get; set; }

        public DbSet<Role> Roles { get; set; }

        public DbSet<UserRoleRelation> UserRoleRelations { get; set; }

        public CMSDbContext() : base("CMS")
        {
            Database.CreateIfNotExists();
        }
    }
}
