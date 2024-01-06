var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
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

app.UseHttpsRedirection();

app.MapGet("/bank/tva/{valueToConvert}/{country}", (int price, string country) =>
{
    double vatCalculation = 0;
    switch (country)
    {
        case "BE":
            vatCalculation = price * 1.21;
            return Results.Ok("Prix calculé : " + vatCalculation);

        case "FR":
            vatCalculation = price * 1.20;
            return Results.Ok("Prix calculé : " + vatCalculation);
        default:
            return Results.BadRequest("Code de pays non valide. Utilisez 'BE' ou 'FR'.");
    }

});

app.Run();
