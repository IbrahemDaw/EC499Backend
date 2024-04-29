using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.DataSeed
{
    public static class RoleSeed
    {
        public static List<Role> GetRoleSeeds()
        {
            return new List<Role>
            { new Role
                    {
                        Id = 1,
                        CreatedAt = DateTime.Now,
                        IsDeleted = false,
                        IsEnabled = true,
                        Name = "مدير النضام",
                        UpdatedAt = DateTime.Now,
                    }

            };
        }
    }
}
