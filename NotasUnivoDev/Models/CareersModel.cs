using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NotasUnivoDev.Models
{
    public class CareersModel : BaseModel
    {
        [Key]
        public int CareerId { get; set; }
        public string CareerName { get; set; }
        [ForeignKey(nameof(FacultyId))]
        public int FacultyId { get; set; }
        public virtual FacultiesModel Faculty { get; set; }

        public CareersModel()
        {
            IsActive = true;
            Created = DateTime.Now;
            CreatedBy = "ADMIN";
        }
    }
}
