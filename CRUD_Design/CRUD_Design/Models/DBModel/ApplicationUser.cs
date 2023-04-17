using CRUD_Design.Models.DBModel;
using Microsoft.AspNetCore.Identity;

namespace CRUD_Design
{
    public class ApplicationUser :IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public virtual ICollection<RunInfo> RunInfos { get; set; }
    }
}
