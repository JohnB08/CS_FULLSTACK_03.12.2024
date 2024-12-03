using System;
using System.Text.Json;
using CS_FULLSTACK_03._12._2024.Models;

namespace CS_FULLSTACK_03._12._2024.Context;

public class DataContext
{
    private int IdIncrementor { get; set; } = 0;
    public MovieList Movies = [];
    public class MovieList : List<Movie>
    {
    }
    /// <summary>
    /// En metode som tar inn et jsonObject
    /// og legger til en ny Movie med en incremented 
    /// IdIncrementor value som Id.
    /// </summary>
    /// <param name="jsonObj">et element fra movies.json</param>
    public void AddMovie(JsonMovieObject jsonObj)
    {
        IdIncrementor++;
        var movie = new Movie()
        {
            Id = IdIncrementor,
            Title = jsonObj.Title,
            Year = jsonObj.Year,
            Cast = jsonObj.Cast,
            Genres = jsonObj.Genres,
            Extract = jsonObj.Extract
        };
        Movies.Add(movie);
    }
    /// <summary>
    /// En metode som lagrer endringene
    /// fra Movies til movies.json
    /// slik at disse endringene eksisterer neste gang
    /// appen kjører.
    /// </summary>
    public void SaveData()
    {
        //Her lager vi noen serializer options, som forteller JsonSerializeren vår
        //at vi vil ha alle nøklene i Jsonstringen som camelCase. 
        var serializerOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        };
        var jsonData = JsonSerializer.Serialize(Movies, serializerOptions);
        File.WriteAllText("movies.json", jsonData);
    }
    public DataContext()
    {
        //Her prepopulerer vi Movies listen vår, med eksisterende data
        //fra movies.json ved boot.
        var jsonString = File.ReadAllText("movies.json");
        var jsonData = JsonSerializer.Deserialize<List<JsonMovieObject>>(jsonString);
        foreach (var element in jsonData)
        {
            AddMovie(element);
        }
    }


}
