using System.Net;
using Microsoft.VisualBasic;
using TeslaACDC.Business.Interfaces;
using TeslaACDC.Data.IRepository;
using TeslaACDC.Data.Models;
using TeslaACDC.Data.Repository;

namespace TeslaACDC.Business.Services;



public class AlbumService : IAlbumService
{
    private readonly IAlbumRepository<int, Album> _albumRepository;

    public AlbumService(IAlbumRepository<int, Album> albumRepository)
    {
        _albumRepository = albumRepository;
    }



    public async Task<BaseMessage<Album>> AddAlbum(Album album)
    {
        var isValid = ValidateModel(album);
        if (!string.IsNullOrEmpty(isValid))
        {
            return new BaseMessage<Album>()
            {

                Message = isValid,
                StatusCode = System.Net.HttpStatusCode.BadRequest,
                TotalElements = 0,
                ResponseElements = new List<Album>()
            };
        }
        try
        {
            // Guardar en la base de datos
            await _albumRepository.addAsync(album);
        }
        catch (Exception ex)
        {
            // Construir la respuesta con BaseMessage
            return new BaseMessage<Album>()
            {

                Message = $"[Exception] {ex.Message}",
                StatusCode = System.Net.HttpStatusCode.InternalServerError,
                TotalElements = 0,
                ResponseElements = new List<Album>()
            };
        }

        return new BaseMessage<Album>()
        {

            Message = "Error al agregar álbum",
            StatusCode = System.Net.HttpStatusCode.OK,
            TotalElements = 1,
            ResponseElements = new List<Album> { album }
        };


    }

    public async Task<BaseMessage<Album>> FindAlbumById(int id)
    {
        // Buscar el álbum en la base de datos
        var album = await _albumRepository.FindAsync(id);

        // Si no se encuentra, devolver un mensaje de error
        if (album == null)
        {
            return BuildResponse(
                new List<Album>(),  // Lista vacía en lugar de null
                "Álbum no encontrado",
                HttpStatusCode.NotFound,
                0
            );
        }

        // Si se encuentra, devolver el álbum con un mensaje de éxito
        return BuildResponse(
            new List<Album> { album },
            "Álbum encontrado con éxito",
            HttpStatusCode.OK,
            1
        );
    }


    public async Task<BaseMessage<Album>> FindById(int id)
    {
        Album? album = new();
        album = await _albumRepository.FindAsync(id);
        return album != null ? BuildResponse(new List<Album>(){ album }, "Álbum encontrado", HttpStatusCode.OK, 1) :
                               BuildResponse(new List<Album>(), "Álbum no encontrado", HttpStatusCode.NotFound, 0);
    }



    private string ValidateModel(Album album)
    {
        string Message = string.Empty;
        if (string.IsNullOrEmpty(album.Name))
        {
            Message = "Name is required";
        }
        if (album.Year < 1901 || album.Year > DateAndTime.Now.Year) 
        {
            Message += "The year must be between 1901 and 2025";
        }

        return Message;

    }



    //unificar los mensajes 
    private BaseMessage<Album> BuildResponse(List<Album> lista, string message = "", HttpStatusCode status = System.Net.HttpStatusCode.OK, int totalElements = 0)
    {
        return new BaseMessage<Album>()
        {
            Message = message,
            StatusCode = status,
            TotalElements = totalElements,
            ResponseElements = lista
        };
    }


    #region Learning to Test

    public async Task<string> HealthCheckTest()
    {
        return "OK";
    }

    public async Task<string> HealthCheckTest(bool IsOK)
    {
        return IsOK ? "OK!" : "Not OK!";
    }


    public async Task<string> TestAlbumCreation(Album album)
    {
        return ValidateModel(album);
    }



    #endregion



