namespace TeslaACDC.Business.Interfaces;
public interface IArtistService
{
   public Task<Artist> FindArtistById(int id);
   public Task<Artist> AddArtist(Artist artist);
}