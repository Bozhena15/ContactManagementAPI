using ContactManagementAPI.Data;
using Microsoft.EntityFrameworkCore;

namespace ContactManagementAPI;

public static class Extensions
{
    public static IServiceCollection AddServices(this WebApplicationBuilder builder)
    {
        builder.Services
            .AddPooledDbContextFactory<AppDbContext>(
                options => options.UseSqlServer(builder.Configuration.GetConnectionString("SqlConnection"),
                builder => builder.CommandTimeout(60))
            .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking));

        builder.Services.AddTransient<IContactManagmentService, ContactManagmentService>();

        builder.Services.AddControllers();

        return builder.Services;
    }
}
