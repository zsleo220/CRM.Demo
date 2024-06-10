using CRM.Demo.Core;
using CRM.Demo.Kernel;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;


namespace CRM.Demo.App
{
    public class PisteService : IPisteService
    {
        private readonly AppDbContext _context;
        private readonly ILogger<PisteService> _logger;

        public PisteService(AppDbContext context, ILogger<PisteService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<Piste?> CreatePisteAsync(Piste piste)
        {
            try
            {
                piste.Statut = PisteStatut.Nouveau;
                _context.Pistes.Add(piste);
                await _context.SaveChangesAsync();
                _logger.LogInformation($"Piste créée avec ID: {piste.Id}");
                return piste;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Une erreur est survenue lors de la création de la piste");
                return null;
            }
        }

        public async Task<Piste?> UpdatePisteAsync(Piste piste)
        {
            try
            {
                _context.Pistes.Update(piste);
                await _context.SaveChangesAsync();
                _logger.LogInformation($"Piste mise à jour avec ID: {piste.Id}");
                return piste;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Une erreur est survenue lors de la mise à jour de la piste");
                return null;
            }
        }

        public async Task<Piste?> DeletePisteAsync(int pisteId)
        {
            try
            {
                var piste = await _context.Pistes.FindAsync(pisteId);
                if (piste == null)
                {
                    _logger.LogWarning($"Piste non trouvée avec ID: {pisteId}");
                    return null;
                }
                _context.Pistes.Remove(piste);
                await _context.SaveChangesAsync();
                _logger.LogInformation($"Piste supprimée avec ID: {pisteId}");
                return piste;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Une erreur est survenue lors de la suppression de la piste");
                return null;
            }
        }

        public async Task<Piste?> GetPisteByIdAsync(int pisteId)
        {
            try
            {
                var piste = await _context.Pistes.FindAsync(pisteId);
                if (piste == null)
                {
                    _logger.LogWarning($"Piste non trouvée avec ID: {pisteId}");
                }
                return piste;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Une erreur est survenue lors de la récupération de la piste par ID");
                return null;
            }
        }

        public async Task<IEnumerable<Piste>?> SearchPistesAsync(string searchTerm)
        {
            try
            {
                _logger.LogInformation("Recherche de pistes avec le terme : {SearchTerm}", searchTerm);
                var result = await _context.Pistes
                    .Where(p => p.Denomination.Contains(searchTerm) ||
                                p.Civilite.Contains(searchTerm) ||
                                p.Nom.Contains(searchTerm) ||
                                p.Prenom.Contains(searchTerm) ||
                                p.Adresse.Contains(searchTerm) ||
                                p.CodePostale.Contains(searchTerm) ||
                                p.Ville.Contains(searchTerm) ||
                                p.Region.Contains(searchTerm) ||
                                p.Pays.Contains(searchTerm) ||
                                p.TelephoneFixe.Contains(searchTerm) ||
                                p.Fax.Contains(searchTerm) ||
                                p.Email.Contains(searchTerm) ||
                                p.SiteWeb.Contains(searchTerm) ||
                                p.Categorie.Contains(searchTerm) ||
                                p.SecteurActivite.Contains(searchTerm) ||
                                p.Provenance.Contains(searchTerm))
                    .ToListAsync();
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Une erreur est survenue lors de la recherche des pistes");
                return null;
            }
        }

        public async Task AssignPisteAsync(int pisteId, int collaborateurId)
        {
            try
            {
                var piste = await _context.Pistes.FindAsync(pisteId);
                if (piste != null)
                {
                    piste.AssignedTo = collaborateurId;
                    piste.Statut = PisteStatut.Assigne;
                    await _context.SaveChangesAsync();
                    _logger.LogInformation($"Piste avec ID: {pisteId} assignée au collaborateur avec ID: {collaborateurId}");
                }
                else
                {
                    _logger.LogWarning($"Piste non trouvée avec ID: {pisteId}");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Une erreur est survenue lors de l'assignation de la piste");
            }
        }

        public async Task StartPisteAasync(int pisteId)
        {
            try
            {
                var piste = await _context.Pistes.FindAsync(pisteId);
                if (piste != null)
                {
                    piste.Statut = PisteStatut.EnCours;
                    await _context.SaveChangesAsync();
                    _logger.LogInformation($"Piste avec ID: {pisteId} démarrée");
                }
                else
                {
                    _logger.LogWarning($"Piste non trouvée avec ID: {pisteId}");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Une erreur est survenue lors du démarrage de la piste");
            }
        }

        public async Task ConvertToProspectAsync(int pisteId)
        {
            try
            {
                var piste = await _context.Pistes.FindAsync(pisteId);
                if (piste != null)
                {
                    piste.Statut = PisteStatut.ConvertiGagne;
                    await _context.SaveChangesAsync();
                    _logger.LogInformation($"Piste avec ID: {pisteId} convertie en prospect");
                }
                else
                {
                    _logger.LogWarning($"Piste non trouvée avec ID: {pisteId}");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Une erreur est survenue lors de la conversion de la piste en prospect");
            }
        }

        public async Task MarkAsLostAsync(int pisteId)
        {
            try
            {
                var piste = await _context.Pistes.FindAsync(pisteId);
                if (piste != null)
                {
                    piste.Statut = PisteStatut.ConvertiPerdu;
                    await _context.SaveChangesAsync();
                    _logger.LogInformation($"Piste avec ID: {pisteId} marquée comme perdue");
                }
                else
                {
                    _logger.LogWarning($"Piste non trouvée avec ID: {pisteId}");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Une erreur est survenue lors du marquage de la piste comme perdue");
            }
        }

        public async Task CreateEventFromPisteAsync(int pisteId, string eventDetails)
        {
            try
            {
                var piste = await _context.Pistes.FindAsync(pisteId);
                if (piste != null)
                {
                    // Implémentation de la création d'un événement à partir de la piste
                    // Logique à ajouter selon le modèle de votre événement
                    _logger.LogInformation($"Événement créé à partir de la piste avec ID: {pisteId}");
                    await _context.SaveChangesAsync();
                }
                else
                {
                    _logger.LogWarning($"Piste non trouvée avec ID: {pisteId}");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Une erreur est survenue lors de la création d'un événement à partir de la piste");
            }
        }
    }
}
