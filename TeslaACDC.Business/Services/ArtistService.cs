using System.Net;
using Microsoft.Extensions.Options;
using Microsoft.VisualBasic;
using TeslaACDC.Business.Interfaces;
using TeslaACDC.Data;
using TeslaACDC.Data.IRepository;
using TeslaACDC.Data.Models;
using TeslaACDC.Data.Repository;

namespace TeslaACDC.Business.Services;

public class ArtistService : IArtistService
{
    //private IArtistRepository<int, Artist> _artistRepository;
    private readonly IUnitOfWork _unitOfWork;
    private List<Artist> _listaArtista = new();

    public ArtistService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
        //_artistRepository = artistRepository;
        //_artistRepository = new ArtistRepository<int, Artist>(_context);
    }


    public async Task<BaseMessage<Artist>> FindArtistById(int id)
    {
        Artist? artist = new();
        artist = await _unitOfWork.ArtistRepository.FindAsync(id);
        //_listaArtista = await _artistRepository.GetAllAsync();

        return artist != null ? 
            BuildResponse(new List<Artist> { artist }, "Artist found", HttpStatusCode.OK, 1) : 
            BuildResponse(null, "Artist not found", HttpStatusCode.NotFound, 0);
    }

    public async Task<BaseMessage<Artist>> FindArtistByName(string name)
    {
        var lista = await _unitOfWork.ArtistRepository.GetAllAsync( x => x.Name.ToLower().Contains(name.ToLower())); //_listaArtista.FindAll( x => x.Name.Tolower().Contains(name.ToLower())); 
        //var _listaArtista = _unitOfWork.ArtistRepository.GetAllAsync(x => x.Name.Contains(name));
            //x.Name.Include(name.ToLower());

        return lista.Any() ? 
            BuildResponse(lista.ToList(), "Artist found", HttpStatusCode.OK, lista.Count()) : 
            BuildResponse(lista.ToList(), "Artist not found", HttpStatusCode.NotFound, 0);
    }
     
    
    


    public async Task<BaseMessage<Artist>> AddArtist(Artist artist)
    {
        var isValid = ValidateModel(artist);
        if (!string.IsNullOrEmpty(isValid))
        {
            return BuildResponse(null, isValid, HttpStatusCode.BadRequest, new());
        }

        try
        { 
            await _unitOfWork.ArtistRepository.AddAsync(artist);
            await _unitOfWork.SaveAsync(); // 🔥 Asegura que se guarde en la BD

            return BuildResponse(new List<Artist> { artist }, "Artist added successfully", HttpStatusCode.Created, 1);
        }
        catch (Exception ex)
        {
            return new BaseMessage<Artist>
            {
                Message = $"[Exception]: {ex.Message}",
                StatusCode = HttpStatusCode.InternalServerError,
                TotalElements = 0,
                ResponseElements = new()
            };
        }
    }
    
    public async Task<BaseMessage<Artist>> FindArtistByProperties(string name, string country)
    {
        var lista = await _unitOfWork.ArtistRepository.GetAllAsync(x => x.Name.ToLower().Contains(name.ToLower()) && x.Country.ToLower().Contains(country.ToLower()));
        return lista.Any() ? 
            BuildResponse(lista.ToList(), "Artist found", HttpStatusCode.OK, lista.Count()) : 
            BuildResponse(lista.ToList(), "Artist not found", HttpStatusCode.NotFound, 0);
    }

    public async Task<BaseMessage<Artist>>GetArtistList()
    {
        var lista = await _unitOfWork.ArtistRepository.GetAllAsync();
        return lista.Any() ? 
            BuildResponse(lista.ToList(), "Artist found", HttpStatusCode.OK, lista.Count()) : 
            BuildResponse(lista.ToList(), "Artist not found", HttpStatusCode.NotFound, 0);

    }





    //////////  validaciones de los modelos

     private string ValidateModel(Artist artist)
    {
        string Message = string.Empty;
        if (string.IsNullOrEmpty(artist.Name))
        {
            Message = "Name is required";
        }

        return Message;

    }



    //unificar los mensajes 
    private BaseMessage<Artist> BuildResponse(List<Artist> lista, string message = "", HttpStatusCode status = System.Net.HttpStatusCode.OK, int totalElements = 0)
    {
        return new BaseMessage<Artist>()
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


    public async Task<string> TestArtistCreation(Artist artist)
    {
        return ValidateModel(artist);
    }

    #endregion






}