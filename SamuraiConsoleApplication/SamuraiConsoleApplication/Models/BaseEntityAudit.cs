using System;
using MongoDB.Bson;

namespace SamuraiConsoleApplication.Models
{
  public class BaseEntityAudit
  {
    public ObjectId Id { get; set; }
    public DateTime CreatedDate { get; set; }
  }
}
