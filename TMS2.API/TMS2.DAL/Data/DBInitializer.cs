using Microsoft.EntityFrameworkCore;
using TMS2.DAL.Models;
using TMS2.DAL.Data;

namespace TMS2.DAL.Data;

public class DbInitializer
{
    public static void Initialize(Tms2Context context)
    {
        // context.Database.EnsureDeleted();
        // context.Database.EnsureCreated();
        context.Database.Migrate();

        context.SaveChanges();
    }
}