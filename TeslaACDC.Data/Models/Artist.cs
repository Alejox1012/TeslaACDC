using TeslaACDC.Data.Models;

public class Artist : BaseEntity<int>
{
    public string Name{get; set;} = string.Empty;
    public string Country{get; set;} = string.Empty;
    //boolean write always verb + question example: isOnTour yes/no
    public bool isOnTour{get; set;} = false;
}