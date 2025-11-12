namespace GestionEtudiant.Entity
{
    public class Note
    {
      
        public Etudiant Etudiant { get; set; } = new();
        public decimal Valeur { get; set; }
        public string Matiere { get; set; } = string.Empty;
        
    }
}
