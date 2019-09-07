using GraphiQl;
using GraphQlStudy.Domains;
using GraphQlStudy.Interfaces;
using GraphQlStudy.Models.Contexts;
using GraphQlStudy.Models.GraphQL.Queries;
using GraphQlStudy.Models.GraphQL.Types;
using GraphQlStudy.Queries;
using GraphQlStudy.Services;
using GraphQL;
using GraphQL.DataLoader;
using GraphQL.Http;
using GraphQL.Types;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GraphQlStudy
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
            var sqlConnectionString = Configuration.GetConnectionString("sqlServerConnectionString");
            services.AddDbContext<RelationalDbContext>(options =>
                options.UseSqlServer(sqlConnectionString, b => b.MigrationsAssembly(nameof(GraphQlStudy))));

            services.AddSingleton<IDocumentWriter, DocumentWriter>();
            services.AddSingleton<IDocumentExecuter, DocumentExecuter>();

            services.AddSingleton<IDataLoaderContextAccessor, DataLoaderContextAccessor>();
            services.AddSingleton<DataLoaderDocumentListener>();

            //services.AddScoped<StudentQuery>();
            //services.AddScoped<ClassQuery>();
            services.AddScoped<RootQuery>();
            services.AddScoped<ISchema, GraphQLGenericSchema<RootQuery>>();

            services.AddScoped<IClassDomain, ClassDomain>();
            services.AddScoped<DbContext, RelationalDbContext>();
            services.AddScoped<IDependencyResolver>(s => new FuncDependencyResolver(s.GetRequiredService));
            services.AddScoped(typeof(ICacheService<,>), typeof(JsonizeKeyValueCacheService<,>));

            services.AddScoped<StudentType>();
            services.AddScoped<ClassType>();
            services.AddScoped(typeof(RangeModelType<,>));
            services.AddScoped<PaginationModelType>();
            services.AddScoped<SearchClassModelType>();
            
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();

            app.UseGraphiQl();
            app.UseMvc();
        }
    }
}