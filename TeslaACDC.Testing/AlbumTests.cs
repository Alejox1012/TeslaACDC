using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using TeslaACDC.Business.Services;
using TeslaACDC.Business.Interfaces;
using TeslaACDC.Data.IRepository;
using TeslaACDC.Data.Models;
using Microsoft.EntityFrameworkCore.Query.Internal;
using System.Net;
using NSubstitute;
using NSubstitute.ExceptionExtensions;

namespace TeslaACDC.Tests;

[TestClass]
public class AlbumTests
{

    // Teacher´s Method
    private IAlbumRepository<int, Album> _albumRepository;
    private readonly Album _correctAlbum;

    public AlbumTests()
    {
        _albumRepository = Substitute.For<IAlbumRepository<int, Album>>();
        _correctAlbum = new Album()
        {
            Id = 1,
        };
    }

    // other Method
    private Mock<IAlbumRepository<int, Album>> _mockAlbumRepository;
   
    private AlbumService _albumService;

    [TestInitialize]
    public void Setup()
   {
        _mockAlbumRepository = new Mock<IAlbumRepository<int, Album>>();
       //Ahora podemos inyectar el mock en AlbumService
        _albumService = new AlbumService(_mockAlbumRepository.Object);
    }

 
    [TestMethod]
    public async Task HealthCheckTest()
    {
        // 🟠 Arrange: Crear el mock del repositorio
        var mockRepo = new Mock<IAlbumRepository<int, Album>>();
        var service = new AlbumService(mockRepo.Object);

        // 🟢 Act: Llamar al método que estamos probando
        var response = await service.HealthCheckTest();

        // 🔵 Assert: Verificar que el resultado es el esperado
        Assert.AreEqual("OK", response);
    }

    [TestMethod]
    public async Task HealthCheckTestIsOK()
    {
        // 🟠 Arrange: Crear el mock del repositorio
        var mockRepo = new Mock<IAlbumRepository<int, Album>>();
        var service = new AlbumService(mockRepo.Object);

        // 🟢 Act: Llamar al método que estamos probando
        var response = await service.HealthCheckTest(true);

        // 🔵 Assert: Verificar que el resultado es el esperado
        Assert.AreEqual(response,"OK!");
    }

