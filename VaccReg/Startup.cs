using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using VaccRegDb;

namespace VaccReg
{
  public class Startup
  {
    private readonly string myAllowSpecificOrigins = "_myAllowSpecificOrigins";
    private const string swaggerVersion = "v1";
    private const string swaggerTitle = "VaccReg";

    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
      var builder = new SqliteConnectionStringBuilder(@"Data Source=c:\Users\eugen\Unterricht\Tools\createWepapiProject\vaccinations.db");
      var location = System.Reflection.Assembly.GetEntryAssembly().Location;
      string dataDirectory = System.IO.Path.GetDirectoryName(location);
      builder.DataSource = System.IO.Path.Combine(dataDirectory, builder.DataSource);
      var absoluteConnectionString = builder.ToString();
      services.AddDbContext<VaccRegContext>(options => options.UseSqlite(absoluteConnectionString));

      services.AddCors(options =>
      {
        options.AddPolicy(myAllowSpecificOrigins,
            x => x.AllowAnyOrigin()
                  .AllowAnyMethod()
                  .AllowAnyHeader()
          );
      });
      services.AddControllers();
      services.AddSwaggerGen(x =>
      {
        x.SwaggerDoc(swaggerVersion, new OpenApiInfo
        {
          Title = swaggerTitle,
          Version = swaggerVersion
        });
      });
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
     if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
	    Console.WriteLine("UseSwagger");
	    app.UseSwagger();
	    app.UseSwaggerUI(x =>
	    {
		  x.SwaggerEndpoint( $"/swagger/{swaggerVersion}/swagger.json", swaggerTitle);
	    });
      }

      app.UseCors(myAllowSpecificOrigins);

      app.UseHttpsRedirection();

      app.UseRouting();

      app.UseAuthorization();

      app.UseEndpoints(endpoints =>
      {
        endpoints.MapControllers();
      });
    }
  }
}
