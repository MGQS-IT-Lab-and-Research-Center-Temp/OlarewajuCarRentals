﻿namespace CarRentals.Context;

internal static class DbInitializerExtension
{
    public static IApplicationBuilder SeedToDatabase(this IApplicationBuilder app)
    {
        ArgumentNullException.ThrowIfNull(app, nameof(app));

        using var scope = app.ApplicationServices.CreateScope();
   
        var services = scope.ServiceProvider;

        try
        {
            var context = services.GetRequiredService<CarRentalsContext>();
            DbInitializer.Initialize(context);
        }
        catch (Exception)
        {

        }

        return app;
    }
}
