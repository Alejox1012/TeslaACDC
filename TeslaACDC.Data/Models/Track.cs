using TeslaACDC.Data.Models;

public class Track : BaseEntity<int>
{
    public string Name { get; set; } = ""; // Nombre de la canción
    public TimeSpan Duration { get; set; } // Duración en formato HH:mm:ss
    public bool IsExplicit { get; set; } // Si tiene contenido explícito
    public DateTime ReleaseDate { get; set; } // Fecha de lanzamiento

}