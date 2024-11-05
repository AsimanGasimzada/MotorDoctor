using MotorDoctor.DataAccess.DataInitializers;

namespace MotorDoctor.Presentation.Extensions;

public static class ExtensionMethods
{
    public static async Task InitDatabaseAsync(this WebApplication app)
    {
        using (var scope = app.Services.CreateScope())
        {
            var initializer = scope.ServiceProvider.GetRequiredService<DbContextInitalizer>();
            await initializer.InitDatabaseAsync();
        }
    }
}
