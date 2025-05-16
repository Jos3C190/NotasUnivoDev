namespace NotasUnivoDev.Models.ViewModels
{
    public class CareersViewModel 
    {
        public int CareerId { get; set; }
        public string CareerName { get; set; }
        public int FacultyId { get; set; }
        public bool IsActive { get; set; } 
        public List<FacultiesModel> FacultiesList { get; set; } 

    }
}
