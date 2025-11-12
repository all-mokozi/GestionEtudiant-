using System.Collections.Generic;
using GestionEtudiant.Entity;

namespace GestionEtudiant.Repository
{
    public interface INoteRepository
    {
        void Ajouter(Note note);
        List<Note> Lister();
        List<Note> ListerParEtudiant(int etudiantId);
        Etudiant EtudiantMeilleur();
    }
}
