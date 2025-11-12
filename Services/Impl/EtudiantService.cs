using System.Collections.Generic;
using GestionEtudiant.Entity;
using GestionEtudiant.Repository;

namespace GestionEtudiant.Services.Impl
{
    public class EtudiantService : IEtudiantService
    {
        private readonly IEtudiantRepository _etudiantRepo;

        public EtudiantService(IEtudiantRepository etudiantRepo)
        {
            _etudiantRepo = etudiantRepo;
        }

        public void AjouterEtudiant(string nom, string prenom)
        {
            // L'id sera attribué automatiquement par le repository si nécessaire
            _etudiantRepo.Ajouter(new Etudiant { Id = 0, Nom = nom, Prenom = prenom });
        }

        public List<Etudiant> ListerEtudiants() => _etudiantRepo.Lister();

        public Etudiant GetEtudiantById(int id) => _etudiantRepo.GetById(id);

        public void SupprimerEtudiant(int id) => _etudiantRepo.Supprimer(id);
    }
}
