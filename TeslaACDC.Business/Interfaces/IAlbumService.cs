using TeslaACDC.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TeslaACDC.Business.Interfaces;

public interface IAlbumService
{
   
   
    
   Task<BaseMessage<Album>> GetAlbumList();
   Task<BaseMessage<Album>> FindAlbumById(int id);
   Task<BaseMessage<Album>> FindById(int id);
   Task<BaseMessage<Album>> AddAlbum(Album album);



    /*
    Task<List<Album>> GetAlbumsList(); // Obtiene toda la lista de álbumes
    Task<Album> GetAlbum(); // Obtiene un álbum específico
    Task<BaseMessage<Album>> FindById(int id); // Obtiene un álbum por su id
    Task<BaseMessage<Album>> FindByName(string name); // Busca un álbum por su nombre
    Task<BaseMessage<Album>> FindByProperties(string name, int Year);
    Task<BaseMessage<Album>> Getlist(); 
    Task<BaseMessage<Album>> FindByArtist(int artistId);
    Task<BaseMessage<Album>> FindByYearRange(int startYear, int endYear);
    Task<BaseMessage<Album>> FindByGenre(Genre genre);
    Task<BaseMessage<Album>> AddAlbum(Album newAlbum);
    Task<BaseMessage<Album>> DeleteAlbum(int id);
    Task<BaseMessage<Album>> EditAlbum(Album updatedAlbum);

    */


    #region Learning to Test
    Task<string>HealthCheckTest();
    Task<string>TestAlbumCreation(Album album);

    #endregion

}
