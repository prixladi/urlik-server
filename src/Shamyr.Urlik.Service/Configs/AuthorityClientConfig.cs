using System;
using Shamyr.Cloud.Authority.Client;

namespace Shamyr.Urlik.Service.Configs
{
  public class AuthorityClientConfig: IAuthorityClientConfig
  {
    public Uri AuthorityUrl => throw new NotImplementedException();
  }
}
