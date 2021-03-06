using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Catalog.Repositories;
using Catalog.Settings;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;

namespace Catalog
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Serializers for MongoDb id and createdDate
            BsonSerializer.RegisterSerializer(new GuidSerializer(BsonType.String));
            BsonSerializer.RegisterSerializer(new DateTimeOffsetSerializer(BsonType.String));

            // MongoDb Dockerized Service
            // services.AddSingleton<IMongoClient>(ServiceProvider=>{
            //     var settings = Configuration.GetSection(nameof(MongoDbSettings)).Get<MongoDbSettings>();
            //     return new MongoClient(settings.ConnectionString);
            // });


            // MongoDb Atlas Cloud Service
            services.AddSingleton<IMongoClient>(ServiceProvider=>{
                var settings = MongoClientSettings.FromConnectionString(Configuration.GetConnectionString("Atlas"));
                return new MongoClient(settings);
            });
            // This is for Dependecy injecting the Interface we give to the controller
            services.AddSingleton<IItemsRespository,MongoDbItemsRepository>();

            // In Mem servicee and interface for dependency injection
            //services.AddSingleton<IItemsRespository,InMemItemsRespository>();



            //options =>{ options.SuppressAsyncSuffixInActionNames = false} needed to stop .net3 breaking change
            services.AddControllers(options =>{
                options.SuppressAsyncSuffixInActionNames= false;
            } );
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Catalog", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Catalog v1"));
            }

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
