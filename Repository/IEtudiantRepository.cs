using System.Collections.Generic;
using GestionEtudiant.Entity;

namespace GestionEtudiant.Repository
{
    public interface IEtudiantRepository
    {
        void Ajouter(Etudiant etudiant);
        List<Etudiant> Lister();
        Etudiant GetById(int id);
        void Supprimer(int id);
    }
}
