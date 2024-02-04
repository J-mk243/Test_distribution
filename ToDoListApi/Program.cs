using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using ToDoListApi.Data;
using AutoMapper;
using ToDoListApi.Services.Interfaces;
using ToDoListApi.Services;
using ToDoListApi.Models;

namespace ToDoListApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("OpenCorsPolicy", builder =>
                {
                    builder
                        .AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
            });
            builder.Services.AddDbContext<ToDoListDbContext>(options => options.UseInMemoryDatabase("ToDoListDb"));
            builder.Services.AddAutoMapper(typeof(Program));
            builder.Services.AddScoped<ITaskItemService, TaskItemService>();
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();


            var app = builder.Build();

            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var dbContext = services.GetRequiredService<ToDoListDbContext>();

                dbContext.Database.EnsureCreated();
                if (!dbContext.TaskItems.Any())
                {
                    dbContext.TaskItems.AddRange(
                    new TaskItem { Id = 1, Title = "Postuler pour mon nouveau poste", IsCompleted = true },
                    new TaskItem { Id = 2, Title = "Faire le test techniique", IsCompleted = true },
                    new TaskItem { Id = 3, Title = "Attendre le feedback", IsCompleted = false }
                    );
                    dbContext.SaveChanges();
                }
            }

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseCors("OpenCorsPolicy");
            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();


            app.Run();
        }
    }
}
