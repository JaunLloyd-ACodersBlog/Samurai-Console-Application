using MongoDB.Bson.Serialization.Attributes;

namespace SamuraiConsoleApplication.Models
{
  public class Samurai : BaseEntityAudit
  {
    public string Name { get; set; }
    public int SwordProficiency { get; set; }
    public int BowProficiency { get; set; }
    public int SpearProficiency { get; set; }
    public int HorseRidingProficiency { get; set; }
    public string HorseName { get; set; }
  }
}
