using System.Collections.Generic;
using System.Linq;
using GestionEtudiant.Entity;

namespace GestionEtudiant.Repository.Impl
{
    public class NoteRepository : INoteRepository
    {
        private readonly List<Note> notes = new();

        public void Ajouter(Note note) => notes.Add(note);

        public List<Note> Lister() => notes;

        public List<Note> ListerParEtudiant(int etudiantId)
            => notes.Where(n => n.Etudiant.Id == etudiantId).ToList();

        public Etudiant EtudiantMeilleur()
        {
            var notesGroupees = notes.GroupBy(n => n.Etudiant.Id);
            if (!notesGroupees.Any()) return null;
            
            var meilleurEtudiant = notesGroupees
                .OrderByDescending(g => g.Average(n => n.Valeur))
                .FirstOrDefault()
                ?.First()
                .Etudiant;
            
            return meilleurEtudiant;
        }
    }
}
