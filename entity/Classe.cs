namespace GestionEtudiant.Entity
{
    public class Classe
    {
        public int Id { get; set; }
        public string Libelle { get; set; } = string.Empty;
        public override string ToString()
        {
            return $"Classe [Id={Id}, Libelle={Libelle}]";
        }

    }
}