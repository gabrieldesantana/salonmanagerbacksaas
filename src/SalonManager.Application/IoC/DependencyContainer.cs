using Microsoft.EntityFrameworkCore;
using SalonManager.Domain.Interfaces.Repository;
using SalonManager.Domain.Interfaces.Services;
using SalonManager.Infra.Data.Context;
using SalonManager.Infra.Data.Repository;
using SalonManager.Infra.Data.Repository.UnitOfWork;
using SalonManager.Service.Services;

namespace SalonManager.Application.IoC
{
    public static class DependecyContainer
    {

        public static void AddRegisterServices(this IServiceCollection services)
        {
            #region dbContext and ILogger
            services.AddDbContext<SalonManagerDbContext>(opt => opt.UseInMemoryDatabase("database"));
            #endregion

            #region Service
            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<ISalonServiceService, SalonServiceService>();
            services.AddScoped<IFinanceService, FinanceService>();
            services.AddScoped<IAppointmentService, AppointmentService>();
            services.AddScoped<IGoalService, GoalService>();
            services.AddScoped<IUserService, UserService>();
            #endregion

            #region Repository
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<ISalonServiceRepository, SalonServiceRepository>();
            services.AddScoped<IFinanceRepository, FinanceRepository>();
            services.AddScoped<IAppointmentRepository, AppointmentRepository>();
            services.AddScoped<IGoalRepository, GoalRepository>();
            services.AddScoped<IUserRepository, UserRepository>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            #endregion
            //services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        }

    }
}
