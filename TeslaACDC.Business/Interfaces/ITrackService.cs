namespace TeslaACDC.Business.Interfaces;
public interface ITrackService
{
   public Task<Track> FindTrackById(int id);
   public Task<Track> AddTrack(Track track);
}