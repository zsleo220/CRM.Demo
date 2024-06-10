using CRM.Demo.Kernel;

namespace CRM.Demo.Core;

public interface IPisteService
{
    Task<Piste?> CreatePisteAsync(Piste piste);
    Task<Piste?> UpdatePisteAsync(Piste piste);
    Task<Piste?> DeletePisteAsync(int pisteId);
    Task<Piste?> GetPisteByIdAsync(int pisteId);
    Task<IEnumerable<Piste>?> SearchPistesAsync(string searchTerm);
    Task AssignPisteAsync(int pisteId, int collaborateurId);
    Task StartPisteAasync(int pisteId);
    Task ConvertToProspectAsync(int pisteId);
    Task MarkAsLostAsync(int pisteId);
    Task CreateEventFromPisteAsync(int pisteId, string eventDetails);
}

