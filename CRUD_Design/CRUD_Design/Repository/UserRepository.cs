using AutoMapper;
using Sportsmeter_frontend.Model.Services;

namespace CRUD_Design.Repository
{
    public class UserRepository: GenericRepository<ApplicationUser>
    {
        public UserRepository(DataContext context, IMapper mapper) : base(context, mapper)
        {
        }
    }
}
