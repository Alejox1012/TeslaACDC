namespace TeslaACDC.Controllers;

using Microsoft.AspNetCore.Mvc;
using TeslaACDC.Data.Models;
using TeslaACDC.Business.Services;
using TeslaACDC.Business.Interfaces;
using TeslaACDC.Data.DTO;
using System.Net;

[ApiController]  //@Controller
[Route("api/[controller]")]
public class TeslaController : ControllerBase
{

    private readonly IAlbumService _albumService;
    private readonly IMatematika _matematikaService;

    public TeslaController(IAlbumService albumService, IMatematika matematika)
    {
        _albumService = albumService;
        _matematikaService = matematika;
    }

/*

    // Obtiene un solo álbum
    [HttpGet]
    [Route("GetAlbum")]
    public async Task<IActionResult> GetAlbum()
    {
        var album = await _albumService.GetAlbum();
        return Ok(album);
    }

    // Obtiene toda la lista de álbumes
    [HttpGet]
    [Route("GetAlbumsList")]
    public async Task<IActionResult> GetAlbumsList()
    {
        var lista = await _albumService.GetAlbumsList();
        return Ok(lista);
    }

    // lista + message
    [HttpGet]
    [Route("ListAlbums")]
    public async Task<IActionResult> Getlist()
    {
        return Ok(await _albumService.Getlist());
    }
    // Buscar por id
    [HttpGet]
    [Route("GetAlbumById")]
    public async Task<IActionResult> GetAlbumById(int id)
    {
        var response = await _albumService.FindById(id);
        return StatusCode((int)response.StatusCode, response);
    }

    // Search for name
    [HttpGet]
    [Route("GetAlbumByName")]
    public async Task<IActionResult> GetAlbumByName(string name)
    {
        var response = await _albumService.FindByName(name);
        return StatusCode((int)response.StatusCode, response);
    }








    [HttpGet]
    [Route("FindByArtist/{artistId}")]
    public async Task<IActionResult> FindByArtist(int artistId)
    {
        var albums = await _albumService.FindByArtist(artistId);
        return StatusCode((int)albums.StatusCode, albums);
    }

    [HttpGet]
    [Route("FindByYearRange/{startYear}/{endYear}")]
    public async Task<IActionResult> FindByYearRange(int startYear, int endYear)
    {
        var albums = await _albumService.FindByYearRange(startYear, endYear);
        return StatusCode((int)albums.StatusCode, albums);
    }

    [HttpGet]
    [Route("FindByGenre/{genre}")]
    public async Task<IActionResult> FindByGenre(Genre genre)
    {
        var albums = await _albumService.FindByGenre(genre);
        return StatusCode((int)albums.StatusCode, albums);
    }

    [HttpPost]
    [Route("AddAlbum")]
    public async Task<IActionResult> AddAlbum([FromBody] Album newAlbum)
    {
        if (newAlbum == null)
            return BadRequest("El cuerpo de la solicitud no puede estar vacío.");

        var response = await _albumService.AddAlbum(newAlbum);
        return StatusCode((int)response.StatusCode, response);
    }



    [HttpDelete]
    [Route("DeleteAlbum/{id}")]
    public async Task<IActionResult> DeleteAlbum(int id)
    {
        var response = await _albumService.DeleteAlbum(id);
        return StatusCode((int)response.StatusCode, response);
    }




    [HttpPut]
    [Route("EditAlbum")]
    public async Task<IActionResult> EditAlbum([FromBody] Album updatedAlbum)
    {
        var response = await _albumService.EditAlbum(updatedAlbum);
        return StatusCode((int)response.StatusCode, response);
    }











    // Agrega un nuevo álbum (POST)


    [HttpPost]
    [Route("AddTwoNumbers")]
    public async Task<IActionResult> AddTwoNumbers(Sumatoria sumatoria)
    {
        if (sumatoria.SumandoA == 0 && sumatoria.SumandoB == 0)
            return BadRequest("Se requieren SumandoA y SumandoB para la suma.");

        var resultado = await _matematikaService.AddTwoNumbers(sumatoria.SumandoA, sumatoria.SumandoB);
        return Ok(new { resultado = resultado });
    }

    [HttpPost]
    [Route("CalculateSquareArea")]
    public async Task<IActionResult> CalculateSquareArea(Squarearea request)
    {
        if (request.SideLengthA <= 0 || request.SideLengthB <= 0 || request.SideLengthC <= 0 || request.SideLengthD <= 0)
        {
            return BadRequest("Los lados no pueden ser menores o iguales a cero.");
        }

        float area = await _matematikaService.CalculateSquareArea(request.SideLengthA.Value, request.SideLengthB.Value, request.SideLengthC.Value, request.SideLengthD.Value);

        if (area == 0)
        {
            return BadRequest("Los lados no coinciden.");
        }

        return Ok(new { resultado = area });
    }


    [HttpPost]
    [Route("CalculateTriangleArea")]
    public async Task<IActionResult> CalculateTriangleArea(AreaTriangulo request)
    {
        var area = await _matematikaService.CalculateTriangleArea(request.BaseT, request.AlturaT);
        //var area = (areaTrianguloDTO.BaseT * areaTrianguloDTO.AlturaT) / 2;
        return Ok(new { resultado = area });
    }

    [HttpPost]
    [Route("VoyAFallar")]
    public async Task<IActionResult> Fallo(int dividendo)
    {
        var resultado = await _matematikaService.Divide(dividendo);
        // return resultado.StatusCode == HttpStatusCode.OK ? Ok(resultado) : BadRequest(resultado);

        return resultado.StatusCode == HttpStatusCode.OK ? Ok(resultado) : StatusCode((int)resultado.StatusCode, resultado);
        /*         if(resultado.StatusCode == HttpStatusCode.OK)
                {
                    return Ok(resultado);
                }
                else
                {
                    return StatusCode((int)resultado.StatusCode, resultado);
                } */
//   }





