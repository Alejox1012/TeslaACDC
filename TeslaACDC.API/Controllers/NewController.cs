using Microsoft.AspNetCore.Mvc;
using TeslaACDC.Business.Interfaces;
using TeslaACDC.Data.Models;

namespace TeslaACDC.API.Controllers;

[Controller]
[Route("api/[controller]")]
public class NewController : ControllerBase
{
    private readonly IArtistService _artistService;
    private readonly IAlbumService _albumService;
    private readonly ITrackService _trackService;


    public NewController(IArtistService artistService, IAlbumService albumService, ITrackService trackService)
    {
    _artistService = artistService;
    _albumService = albumService;
    _trackService = trackService;
    }   

    [HttpGet("GetArtistbyId/{id}")]
    public async Task<IActionResult> GetArtistById(int id)
    {
        var artist = await _artistService.FindArtistById(id);
        if (artist == null)
            return NotFound($"No se encontró el artista con ID {id}");
        
        return Ok(artist);
    }

    
    [HttpPost]
    [Route("AddArtist")]
    public async Task<IActionResult> AddArtist([FromBody] Artist artist)
    {
        var newArtist = await _artistService.AddArtist(artist);
        return Ok(newArtist);
    }


    [HttpGet("GetAlbumbyId/{id}")]
    public async Task<IActionResult> GetAlbumById(int id)
    {
        var album = await _albumService.FindAlbumById(id);
        if (album == null)
            return NotFound($"No se encontró el artista con ID {id}");
        
        return Ok(album);
    }

    [HttpPost]
    [Route("AddAlbum")]
    public async Task<IActionResult> AddAlbum([FromBody] Album album)
    {
        var newAlbum = await _albumService.AddAlbum(album);
        return Ok(newAlbum);
    }

    [HttpGet("GetTrackbyId/{id}")]
    public async Task<IActionResult> GetTrackById(int id)
    {
        var track = await _trackService.FindTrackById(id);
        if (track == null)
            return NotFound($"No se encontró el artista con ID {id}");
        
        return Ok(track);
    }

    [HttpPost]
    [Route("AddTrack")]
    public async Task<IActionResult> AddTrack([FromBody] Track track)
    {
        var newTrack = await _trackService.AddTrack(track);
        return Ok(newTrack);
    }
    

}
