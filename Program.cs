using CS_FULLSTACK_03._12._2024.Context;
using CS_FULLSTACK_03._12._2024.Models;
using Microsoft.AspNetCore.Http.HttpResults;

//!!CONFIG
//Her henter vi inn alle konfigurasjonsobjekter, og annen data
//vår api kan ta i bruk.
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var context = new DataContext();


//!!BUILD
//Her bruker vi konfigurasjonen, og andre objekter, for å 
//bygge en definisjon av vår app, som skal aktivt kjøre og lytte etter
//requests.
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//!!Endepunkter
//Her definerer vi hvilke endepunkter vår app skal lytte på, 
//og hvilke request hvert endepunkt kan motta. 

//CREATE, her tillater vi apiet å CREATE (lage) en film
//Via en POST request med jsondata som samsvarer med 
//vår JsonMovieObject modell.
app.MapPost("/movies", (JsonMovieObject newMov)=>
{
    context.AddMovie(newMov);
    context.SaveData();
});

//READ, her åpner vi for å kunne READ (lese) etter en film, basert på ID.
app.MapGet("/movies/{id}", Results<Ok<Movie>, NotFound>(int id)=>
{
    var movie = context.Movies.FirstOrDefault(movie => movie.Id == id);
    if (movie == null) return TypedResults.NotFound();
    else return TypedResults.Ok(movie);

});

//READ, Her åpner vi for å kunne READ (lese) alle filmer.
//!!TODO Implementer å hente inn søkeparametere!
app.MapGet("/movies", ()=> context.Movies);


//!!TODO Implementer en UPDATE (oppdater) hvor vi skal kunne oppdatere et datapunkt i Movies;

//!!TODO Implementer en DELETE (slett) hvor vi skal kunne slette et datapunkt i Movies;
app.UseHttpsRedirection();


//!!RUN
//Her starter vi applikasjonen vår, basert på konfigurasjon
//og build.
app.Run();

