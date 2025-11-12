using System.Collections.Generic;
using GestionEtudiant.Entity;

namespace GestionEtudiant.Services
{
    public interface IEtudiantService
    {
    // L'id est auto-incrémenté par le repository, on ne le fournit pas ici
    void AjouterEtudiant(string nom, string prenom);
        List<Etudiant> ListerEtudiants();
        Etudiant GetEtudiantById(int id);
        void SupprimerEtudiant(int id);
    }
}
