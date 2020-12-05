using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using MongoDB.Bson;
using Shamyr.Cloud.Authority.Client.Factories;
using Shamyr.Cloud.Authority.Models;

namespace Shamyr.Urlik.Service.Factories
{
  public class PrincipalFactory: PrincipalFactoryBase
  {
    protected override Task<ClaimsIdentity> CreateIdentityAsync(IServiceProvider serviceProvider, string authenticationType, UserModel model, CancellationToken cancellationToken)
    {
      var userId = new ObjectId(model.Id);
      return Task.FromResult<ClaimsIdentity>(new UserIdentity(userId, authenticationType));
    }

    protected override Task<IEnumerable<string>> GetRolesAsync(IServiceProvider serviceProvider, ClaimsIdentity identity, CancellationToken cancellationToken)
    {
      return Task.FromResult<IEnumerable<string>>(Array.Empty<string>());
    }
  }
}
