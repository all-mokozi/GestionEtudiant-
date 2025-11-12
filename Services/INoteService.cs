using System.Collections.Generic;
using GestionEtudiant.Entity;

namespace GestionEtudiant.Services
{
    public interface INoteService
    {
        // Ajoute une note (pas d'id pour la note)
        void AjouterNote(int etudiantId, decimal valeur, string matiere);
        List<Note> ListerNotes();
        List<Note> ListerNotesEtudiant(int etudiantId);
        Etudiant MeilleurEtudiant();
        decimal MoyenneClasse();
        // Fournit l'appréciation pour une valeur de note
        string GetAppreciation(decimal valeur);
    }
}
