using System.Net;

namespace TeslaACDC.Data.Models;

public class BaseMessage<T>
where T : class
{
       
  public string Message {get;set;} = string.Empty; // el mensaje: donde digo que esta sucediendo
  public HttpStatusCode StatusCode {get;set;}  = HttpStatusCode.OK; // el codigo de estado de la respuesta
  public int TotalElements {get;set;} // el total de elementos que se estan retornando
  public List<T> ResponseElements {get;set;} = new List<T>() ;// los elementos que se estan retornando
}