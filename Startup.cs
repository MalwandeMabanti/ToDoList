using Microsoft.EntityFrameworkCore;
using ToDoList.Data;
using ToDoList.Interfaces;
using ToDoList.Services;

namespace ToDoList
{
    public class Startup
    {
        public IConfiguration Configuration { get; set; }
        public Startup(IConfiguration configuration) 
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options => 
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddControllers();

            services.AddScoped<ITodoService, TodoService>();

            services.AddEndpointsApiExplorer();

            services.AddSwaggerGen();

            services.AddCors(options => 
            {
                options.AddPolicy("AllowAll",
                    builder =>
                    {
                        builder.AllowAnyOrigin()
                                .AllowAnyHeader()
                                .AllowAnyMethod();
                    });
            });

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env) 
        {
            if(env.IsDevelopment()) 
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1"));
            }

            
            app.UseHttpsRedirection();

            // Add these before the app.Endpoints() line:
            app.UseCors("AllowAll");

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            


           

            
        }
    }
}
