using System.Collections.Generic;
using System.Linq;
using GestionEtudiant.Entity;

namespace GestionEtudiant.Repository.Impl
{
    public class EtudiantRepository : IEtudiantRepository
    {
        private readonly List<Etudiant> etudiants = new();
        private int nextId = 1;

        public void Ajouter(Etudiant etudiant)
        {
            // Assigner un Id auto-increment si l'appelant n'en fournit pas
            if (etudiant.Id == 0)
            {
                etudiant.Id = nextId++;
            }
            else
            {
                // si un Id est fourni, garantir que nextId le dépasse
                if (etudiant.Id >= nextId) nextId = etudiant.Id + 1;
            }

            etudiants.Add(etudiant);
        }

        public List<Etudiant> Lister() => etudiants;

        public Etudiant GetById(int id) => etudiants.FirstOrDefault(e => e.Id == id);

        public void Supprimer(int id)
        {
            var etudiant = etudiants.FirstOrDefault(e => e.Id == id);
            if (etudiant != null)
                etudiants.Remove(etudiant);
        }
    }
}