    /*
    private List<Album> _listaAlbum = new();
    
    public AlbumService()
    {
        _listaAlbum.Add(new Album { Id = 1, Name = "Mañana será bonito", Genre = Genre.Pop, Year = 2022 });
        _listaAlbum.Add(new Album { Id = 2, Name = "El mal querer", Genre = Genre.Disco, Year = 2018});
        _listaAlbum.Add(new Album { Id = 3, Name = "Random Access Memories", Genre = Genre.House, Year = 2});
        _listaAlbum.Add(new Album { Id = 4, Name = "Back in Black", Genre = Genre.Rock, Year = 1980 });
        _listaAlbum.Add(new Album { Id = 5, Name = "Thriller", Genre = Genre.Pop, Year = 1982 });
        _listaAlbum.Add(new Album { Id = 6, Name = "The Wall", Genre = Genre.Rock, Year = 1979 });
        _listaAlbum.Add(new Album { Id = 7, Name = "The Dark Side of the Moon", Genre = Genre.Rock, Year = 1973 });
        _listaAlbum.Add(new Album { Id = 8, Name = "levitating", Genre = Genre.Rock, Year = 1973 });
        _listaAlbum.Add(new Album { Id = 9, Name = "Debi Tirar Mas Fotos", Genre = Genre.Rock, Year = 1973 });
        _listaAlbum.Add(new Album { Id = 10, Name = "My Dark Twisted Fantasy", Genre = Genre.Rock, Year = 1973 });
        _listaAlbum.Add(new Album { Id = 11, Name = "The College Dropout", Genre = Genre.Electronic, Year = 1973 });
       
        Album album = new Album();
    }



       public async Task<BaseMessage<Album>> FindByProperties(string name, int Year, Genre genre)
    {
        if (!string.IsNullOrEmpty(name) && Year == 0 && genre == Genre.Electronic)
        {
        

        }
        if (Year >= 0 || !String.IsNullOrEmpty(name) || genre != Genre.Pop)
        {
    
        
        }
           
        
        var albums = _listaAlbum.Where(x => x.Name.ToLower().Contains(name.ToLower()) ||  x.Year == Year).ToList();
        return albums.Any() ? BuildResponse(albums, "", HttpStatusCode.OK, albums.Count) :
               BuildResponse(new List<Album>());
    }

    // Lista de álbumes en memoria
    private readonly List<Album> _albums = new()
    {
        new Album { Name = "Mañana será bonito", Genre = Genre.Pop, Year = 2022 },
        new Album { Name = "El mal querer", Genre = Genre.Disco, Year = 2018 },
        new Album { Name = "Random Access Memories", Genre = Genre.Disco, Year = 2013 },
        new Album { Name = "Back in Black", Genre = Genre.Metal, Year = 1980 },
        new Album { Name = "Thriller", Genre = Genre.Pop, Year = 1982 }

    };

    // Obtiene todos los álbumes
    public async Task<List<Album>> GetAlbumsList()
    {
        return await Task.FromResult(_albums);
    }

    // Obtiene solo un álbum de ejemplo
    public async Task<Album> GetAlbum()
    {
        return await Task.FromResult(_albums.FirstOrDefault() ?? new Album());
    }
    
    // Obtiene un álbum por su id
    public async Task<BaseMessage<Album>> FindById(int id)
    {
        Album album;
        


     //  foreach(var item in _listaAlbum)
    //   {
     //   if(item.Id == id)
     //   {
    //      album = item;
    //        break;
//}
       

       // LINQ -> ORM Entity Framework
       // Dapper -> ORM (Framework Distinto)
       //IEnumerable
       var lista = _listaAlbum.Where(x => x.Id == id).ToList();

       // Select * from Album where Id = 1 AND Name = 'Mañana será bonito' 
       
        return lista.Any()? BuildResponse(lista, "Álbum encontrado", HttpStatusCode.OK, lista.Count):
        BuildResponse(lista, "Álbum no encontrado", HttpStatusCode.NotFound, 0);
    }


    public async Task<BaseMessage<Album>> AddAlbum()
    {
        try{
        _listaAlbum.Add(new Album { Id = 5, Name = "Thriller", Genre = Genre.Pop, Year = 1982 });
        }catch{
            return new BaseMessage<Album>()
            {
                Message = "Error al agregar álbum",
                StatusCode = System.Net.HttpStatusCode.InternalServerError,
                TotalElements = 0,
                ResponseElements = new ()
            };

        }
        return new BaseMessage<Album>()
        {
            Message = "Álbum agregado correctamente",
            StatusCode = System.Net.HttpStatusCode.OK,
            TotalElements = _listaAlbum.Count,
            ResponseElements = _listaAlbum
        };
    }

    public async Task<BaseMessage<Album>> Getlist()
    {
        return new BaseMessage<Album>(){
            Message = "",
            StatusCode = System.Net.HttpStatusCode.OK,
            TotalElements = _listaAlbum.Count,
            ResponseElements = _listaAlbum
        };
    }

    //unificar los mensajes 
    private BaseMessage<Album> BuildResponse(List<Album> lista, string message = "", HttpStatusCode status= System.Net.HttpStatusCode.OK,int totalElements = 0)
    {
        return new BaseMessage<Album>()
        {
            Message = message,
            StatusCode = status,
            TotalElements = totalElements,
            ResponseElements = lista
        };
    }

    public async Task<BaseMessage<Album>> FindByName(string name)
    {
         var lista = _listaAlbum.Where(x => x.Name.ToLower().Contains(name.ToLower())).ToList();

       // Select * from Album where Id = 1 AND Name = 'Mañana será bonito' 
       
        return lista.Any()? BuildResponse(lista, "Álbum encontrado", HttpStatusCode.OK, lista.Count):
        BuildResponse(lista, "Álbum no encontrado", HttpStatusCode.NotFound, 0);
    }

    public Task<Album> GetAlbum(int id)
    {
        throw new NotImplementedException();
    }


   //public async Task<BaseMessage<Album>> FindByArtist(int artistId)
   //{
   //  var lista = _listaAlbum.Where(x => x.ArtistId == artistId).ToList();

   // // Select * from Album where Id = 1 AND Name = 'Mañana será bonito' 
   // 
   //  return lista.Any()? BuildResponse(lista, "Álbum encontrado", HttpStatusCode.OK, lista.Count):
   //  BuildResponse(lista, "Álbum no encontrado", HttpStatusCode.NotFound, 0);
   //  
   //

    public Task<BaseMessage<Album>> FindByArtist(int artistId)
    {
        throw new NotImplementedException();
    }
    public Task<BaseMessage<Album>> FindByYearRange(int startYear, int endYear)
    {
        var lista = _listaAlbum.Where(x => x.Year >= startYear && x.Year <= endYear).ToList();
        return Task.FromResult(BuildResponse(lista, lista.Any() ? "Álbumes encontrados en el rango de años" : "No se encontraron álbumes en este rango", 
                                             lista.Any() ? HttpStatusCode.OK : HttpStatusCode.NotFound, 
                                             lista.Count));
    }


    public Task<BaseMessage<Album>> FindByGenre(Genre genre)
    {
        var lista = _listaAlbum.Where(x => x.Genre == genre).ToList();
        return Task.FromResult(BuildResponse(lista, lista.Any() ? "Álbumes encontrados en este género" : "No se encontraron álbumes de este género", 
                                             lista.Any() ? HttpStatusCode.OK : HttpStatusCode.NotFound, 
                                             lista.Count));
    }

   public Task<BaseMessage<Album>> AddAlbum(Album newAlbum)
    {
        newAlbum.Id = _listaAlbum.Count + 1;
        _listaAlbum.Add(newAlbum);
        return Task.FromResult(BuildResponse(new List<Album> { newAlbum }, "Álbum agregado con éxito", HttpStatusCode.Created, 1));
    }
    

    public Task<BaseMessage<Album>> DeleteAlbum(int id)
    {
        var album = _listaAlbum.FirstOrDefault(x => x.Id == id);
        if (album == null)
            return Task.FromResult(BuildResponse(new List<Album>(), "Álbum no encontrado", HttpStatusCode.NotFound, 0));

        _listaAlbum.Remove(album);
        return Task.FromResult(BuildResponse(new List<Album>(), "Álbum eliminado con éxito", HttpStatusCode.OK, 1));
    }




    public Task<BaseMessage<Album>> EditAlbum(Album updatedAlbum)
    {
        var index = _listaAlbum.FindIndex(x => x.Id == updatedAlbum.Id);
        if (index == -1)
            return Task.FromResult(BuildResponse(new List<Album>(), "Álbum no encontrado", HttpStatusCode.NotFound, 0));

        _listaAlbum[index] = updatedAlbum;
        return Task.FromResult(BuildResponse(new List<Album> { updatedAlbum }, "Álbum actualizado con éxito", HttpStatusCode.OK, 1));
    }

    public Task<BaseMessage<Album>> FindByProperties(string name, int Year)
    {
        throw new NotImplementedException();
    }



*/
}

