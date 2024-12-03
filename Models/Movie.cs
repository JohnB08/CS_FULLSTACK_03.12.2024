using System;

namespace CS_FULLSTACK_03._12._2024.Models;

/// <summary>
/// En modell av Movie objekter i 
/// Movies i context, med en unik Id.
/// </summary>
public class Movie
{
    public int Id { get; set; }
    public string? Title { get; set; }
    public int Year { get; set; }
    public string? Extract { get; set; }
    public required List<string> Cast { get; set; }
    public required List<string> Genres { get; set; }

}
