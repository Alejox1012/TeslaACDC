using TeslaACDC.Data.Models;

namespace TeslaACDC.Business.Interfaces;
public interface IArtistService
{
   Task<BaseMessage<Artist>> FindArtistById(int id);
   Task<BaseMessage<Artist>> FindArtistByName(string name);
   Task<BaseMessage<Artist>> AddArtist(Artist artist);
   Task<BaseMessage<Artist>> GetArtistList();
   Task<BaseMessage<Artist>> FindArtistByProperties(string name, string country);



   
    #region Learning to Test
    Task<string>HealthCheckTest();
    Task<string>TestArtistCreation(Artist artist);

    #endregion

}