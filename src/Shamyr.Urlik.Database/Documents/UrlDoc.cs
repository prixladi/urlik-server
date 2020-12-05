using System;
using MongoDB.Bson;
using Shamyr.MongoDB;
using Shamyr.MongoDB.Attributes;
using Shamyr.MongoDB.Indexes;

namespace Shamyr.Urlik.Database.Documents
{
  [Collection(nameof(DbCollections.Urls))]
  public class UrlDoc: DocumentBase
  {
    public string Url { get; set; }

    [Index(Unique = true)]
    public string Path { get; set; }

    [Index(Sparse = true)]
    public ObjectId? UserId { get; set; }

    public int HitCount { get; set; }
  }
}
