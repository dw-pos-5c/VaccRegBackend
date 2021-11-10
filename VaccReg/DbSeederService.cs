using System;
using System.Collections.Generic;
using System.Data.Common;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Text.Json.Serialization;
using Microsoft.Extensions.Options;
using VaccRegDb;


namespace VaccReg
{
    public class DbSeederService: IHostedService
    {
        private readonly IServiceScopeFactory scopeFactory;
        private readonly IConfiguration configuration;

        public DbSeederService(IServiceScopeFactory scopeFactory, IConfiguration configuration, IOptions<DbConfiguration> dbOptions)
        {
            this.scopeFactory = scopeFactory;
            this.configuration = configuration;
            var a = configuration.GetSection("import");
            var b = configuration.GetSection("ConnectionStrings");
            Console.WriteLine(dbOptions.Value.import);
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            return Task.Run(ParseJson, cancellationToken);
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        private void ParseJson()
        {
            using IServiceScope scope = scopeFactory.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<VaccRegContext>();

            var registrations = JsonSerializer.Deserialize<List<Registration>>(File.ReadAllText(configuration["import"]));

            if (registrations == null) return;

            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();

            Console.Write(registrations);

            db.Registrations.AddRange(registrations);
            db.SaveChanges();
        }
    }
}
