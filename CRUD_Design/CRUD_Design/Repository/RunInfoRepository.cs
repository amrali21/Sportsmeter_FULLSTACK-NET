using AutoMapper;
using CRUD_Design.Contracts;
using CRUD_Design.Models.DBModel;
using Sportsmeter_frontend.Model.Services;

namespace CRUD_Design.Repository
{

    public class RunInfoRepository: GenericRepository<RunInfo>, IRunInfoRepository  {

        public readonly DataContext _context;

        public RunInfoRepository(DataContext context, IMapper mapper) : base(context, mapper)
        {
            _context = context;
        }
    }
}
