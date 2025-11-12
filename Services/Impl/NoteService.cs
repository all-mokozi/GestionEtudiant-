using System.Collections.Generic;
using System.Linq;
using GestionEtudiant.Entity;
using GestionEtudiant.Repository;

namespace GestionEtudiant.Services.Impl
{
    public class NoteService : INoteService
    {
        private readonly INoteRepository _noteRepo;
        private readonly IEtudiantRepository _etudiantRepo;

        public NoteService(INoteRepository noteRepo, IEtudiantRepository etudiantRepo)
        {
            _noteRepo = noteRepo;
            _etudiantRepo = etudiantRepo;
        }

        public void AjouterNote(int etudiantId, decimal valeur, string matiere)
        {
            var etudiant = _etudiantRepo.GetById(etudiantId);
            if (etudiant != null)
            {
                _noteRepo.Ajouter(new Note
                {
                    Etudiant = etudiant,
                    Valeur = valeur,
                    Matiere = matiere
                });
            }
        }

        public List<Note> ListerNotes() => _noteRepo.Lister();

        public List<Note> ListerNotesEtudiant(int etudiantId) => _noteRepo.ListerParEtudiant(etudiantId);

        public Etudiant MeilleurEtudiant() => _noteRepo.EtudiantMeilleur();

        public decimal MoyenneClasse()
        {
            var notes = _noteRepo.Lister();
            if (notes.Count == 0) return 0;
            return (decimal)notes.Average(n => (double)n.Valeur);
        }

        // Fournit l'appréciation pour une valeur de note (logique conditionnelle ici dans le service)
        public string GetAppreciation(decimal valeur)
        {
            if (valeur >= 16) return "Excellent";
            if (valeur >= 14) return "Très Bien";
            if (valeur >= 12) return "Bien";
            if (valeur >= 10) return "Satisfaisant";
            return "À améliorer";
        }
    }
}
