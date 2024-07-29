using ConferenceApi.Entity;
using MongoDB.Driver;

namespace ConferenceApi.Services;

public class ConferenceRepository : IConferenceRepository
{
    private readonly IMongoCollection<Conference> _conferences;

    public ConferenceRepository(IMongoDatabase database)
    {

        _conferences = database.GetCollection<Conference>("Conference");
    }
    public async Task<Conference> GetAsync(int conferenceId)
    {
        return await _conferences
            .Find(x => x.Id == conferenceId)
            .FirstOrDefaultAsync();
    }

}
