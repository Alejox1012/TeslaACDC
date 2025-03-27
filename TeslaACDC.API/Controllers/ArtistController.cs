
/*

using Microsoft.AspNetCore.Mvc;
using TeslaACDC.Business.Interfaces;

namespace TeslaACDC.API.Controllers;

[Controller]
[Route("api/[controller]")]
public class ArtistController : ControllerBase
{
    private readonly IArtistService _artistService;

    public ArtistController(IArtistService artistService)
    {
        _artistService = artistService;
    }

    [HttpGet]
    [Route("GetbyId")]
    public async Task<IActionResult> GetById(int id)
    {
        var artist = await _artistService.FindById(id);
        return Ok(artist);
    }
    
    [HttpPost]
    [Route("AddArtist")]
    public async Task<IActionResult> AddArtist([FromBody] Artist artist)
    {
        var newArtist = await _artistService.AddArtist(artist);
        return Ok(newArtist);
    }

}

*/
