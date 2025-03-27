using System;
using System.Diagnostics.CodeAnalysis;
using System.Net;
using TeslaACDC.Business.Interfaces;
using TeslaACDC.Data.Models;

namespace TeslaACDC.API.Services;

public class Matematika : IMatematika
{

    public async Task<float> AddTwoNumbers(float SumandoA, float SumandoB)
    {
        var sumatoria = SumandoA + SumandoB;
        return sumatoria;
    }

    public async Task<BaseMessage<string>> Divide(float SideLength)
    {
        if (SideLength == 0)
        {
            return new()
            {
                Message = "No se puede dividir entre 0",
                StatusCode = HttpStatusCode.InternalServerError,
                TotalElements = 0,
                ResponseElements = new() {}
            };
        }


        var side = 10;
        float cociente = 0f;
        try
        {
           
                cociente = side / SideLength;

        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return new()
            {
                Message = $"[ERROR]{ex.Message}",
                StatusCode = HttpStatusCode.InternalServerError,
                TotalElements = 0,
                ResponseElements = new() {}
            };
        }

        return new()
        {
            Message = "",
            StatusCode = HttpStatusCode.OK,
            TotalElements = 1,
            ResponseElements = new() { cociente.ToString() }
        };

    }


    public async Task<float> CalculateSquareArea(float sideLengthA, float sideLengthB, float sideLengthC, float sideLengthD)
    {
        if (sideLengthA == sideLengthB && sideLengthB == sideLengthC && sideLengthC == sideLengthD)
        {
            var SquareArea = sideLengthA * sideLengthB;
            return SquareArea;
        }
        else
        {
            return 0;
        }
    }

    public async Task<float> CalculateTriangleArea(float baseT, float alturaT)
    {
        var TriangleArea = ((baseT * alturaT) / 2);
        return TriangleArea;
    }
}
