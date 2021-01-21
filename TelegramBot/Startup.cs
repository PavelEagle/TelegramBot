using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TelegramBot.BotSettings;
using TelegramBot.Quartz;
using TelegramBot.Services;

namespace TelegramBot
{
  public class Startup
  {
    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;
    }

    public IConfiguration Configuration { get; }
    public void ConfigureServices(IServiceCollection services)
    {
      services.AddScoped<IUpdateService, UpdateService>();
      services.AddTransient<IBotService, BotService>();
      services.Configure<BotConfiguration>(Configuration.GetSection("BotConfiguration"));
      services.AddTransient<JobFactory>();

      services.AddControllers()
        .AddNewtonsoftJson(); 
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }

      app.UseRouting();
      app.UseCors();

      app.UseEndpoints(endpoints =>
      {
        endpoints.MapControllers();
      });
    }
  }
}
