using CommonLib;

namespace ConferenceApi.Entity;

public class UserConf : IEntity
{
    public uint Id { get; set; }
    public string? Email { get; set; }
    public string? Secter { get; set; }
    public string? Document { get; set; }
    public string? Departmnet { get; set; }

}
