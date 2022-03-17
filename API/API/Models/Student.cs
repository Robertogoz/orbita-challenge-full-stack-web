using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    [Table("Student")]
    public class Student
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name = "RA")]
        [Column("RA")]
        public int RA { get; set; }

        [Display(Name = "Name")]
        [Column("Name")]
        public string Name { get; set; }

        [Display(Name = "Email")]
        [Column("Email")]
        public string Email { get; set; }

        [Display(Name = "CPF")]
        [Column("CPF")]
        public string CPF { get; set; }
    }
}
