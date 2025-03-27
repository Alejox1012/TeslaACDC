using TeslaACDC.Business.Interfaces;
using TeslaACDC.Data.IRepository;
using TeslaACDC.Data.Repository;

namespace TeslaACDC.Business.Services;

public class TrackService : ITrackService
{
    private readonly NikolaContext _context;
    private ITrackRepository<int, Track> _trackRepository;

    public TrackService(NikolaContext context)
    {
        _context = context;
        _trackRepository = new TrackRepository<int, Track>(_context);
    }

    public async Task<Track> AddTrack(Track track)
    {
        await _trackRepository.addAsync(track);
        return track;
    }

    public async Task<Track> FindTrackById(int id)
    {
        var track = await _trackRepository.FindAsync(id);
        return track;
    }
}



