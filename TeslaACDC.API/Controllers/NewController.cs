using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TeslaACDC.Business.Interfaces;
using TeslaACDC.Data.Models;

namespace TeslaACDC.API.Controllers;

[ApiController]
[Route("api/[controller]")]
//[Authorize]
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

    [HttpGet("GetAlbumbyId/{id}")]
    public async Task<IActionResult> GetAlbumById(int id)
    {
        var album = await _albumService.FindAlbumById(id);
        if (album == null)
            return NotFound($"No se encontró el artista con ID {id}");
        
        return Ok(album);
    }


    [HttpGet("GetbyId/{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var album = await _albumService.FindById(id);
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




    //Artista

        //Get By id
        [HttpGet("GetArtistbyId/{id}")]
        public async Task<IActionResult> GetArtistById(int id)
        {
            var artist = await _artistService.FindArtistById(id);
            if (artist == null)
                return NotFound($"No se encontró el artista con ID {id}");
            
            return Ok(artist);
        }

        //add artist
        [HttpPost]
        [Route("AddArtist")]
        public async Task<IActionResult> AddArtist([FromBody] Artist artist)
        {
            var response = await _artistService.AddArtist(artist);

            if (response.StatusCode == System.Net.HttpStatusCode.Created)
            {
                return CreatedAtAction(nameof(AddArtist), new { id = artist.Id }, response);
            }
            return StatusCode((int)response.StatusCode, response);
        }

        // Search By name
        [HttpGet]
        [Route("GetArtistByName")]
        public async Task<IActionResult> GetByName([FromQuery] string name)
        {
            var result = await _artistService.FindArtistByName(name);
            return StatusCode((int)result.StatusCode, result);
        }

        // Get Artist List
        [HttpGet]
        [Route("GetAllArtists")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _artistService.GetArtistList();
            return StatusCode((int)result.StatusCode, result);
        }

        // Filtrar artistas por nombre y país
        [HttpGet]
        [Route("FilterArtistByProperties")]
        public async Task<IActionResult> GetByProperties([FromQuery] string name, [FromQuery] string country)
        {
            var result = await _artistService.FindArtistByProperties(name, country);
            return StatusCode((int)result.StatusCode, result);
        }

    //Album

    // Get Artist List
        [HttpGet]
        [Route("GetAllAlbums")]
        public async Task<IActionResult> GetAlbumList()
        {
            var result = await _albumService.GetAlbumList();
            return StatusCode((int)result.StatusCode, result);
        }




}
