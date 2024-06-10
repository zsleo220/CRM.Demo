using CRM.Demo.App;
using CRM.Demo.Core;
using CRM.Demo.Infrastructure;
using CRM.Demo.Kernel;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddPisteService(builder.Configuration);
        builder.Services.AddLogging();

        var app = builder.Build();

        // Configure the HTTP request pipeline.

        app.UseHttpsRedirection();

        // Initialisation de la base de données
        app.MapGet("/initdb", async (DatabaseManagement dbManagement) =>
        {
            await dbManagement.InitDatabase();
            return "OK";
        });

        // GET endpoints
        app.MapGet("/pistes/{id}", async (int id, IPisteService pisteService) =>
        {
            var piste = await pisteService.GetPisteByIdAsync(id);
            return piste != null ? Results.Ok(piste) : Results.NotFound();
        });

        app.MapGet("/pistes/search", async (string searchTerm, IPisteService pisteService) =>
        {
            var pistes = await pisteService.SearchPistesAsync(searchTerm);
            return pistes != null ? Results.Ok(pistes) : Results.NoContent();
        });

        // POST endpoints
        app.MapPost("/pistes", async (Piste piste, IPisteService pisteService) =>
        {
            var createdPiste = await pisteService.CreatePisteAsync(piste);
            return createdPiste != null ? Results.Ok(createdPiste) : Results.BadRequest();
        });


        // PUT endpoints
        app.MapPut("/pistes/{id}", async (int id, Piste piste, IPisteService pisteService) =>
        {
            if (id != piste.Id) return Results.BadRequest();
            var updatedPiste = await pisteService.UpdatePisteAsync(piste);
            return updatedPiste != null ? Results.Ok(updatedPiste) : Results.BadRequest();
        });

        // DELETE endpoints
        app.MapDelete("/pistes/{id}", async (int id, IPisteService pisteService) =>
        {
            var deletedPiste = await pisteService.DeletePisteAsync(id);
            return deletedPiste != null ? Results.Ok(deletedPiste) : Results.NotFound();
        });

        // Other endpoints
        app.MapPost("/pistes/{pisteId}/assign", async (int pisteId, int collaborateurId, IPisteService pisteService) =>
        {
            await pisteService.AssignPisteAsync(pisteId, collaborateurId);
            return Results.Ok();
        });

        app.MapPost("/pistes/{pisteId}/start", async (int pisteId, IPisteService pisteService) =>
        {
            await pisteService.StartPisteAasync(pisteId);
            return Results.Ok();
        });

        app.MapPost("/pistes/{pisteId}/convert", async (int pisteId, IPisteService pisteService) =>
        {
            await pisteService.ConvertToProspectAsync(pisteId);
            return Results.Ok();
        });

        app.MapPost("/pistes/{pisteId}/markAsLost", async (int pisteId, IPisteService pisteService) =>
        {
            await pisteService.MarkAsLostAsync(pisteId);
            return Results.Ok();
        });

        app.MapPost("/pistes/{pisteId}/createEvent", async (int pisteId, string eventDetails, IPisteService pisteService) =>
        {
            await pisteService.CreateEventFromPisteAsync(pisteId, eventDetails);
            return Results.Ok();
        });


        app.Run();
    }
}