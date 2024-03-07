

namespace VacationManagement.Models
{
    public class Department: EntityBase
    {

       
        [Display(Name = "Department Name")]
        public string Name { get; set; }=string.Empty;

        public string? Description { get; set; } 


    }
}
