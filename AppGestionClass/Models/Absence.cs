using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AppGestionClass.Models
{
    public class Absence
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        // Propriétés de clé étrangère pour les relations
        public int EtudiantId { get; set; }
        public int FiliereId { get; set; }
        public int LessonId { get; set; }
        public int statut { get; set; }
        public String dateAbsence { get; set;}
        // Propriétés de navigation pour les relations
        [ForeignKey("EtudiantId")]
        public Etudiant Etudiant { get; set; }

        [ForeignKey("FiliereId")]
        public Filiere Filiere { get; set; } // Propriété de navigation vers Filiere

        [ForeignKey("LessonId")]
        public Lesson Lesson { get; set; }

    }
}
