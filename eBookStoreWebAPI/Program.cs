using BusinessObject.DTO;
using BusinessObject.Mapping;
using BusinessObject.Models;
using Microsoft.AspNetCore.OData;
using Microsoft.AspNetCore.OData.Batch;
using Microsoft.EntityFrameworkCore;
using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;
using Repository.IRepository;
using Repository.Repository;
using System.Diagnostics;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers().AddOData(option => option.Select()
               .Filter().OrderBy().Expand().SetMaxTop(100)
               );
               //.AddRouteComponents("odata", GetEdmModel()));

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddAutoMapper(typeof(MappingProfile));
        builder.Services.AddDbContext<EbookStoreDbContext>(opt => builder.Configuration.GetConnectionString("ass2"));
        builder.Services.AddScoped<IBookRepository, BookRepository>();
        builder.Services.AddScoped<IBookAuthorRepository, BookAuthorRepository>();
        builder.Services.AddScoped<IPublisherRepository, PublisherRepository>();
        builder.Services.AddScoped<IAuthorRepository, AuthorRepository>();
        builder.Services.AddScoped<IUserRepository, UserRepository>();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        app.UseODataBatching();

        app.UseRouting();
        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
            //endpoints.MapODataRoute("")
        });

        app.MapControllers();

        app.Run();
    }

    private static IEdmModel GetEdmModel()
    {
        ODataConventionModelBuilder builder1 = new ODataConventionModelBuilder();
        builder1.EntitySet<Book>("Books");
        builder1.EntitySet<Author>("Authors");
        //builder1.EntitySet<Publisher>("Publishers");
        var publisherResponseDto = builder1.EntityType<PublisherCreateDTO>();
        publisherResponseDto.HasKey(dto => dto.PublisherName);
        builder1.EntitySet<PublisherCreateDTO>("PublisherResponseDTOs");
        builder1.EntityType<PublisherDTO>().HasKey(dto => dto.PubId);
        builder1.EntitySet<PublisherDTO>("PublisherRequestDTOs");
        return builder1.GetEdmModel();
    }
}