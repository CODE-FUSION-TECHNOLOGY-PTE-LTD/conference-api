using System;
using CommonLib;

namespace ConferenceApi.Entity;

public class Config : IEntity
{
    public uint  Id { get; set; }
    public int OrganizationId { get; set; }
    public List<Sectors>? Sectors { get; set; }
    

}

public class Sectors {

    public Guid Id { get; set; }

    public string Title { get; set; } = "";
}
