using eUDrive.Domains.Entities;
using Microsoft.EntityFrameworkCore;

namespace eUDrive.DataAccess
{
    public class DbSession : DbContext
    {
        public DbSession(DbContextOptions<DbSession> options) : base(options)
        {
        }

        public DbSet<UserData> Users
        {
            get { return Set<UserData>(); }
        }
    }
}