    [TestMethod]
    public async Task HealthCheckTestIsNotOK()
    {
        // 🟠 Arrange: Crear el mock del repositorio
        var mockRepo = new Mock<IAlbumRepository<int, Album>>();
        var service = new AlbumService(mockRepo.Object);

        // 🟢 Act: Llamar al método que estamos probando
        var response = await service.HealthCheckTest(false);

        // 🔵 Assert: Verificar que el resultado es el esperado
        Assert.AreEqual(response, "Not OK!");
    }
    [TestMethod]
    public async Task ValidateAlbumCreation()
    {
        // 🟠 Arrange: Crear el mock del repositorio
        var mockRepo = new Mock<IAlbumRepository<int, Album>>();
        var service = new AlbumService(mockRepo.Object);
        var album = new Album(){
            Name = "Rude Awakening (RE-MASTERED)",
            Year = 2025,
            Genre = Genre.Rock,
            Id = 10
        };


        // 🟢 Act: Llamar al método que estamos probando
        var response = await service.TestAlbumCreation(album);

        // 🔵 Assert: Verificar que el resultado es el esperado
        Assert.AreEqual(response, String.Empty);
    }
    [TestMethod]
    public async Task ValidateAlbumCreation_nameisempty()
    {
        // 🟠 Arrange: Crear el mock del repositorio
        var mockRepo = new Mock<IAlbumRepository<int, Album>>();
        var service = new AlbumService(mockRepo.Object);
        var album = new Album(){
            Name = "",
            Year = 2025,
            Genre = Genre.Rock,
            Id = 10
        };


        // 🟢 Act: Llamar al método que estamos probando
        var response = await service.TestAlbumCreation(album);

        // 🔵 Assert: Verificar que el resultado es el esperado
        Assert.IsTrue(response.Contains("Name is required"));
    }
    [TestMethod]
    public async Task ValidateAlbumCreation_YearOutOfRangeHigher()
    {
        // 🟠 Arrange: Crear el mock del repositorio
        var mockRepo = new Mock<IAlbumRepository<int, Album>>();
        var service = new AlbumService(mockRepo.Object);
        var album = new Album(){
            Name = "Rude Awakening (RE-MASTERED)",
            Year = 2026,
            Genre = Genre.Rock,
            Id = 10
        };


        // 🟢 Act: Llamar al método que estamos probando
        var response = await service.TestAlbumCreation(album);

        // 🔵 Assert: Verificar que el resultado es el esperado
        Assert.IsTrue(response.Contains("The year must be between 1901 and 2025"));
    }
    [TestMethod]
    public async Task ValidateAlbumCreation_YearOutOfRangeLower()
    {
        // 🟠 Arrange: Crear el mock del repositorio
        var mockRepo = new Mock<IAlbumRepository<int, Album>>();
        var service = new AlbumService(mockRepo.Object);
        var album = new Album(){
            Name = "Rude Awakening (RE-MASTERED)",
            Year = 1889,
            Genre = Genre.Rock,
            Id = 10
        };


        // 🟢 Act: Llamar al método que estamos probando
        var response = await service.TestAlbumCreation(album);

        // 🔵 Assert: Verificar que el resultado es el esperado
        Assert.IsTrue(response.Contains("The year must be between 1901 and 2025"));
    }
    
    
[TestMethod]
public async Task FindById_FindsSomething()
{
    // 🟠 Arrange: Crear el mock del repositorio
    var album = new Album()
    {
        Name = "Rude Awakening (RE-MASTERED)",
        Year = 2025,
        Genre = Genre.Rock,
        Id = 10
    };

    _mockAlbumRepository.Setup(x => x.FindAsync(10)).ReturnsAsync(album);

    var service = new AlbumService(_mockAlbumRepository.Object);

    // 🟢 Act: Llamar al método que estamos probando
    var response = await service.FindAlbumById(10);

    // 🔵 Assert: Verificar que el resultado es el esperado
    Assert.IsNotNull(response);
    Assert.AreEqual("Álbum encontrado con éxito", response.Message);
    Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
    Assert.AreEqual(1, response.TotalElements);
    Assert.AreEqual(album, response.ResponseElements.FirstOrDefault());
}


    
[TestMethod]
public async Task FindById_FindsNothing()
{
    // 🟠 Arrange: Configurar el mock del repositorio para devolver null
    _mockAlbumRepository.Setup(x => x.FindAsync(It.IsAny<int>())).ReturnsAsync((Album)null);

    var service = new AlbumService(_mockAlbumRepository.Object);

    // 🟢 Act: Llamar al método que estamos probando
    var response = await service.FindAlbumById(999); // Usar un ID que no exista

    // 🔵 Assert: Verificar que el resultado es el esperado
    Assert.IsNotNull(response);
    Assert.AreEqual("Álbum no encontrado", response.Message);
    Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
    Assert.AreEqual(0, response.TotalElements);
    Assert.IsFalse(response.ResponseElements.Any());
}



[TestMethod]
public async Task FindById_FindsElements()
{
     // 🟠 Arrange: Crear el mock del repositorio
     // + Mocking
        _albumRepository.FindAsync(1).ReturnsForAnyArgs(Task.FromResult<Album>(_correctAlbum));
        var service = new AlbumService(_albumRepository);

        // 🟢 Act: Llamar al método que estamos probando
        var response = await service.FindById(1);

        // 🔵 Assert: Verificar que el resultado es el esperado
        Assert.IsNotNull(response);
        Assert.AreEqual("Álbum encontrado", response.Message);
        Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        Assert.AreEqual(1, response.TotalElements);
        Assert.IsTrue(response.ResponseElements.Any());
}

[TestMethod]
public async Task FindById_NotFoundElements()
{
     // 🟠 Arrange: Crear el mock del repositorio
     // + Mocking
        _albumRepository.FindAsync(1).ReturnsForAnyArgs(Task.FromResult<Album>(null));
        var service = new AlbumService(_albumRepository);

        // 🟢 Act: Llamar al método que estamos probando
        var response = await service.FindById(9);

        // 🔵 Assert: Verificar que el resultado es el esperado
        Assert.IsNotNull(response);
        Assert.AreEqual("Álbum no encontrado", response.Message);
        Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
        Assert.AreEqual(0, response.TotalElements);
        Assert.IsFalse(response.ResponseElements.Any());
   
}



[TestMethod]
public async Task FindById_ThrowException()
{
    // 🟠 Arrange: Crear el mock del repositorio
    _albumRepository.FindAsync(Arg.Any<int>()).ThrowsAsync(new Exception("ERROR"));
    
    var service = new AlbumService(_albumRepository);

    // 🟢 Act & 🔵 Assert: Verificar que lanza una excepción
    await Assert.ThrowsExceptionAsync<Exception>(async () => await service.FindById(5));
}




// [TestMethod]
//public async Task HealthCheckTestFailed()
//{
//    // 🟠 Arrange: Crear el mock del repositorio
//    var mockRepo = new Mock<IAlbumRepository<int, Album>>();
//    var service = new AlbumService(mockRepo.Object);
//
//    // 🟢 Act: Llamar al método que estamos probando
//    var response = await service.HealthCheckTest();
//
//    // 🔵 Assert: Verificar que el resultado es el esperado
//    Assert.AreNotEqual("OK", response);
//}
//



    // [ TestMethod]
    //   public async Task HealthCheckTestFailed()
    //
    //   // Arrange
    //   var service = new AlbumService(null);
    //   // Act
    //   var response = await service.HealthCheckTest();
    //   // Assert
    //   Assert.AreNotEqual(response,"ok");
    // 
    //
}
    