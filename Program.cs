using CS_FULLSTACK_03._12._2024.Context;
using CS_FULLSTACK_03._12._2024.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

//!!CONFIG
//Her henter vi inn alle konfigurasjonsobjekter, services,
// og annen data
//vår api kan ta i bruk.
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


//!!CORS
//CORS er et ekstra sikkerhetslayer på apien som skjekker om ORIGIN headeren i en respons er tillatt som ORIGIN i vår server. 
//Som standard tillater en server kun seg selv som ORIGIN. 
//Dette fører ofte til at hvis man prøver å gjøre requests via f.eks Javascript, vil man få en NO-CORS in header
//Nedenfor legger vi til en CORS policy, som for nå godtar alle ORIGIN headers, og tillater at de når alle endepunktene våre.
//!! DETTE ER IKKE DET SAMME SOM TILGANGSKONTROLL
//Selv om vi tillater alle ORIGINS er dette ikke det samme som å åpne serveren for internett.
//ORIGIN headeren er ikke det samme som å åpne tilgangsporter i maskinen vår, men refererer til en ORIGIN verdi i en http header 
//(se bilde i readme for eksempel hvor vi kjørte en fetch() fra consollen i Exalidraw.com)
builder.Services.AddCors(options =>
{
    options.AddPolicy(
        name: "AllowAll",
        configurePolicy: policy =>
        {
            policy.AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod();
        }
    );
});
var context = new DataContext();


//!!BUILD
//Her bruker vi konfigurasjonen, og andre objekter, for å 
//bygge en definisjon av vår app, som skal aktivt kjøre og lytte etter
//requests.
var app = builder.Build();

//!!CORS FORTS
//Siden vi kun har konfigurert en CORS policy, må vi og fortelle at vi skal bruke den.
//Det gjør vi via UseCors() methoden til app.
//Grunnen for at dette er separert, er at dette tillater oss lett å ha en APP for hvis vi er i development mode, og en hvis vi er publisert. 
app.UseCors("AllowAll");

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
app.MapPost("/movies", (JsonMovieObject newMov) =>
{
    context.AddMovie(newMov);
    context.SaveData();
});

//READ, her åpner vi for å kunne READ (lese) etter en film, basert på ID.
app.MapGet("/movies/{id}", Results<Ok<Movie>, NotFound> (int id) =>
{
    var movie = context.Movies.FirstOrDefault(movie => movie.Id == id);
    if (movie == null) return TypedResults.NotFound();
    else return TypedResults.Ok(movie);

});

//READ, Her åpner vi for å kunne READ (lese) filmer, basert på params
app.MapGet("/movies", ([AsParameters] MovieQueryParam queryParams) =>
{
    return context.Movies.QueryBuilder(queryParams).ToList();
});


//Her tar vi i mot et Helt movie object, inkludert en ID.
//Vi velger å stole på at PUT requesten har en gyldig film
//Og enten sletter en funnet film og putter inn erstatningen
//Eller putter den inn som en ny film i Movies.
app.MapPut("/movies", Results<NoContent, Created> (Movie mov) =>
{
    var existingMovie = context.Movies.Find(movie => movie.Id == mov.Id);
    if (existingMovie == null)
    {
        context.Movies.Add(mov);
        context.SaveData();
        return TypedResults.Created();
    }
    context.Movies.Remove(existingMovie);
    context.Movies.Add(mov);
    context.SaveData();
    return TypedResults.NoContent();
});

//Her tar vi inn en id fra URL
//Og prøver å slette en film med Id lik parameter id.
//Vi returnerer enten Utført, eller Ikke funnet.
app.MapDelete("/movies/{id}", Results<NoContent, NotFound> (int id) =>
{
    var existingMovie = context.Movies.Find(movie => movie.Id == id);
    if (existingMovie == null)
    {
        return TypedResults.NotFound();
    }
    context.Movies.Remove(existingMovie);
    context.SaveData();
    return TypedResults.NoContent();
});

app.UseHttpsRedirection();


//!!RUN
//Her starter vi applikasjonen vår, basert på konfigurasjon
//og build.
app.Run();

