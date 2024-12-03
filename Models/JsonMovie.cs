using System;
using System.Text.Json.Serialization;

namespace CS_FULLSTACK_03._12._2024.Models;

/// <summary>
/// Dette er en modell av inkommende JsonData
/// som representerer en ny film.
/// Brukes i AddMovie metoden i context
/// for å projektere over til et Movie objekt
/// med en unik Id.
/// </summary>
public class JsonMovieObject
{
    [JsonPropertyName("title")]
    public string? Title { get; set; }
    [JsonPropertyName("year")]
    public int Year { get; set; }
    [JsonPropertyName("extract")]
    public string? Extract { get; set; }
    [JsonPropertyName("cast")]
    public required List<string> Cast { get; set; }
    [JsonPropertyName("genres")]
    public required List<string> Genres { get; set; }
}
