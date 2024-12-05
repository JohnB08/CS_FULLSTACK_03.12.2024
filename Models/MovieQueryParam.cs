using System;
using Microsoft.AspNetCore.Mvc;

namespace CS_FULLSTACK_03._12._2024.Models;

public class MovieQueryParam
{
    [FromQuery(Name = "title")]
    public string? Title { get; set; }
    [FromQuery(Name = "year")]
    public int? Year { get; set; }
    [FromQuery(Name = "to")]
    public int? To { get; set; }
    [FromQuery(Name = "from")]
    public int? From { get; set; }
    [FromQuery(Name = "extract")]
    public string? Extract { get; set; }
    [FromQuery(Name = "castmember")]
    public string? CastMember { get; set; }
    [FromQuery(Name = "genre")]
    public string? Genre { get; set; }
}
