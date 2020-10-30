using MongoDB.Bson;
using Shamyr.MongoDB;
using Shamyr.MongoDB.Attributes;
using Shamyr.MongoDB.Indexes;
using System;

namespace Shamyr.Urlik.Database.Documents
{
  [Collection(nameof(DbCollections.Urls))]
  public class UrlDoc: DocumentBase
  {
    public Uri Url { get; set; }

    [Index(Unique = true)]
    public string Part { get; set; }

    [Index(Sparse = true)]
    public ObjectId? UserId { get; set; }
  }
}
