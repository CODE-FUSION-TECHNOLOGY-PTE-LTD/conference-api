using CommonLib;

namespace ConferenceApi.Entity;

public class AcceptPolicy : IEntity
{
    public uint Id { get; set; }
    public uint User_Id { get; set; }
    public string? ShareDataWithExhibitorsAccept { get; set; } = "0";
    public string? ShareDataWithSponsorsAccept { get; set; } = "0";
    public DateTimeOffset AcceptDate { get; set; }

}
