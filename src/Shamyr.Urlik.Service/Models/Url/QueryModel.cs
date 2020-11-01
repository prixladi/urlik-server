using System;
using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;

namespace Shamyr.Urlik.Service.Models.Url
{
  public record QueryModel
  {
    [Range(0, int.MaxValue)]
    [Required]
    public int Skip { get; set; }

    [Range(0, 200)]
    [Required]
    public int Take { get; set; }

    [Required]
    public ObjectId UserId { get; set; }
  }
}
