using System;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;

namespace CS_FULLSTACK_03._12._2024.Models;

/// <summary>
/// En modell av Movie objekter i 
/// Movies i context, med en unik Id.
/// </summary>
public class Movie
{
    [JsonPropertyName("id")]
    public int Id { get; set; }
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
