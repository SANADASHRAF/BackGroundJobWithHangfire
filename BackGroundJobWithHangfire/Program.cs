using Hangfire;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using BackGroundJobWithHangfire;
public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Services.AddHangfire(x => x.UseSqlServerStorage(builder.Configuration.GetConnectionString("DefaultConnection")));
        builder.Services.AddHangfireServer();
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.UseAuthorization();
        app.UseHangfireDashboard("/hangfire");
        app.UseHangfireServer();

        // Use a lambda expression to define the job
        RecurringJob.AddOrUpdate(() => BackGroundJobWithHangfire.Class.sentemail(), Cron.Minutely());

        app.MapControllers();

        app.Run();
    }
}
