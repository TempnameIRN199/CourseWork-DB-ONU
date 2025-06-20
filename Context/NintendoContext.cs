namespace CourseWork_DB_ONU.Context
{
    using Microsoft.EntityFrameworkCore;
    class NintendoContext : DbContext
    {
        public NintendoContext(DbContextOptions<NintendoContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
