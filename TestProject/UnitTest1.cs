
using CRUD_Design.Contracts;
using CRUD_Design.Models.DBModel;
using CRUD_Design.Repository;
using CRUD_Design_Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Sportsmeter_frontend.Model.Services;

namespace TestProject
{
    public class UnitTest1
    {

        public ServiceProvider serviceProvider { get; set; }
        public UnitTest1()
        {
            // inject services here.
            ServiceCollection services = new ServiceCollection();

            var connstring = "Data Source=LAPTOP-FPNQ81MI\\SQLEXPRESS;Database=CRUD_Sportsmeter;Integrated Security=True";
            services.AddDbContext<DataContext>(options => options.UseSqlServer(connstring));

            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IRunInfoRepository, RunInfoRepository>();
            services.AddScoped<UserRepository>();
            services.AddAutoMapper(typeof(Program).Assembly);

            serviceProvider = services.BuildServiceProvider();
        }

        [Fact]
        public async Task TestGet()
        {
            IRunInfoRepository runINfoRepo = serviceProvider.GetRequiredService<IRunInfoRepository>();
            var test = await runINfoRepo.GetAsync(u => u.Id == 5);

            Assert.True(test.Count > 0);
        }

        [Fact]
        public async Task TestPost()
        {
            IRunInfoRepository runINfoRepo = serviceProvider.GetRequiredService<IRunInfoRepository>();

            DateTime dateTime = DateTime.Now;
            RunInfo ri = new RunInfo
            {
                Distance = 7000,
                Date = dateTime,
                Time = 300,
                ApplicationUserId = "1028d5ad-bc11-46c3-b094-822033b011cf"
            };


             await runINfoRepo.AddAsync(ri);
             var addedItem = await runINfoRepo.GetAsync(u => u.Date == dateTime);
             
            Assert.NotNull(addedItem);
             
             //if(addedItem != null)
             //   await runINfoRepo.DeleteAsync(ri);
        }
    }
}