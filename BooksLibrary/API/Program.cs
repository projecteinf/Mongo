using mba.BooksLibrary;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<mba.BooksLibrary.Model.BooksLibraryDatabaseSettings>(
    builder.Configuration.GetSection("BooksLibraryDatabase"));


builder.Services.AddSingleton<mba.BooksLibrary.Services.BooksService>();
builder.Services.AddSingleton<mba.BooksLibrary.Services.MaterialService>();
builder.Services.AddSingleton<mba.BooksLibrary.Services.LibraryService>();

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}



app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
