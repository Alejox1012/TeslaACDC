using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using TeslaACDC.Business.Services;
using TeslaACDC.Business.Interfaces;
using TeslaACDC.Data.IRepository;
using TeslaACDC.Data.Models;
using Microsoft.EntityFrameworkCore.Query.Internal;

namespace TeslaACDC.Tests;

[TestClass]
public class AlbumTests
{
    private Mock<IAlbumRepository<int, Album>> _mockAlbumRepository;
    private AlbumService _albumService;

    [TestInitialize]
    public void Setup()
    {
        _mockAlbumRepository = new Mock<IAlbumRepository<int, Album>>();

        // Ahora podemos inyectar el mock en AlbumService
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
    