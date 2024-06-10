namespace CRM.Demo.Kernel
{
    public enum PisteType
    {
        Societe,
        Individu
    }

    public enum PisteStatut
    {
        Nouveau,
        Assigne,
        EnCours,
        ConvertiGagne,
        ConvertiPerdu
    }

    public class Piste
    {
        public int Id { get; set; }
        public PisteType Type { get; set; }
        public string? Denomination { get; set; } // Si Société
        public string? Civilite { get; set; } // Si Individu
        public string? Nom { get; set; } // Si Individu
        public string? Prenom { get; set; } // Si Individu

        public string? Adresse { get; set; }
        public string? CodePostale { get; set; }
        public string? Ville { get; set; }
        public string? Region { get; set; }
        public string? Pays { get; set; }
        public string? TelephoneFixe { get; set; }
        public string? Fax { get; set; }
        public string? Email { get; set; }
        public string? SiteWeb { get; set; }
        public string? Categorie { get; set; }
        public string? SecteurActivite { get; set; }
        public string? Provenance { get; set; }

        public PisteStatut Statut { get; set; }
        public int AssignedTo { get; set; }
    }
}
