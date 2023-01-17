using Microsoft.EntityFrameworkCore;
using TMS2.DAL.Models;
using TMS2.DAL.Data;

namespace Prediction.DAL.Data;

public class DBInitializer
{
    public static void Initialize(Tms2Context context)
    {
        //context.Database.EnsureDeleted();
        context.Database.EnsureCreated();
        context.Database.Migrate();

        context.SaveChanges();
    }
}