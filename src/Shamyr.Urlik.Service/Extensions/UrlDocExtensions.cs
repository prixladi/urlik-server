using System;
using System.Collections.Generic;
using System.Linq;
using Shamyr.Urlik.Database.Documents;
using Shamyr.Urlik.Service.Models.Url;

namespace Shamyr.Urlik.Service.Extensions
{
  public static class UrlDocExtensions
  {
    public static DetailModel ToDetailModel(this UrlDoc doc)
    {
      if (doc is null)
        throw new ArgumentNullException(nameof(doc));

      return new DetailModel
      {
        Id = doc.Id,
        Path = doc.Path,
        Url = doc.Url,
        HitCount = doc.HitCount
      };
    }

    public static PreviewModel ToPreviewModel(this UrlDoc doc)
    {
      if (doc is null)
        throw new ArgumentNullException(nameof(doc));

      return new PreviewModel
      {
        Id = doc.Id,
        Path = doc.Path,
        Url = doc.Url,
        HitCount = doc.HitCount
      };
    }

    public static IEnumerable<PreviewModel> ToPreviewModel(this IEnumerable<UrlDoc> docs)
    {
      if (docs is null)
        throw new ArgumentNullException(nameof(docs));

      return docs.Select(ToPreviewModel);
    }
  }
}
