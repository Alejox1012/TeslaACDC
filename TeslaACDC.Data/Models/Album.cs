using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace TeslaACDC.Data.Models;

public class Album : BaseEntity<int>
{
    public string Name{get; set;} = "";
    public int Year{get; set;}
    public Genre Genre{get; set;} = Genre.Unknown;

    [ForeignKey("Artist")] 
    public int ArtistId{get; set;}
}

public enum Genre{
    Rock,
    Pop,
    Metal,
    Jazz,
    Blues,
    Reggae,
    Ska,
    Punk,
    Funk,
    Soul,
    Country,
    Classical,
    HipHop,
    Electronic,
    Dance,
    Disco,
    Techno,
    House,
    Trance,
    Unknown,
    

}

