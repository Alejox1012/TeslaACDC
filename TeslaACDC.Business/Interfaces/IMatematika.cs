using TeslaACDC.Data.Models;
namespace TeslaACDC.Business.Interfaces;

public interface IMatematika
{
    Task<float> AddTwoNumbers(float SumandoA, float SumandoB);

    Task<BaseMessage<string>> Divide(float sideLength); 
    Task<float> CalculateSquareArea(float sideLengthA,float sideLengthB,float sideLengthC,float sideLengthD); 
    Task<float> CalculateTriangleArea(float baseT, float alturaT);
}
