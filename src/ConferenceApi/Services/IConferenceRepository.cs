using ConferenceApi.Entity;

namespace ConferenceApi.Services;

public interface IConferenceRepository
{
    Task<Conference> GetAsync(int conferenceId);
}
