using LibraryManagementSystem.Common;
using LibraryManagementSystem.Data;
using LibraryManagementSystem.Repository.IRepository;
using LibraryManagementSystem.Repository.RepositoryClass;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
#region ConFig Cros
builder.Services.AddCors(Option =>
{
    Option.AddPolicy("CustomPolicy", x => x.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());
});

#endregion
#region Database Connection

var connectionstring = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<LibraryDbContext>(Options => Options.UseSqlServer(connectionstring));

#endregion
#region Mapper Repository
builder.Services.AddAutoMapper(typeof(MappingProfile));

builder.Services.AddTransient<IBookRepository, BookRepository>();
builder.Services.AddTransient<IMemberRepository, MemberRepository>();
builder.Services.AddTransient<ILibraryRepository, LibraryRepository>();
builder.Services.AddTransient<IIssueRepository, IssueRepository>();
builder.Services.AddTransient<IReturnRepository, ReturnRepository>();

#endregion


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("CustomPolicy");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
