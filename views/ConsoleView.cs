namespace GestionEtudiant.Views
{
    using System;
    using GestionEtudiant.Entity;
    using GestionEtudiant.Services;

    public class ConsoleView
    {
        private readonly IEtudiantService etudiantService;
        private readonly INoteService noteService;

        public ConsoleView(IEtudiantService etudiantService, INoteService noteService)
        {
            this.etudiantService = etudiantService;
            this.noteService = noteService;
        }

        public void Run()
        {
            while (true)
            {
                Console.WriteLine("=========== MENU PRINCIPAL ===========");
                Console.WriteLine("1. Ajouter un etudiant");
                Console.WriteLine("2. Afficher les Etudiants");
                Console.WriteLine("3. Ajouter une note à un etudiant");
                Console.WriteLine("4. Afficher les notes d'un etudiant avec l'appreciation");
                Console.WriteLine("5. Supprimer un Etudiant");
                Console.WriteLine("6. Afficher le Meilleur etudiant");
                Console.WriteLine("7. Afficher la moyenne de la classe");
                Console.WriteLine("8. Quitter");
                Console.Write("Faites votre choix: ");

                var choix = Console.ReadLine();

                switch (choix)
                {
                    case "1":
                        var etudiant = CreateEtudiant();
                        this.etudiantService.AjouterEtudiant(etudiant.Nom, etudiant.Prenom);
                        Console.WriteLine("Étudiant ajouté avec succès !");
                        break;

                    case "2":
                        AfficherEtudiants();
                        break;

                    case "3":
                        AjouterNote();
                        break;

                    case "4":
                        AfficherNotesEtudiant();
                        break;

                    case "5":
                        SupprimerEtudiant();
                        break;

                    case "6":
                        AfficherMeilleurEtudiant();
                        break;

                    case "7":
                        AfficherMoyenneClasse();
                        break;

                    case "8":
                        Console.WriteLine("Au revoir !");
                        return;

                    default:
                        Console.WriteLine("Veuillez choisir une option valide.");
                        break;
                }

                Console.WriteLine(); // ligne vide pour espacer le menu
            }
        }

        private void AfficherEtudiants()
        {
            var etudiants = this.etudiantService.ListerEtudiants();
                if (etudiants.Count == 0)
                Console.WriteLine("Aucun étudiant disponible.");
            else
            {
                Console.WriteLine("Liste des étudiants :");
                    foreach (var e in etudiants)
                        Console.WriteLine($"[{e.Id}] {e.Nom} {e.Prenom}");
            }
        }

        private void AjouterNote()
        {
            AfficherEtudiants();
            Console.Write("Entrer l'id de l'étudiant : ");
            int etudiantId = int.Parse(Console.ReadLine() ?? "0");
            var etudiant = this.etudiantService.GetEtudiantById(etudiantId);

            if (etudiant == null)
            {
                Console.WriteLine("⚠️ Étudiant introuvable !");
                return;
            }

            // La note n'a pas d'id — on demande seulement la matière et la valeur
            Console.Write("Entrer la matière : ");
            string matiere;
            do
            {
                matiere = Console.ReadLine() ?? string.Empty;
            } while (matiere.Length == 0);

            Console.Write("Entrer la valeur de la note : ");
            decimal valeur = decimal.Parse(Console.ReadLine() ?? "0");

            this.noteService.AjouterNote(etudiantId, valeur, matiere);
            Console.WriteLine("Note ajoutée avec succès !");
        }

        private void AfficherNotesEtudiant()
        {
            AfficherEtudiants();
            Console.Write("Entrer l'id de l'étudiant : ");
            int etudiantId = int.Parse(Console.ReadLine() ?? "0");
            var etudiant = this.etudiantService.GetEtudiantById(etudiantId);

            if (etudiant == null)
            {
                Console.WriteLine("Étudiant introuvable.");
                return;
            }

            var notes = this.noteService.ListerNotesEtudiant(etudiantId);
            if (notes.Count == 0)
                Console.WriteLine($"Aucune note pour l'étudiant {etudiant.Nom} {etudiant.Prenom}.");
            else
            {
                Console.WriteLine($"\nNotes de {etudiant.Nom} {etudiant.Prenom} :");
                foreach (var note in notes)
                    Console.WriteLine($"  {note.Matiere} : {note.Valeur}/20 - {this.noteService.GetAppreciation(note.Valeur)}");

                decimal moyenne = (decimal)notes.Average(n => (double)n.Valeur);
                Console.WriteLine($"  Moyenne : {moyenne:F2}/20");
            }
        }

        private void SupprimerEtudiant()
        {
            AfficherEtudiants();
            Console.Write("Entrer l'id de l'étudiant à supprimer : ");
            int etudiantId = int.Parse(Console.ReadLine() ?? "0");
            var etudiant = this.etudiantService.GetEtudiantById(etudiantId);

            if (etudiant == null)
            {
                Console.WriteLine("Étudiant introuvable.");
                return;
            }

            this.etudiantService.SupprimerEtudiant(etudiantId);
            Console.WriteLine($"L'étudiant {etudiant.Nom} {etudiant.Prenom} a été supprimé avec succès !");
        }

        private void AfficherMeilleurEtudiant()
        {
            var meilleur = this.noteService.MeilleurEtudiant();
            if (meilleur == null)
            {
                Console.WriteLine("Aucun étudiant avec des notes disponible.");
                return;
            }

            var notes = this.noteService.ListerNotesEtudiant(meilleur.Id);
            decimal moyenne = (decimal)notes.Average(n => (double)n.Valeur);
            Console.WriteLine($"Meilleur étudiant : {meilleur.Nom} {meilleur.Prenom} (Moyenne : {moyenne:F2}/20)");
        }

        private void AfficherMoyenneClasse()
        {
            decimal moyenne = this.noteService.MoyenneClasse();
            Console.WriteLine($"Moyenne de la classe : {moyenne:F2}/20");
        }

        private Etudiant CreateEtudiant()
        {
            Console.Write("Entrer le nom : ");
            string nom;
            do
            {
                nom = Console.ReadLine() ?? string.Empty;
            } while (nom.Length == 0);

            Console.Write("Entrer le prénom : ");
            string prenom;
            do
            {
                prenom = Console.ReadLine() ?? string.Empty;
            } while (prenom.Length == 0);
            // Id non fourni (auto-incrémenté par le repository)
            return new Etudiant { Id = 0, Nom = nom, Prenom = prenom };
        }
    }
}
