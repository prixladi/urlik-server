using System;
using Shamyr.Cloud.Authority.Client;

namespace Shamyr.Urlik.Service.Configs
{
  public class AuthorityClientConfig: IAuthorityClientConfig
  {
    public Uri AuthorityUrl => new Uri(EnvVariable.Get(EnvVariables._AuthorityUrl));
  }
}