    /*   PRIMERA ENTREGA:  
        [HttpGet]
        [Route("ListAlbums")]
        public async Task<IActionResult> Getlist()
        {
            var ListaAlbums = new List<Album>()
            {
                new Album { Nombre = "Mañana será bonito", Genero = "Urbano", Anio = 2022 },
                new Album { Nombre = "Un Verano Sin Ti", Genero = "Urbano", Anio = 2022 },
                new Album { Nombre = "Mr. Morale & The Big Steppers", Genero = "Hip-Hop", Anio = 2022 }
            };

            //agregar uno nuevo al array
            ListaAlbums.Add(new Album{Nombre ="Dark saga", Anio = 1996, Genero = "Speed Metal"});
            return Ok(ListaAlbums);
        }


        [HttpPost]
        [Route("Sum")]
        public async Task<IActionResult> Sum([FromBody] Sum request)
        {
            int resultado = request.Valor1 + request.Valor2;
            string mensaje = $"El resultado de la suma de {request.Valor1} y {request.Valor2} es: {resultado}";

            return Ok(new { Mensaje = mensaje, Resultado = resultado });
        }


        //        {
        //        "Valor1": 5,
        //        "Valor2": 3
        //        }


       [HttpPost]
       [Route("CalcularAreaCuadrado")]
        public async Task<IActionResult> CalcularAreaCuadrado([FromBody] RequestModel request)
        {
            int area = request.Lado * request.Lado;
            string mensaje = $"El área del cuadrado con lado {request.Lado} es: {area}";

            return Ok(new { Mensaje = mensaje, Area = area });
        }


     //   {
     //   "lado": 5
     // }





        [HttpPost]
        [Route("CalcularAreaTriangulo")]
        public async Task<IActionResult> CalcularAreaTriangulo([FromBody] AreaTrianguloRequest request)
        {
            double area = (request.Base * request.Altura) / 2;
            return Ok(new { Area = area });
        }


    //   {
    // "Base": 10,
    // "Altura": 5
    //  }




       [HttpPost]
       [Route("ReciboValor")]
       public async Task<IActionResult> ReciboValor(Album album)
       {
        return Ok("Mi nombre es: "+ album.Nombre);
        //return BadRequest("Esto es un error 400");

       }

       [HttpPost]
       [Route("ReciboUnValor")]
       public async Task<IActionResult> ReciboUnValor([FromBody]String album){
        return Ok(""+ album);
       }

       */


    //tres metodos:
    /*
    1ero: debe devolver un array de albums
    2do: debe recibir dos valores y sumarlos. devolver el resultado
    3ero: debe calcular el area del cuadrado.

    Extra: Crear una clase extra y poner la logia fuera del controlador
         - Capturar area de un triangulo
         - Captura de errores



         1ro : debe devolver un array de albums
         2do: debe recibir dos valores y sum
         3ro : debe calcular el area de un cuadrado
         4to: calcular el area de un triangulo
         ////    -- 5to : capturar errores --  /////
         6to: calcular el area de un cuadrado recibiendo todos los lados
         7to: PONER POPELINE DE GITHUB A FUNCIONAR


         1ro: corregir el async y el await en todos los metodos
         2do: quitar los metodos del dto
         3ero: quitar la palabra DTO en el nombre del DTO a los DTOS
         4ero: corregir que los bussines solo devuelvan el resultado t nada mas
         quitar los mensajes de string y devolver solo el resultado


         LINQ (LINK LIN-Q)
         - Acciones de un CRUD
               -Buscar por id
               -Buscar por nombre
               -Buscar por Artista
               -Agregar
               -Eliminar
               -editar


                     -buscar por nombre 
         -buscar por año de publicacion (año de inicio , año final)
         -Buscar por nombre de artista
         -buscar por genero ( album )




         - Instalar Docker Desktop
         - Hacer pull de la imagen de Postgres
         - Instalar beekeper
         - Crear una cuenta de SupaBase [https://supabase.io/]
         - entity framework + migrations

         -buscar por nombre 
         -buscar por año de publicacion (año de inicio , año final)
         -Buscar por nombre de artista
         -buscar por genero ( album )

    */




}