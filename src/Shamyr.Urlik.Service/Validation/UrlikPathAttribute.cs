using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Shamyr.Urlik.Service.Validation
{
  public class UrlikPathAttribute: ValidationAttribute
  {
    public override bool IsValid(object? value)
    {
      if (value is string str)
        return str.All(c => Constants._AllowedPathCharacters.Contains(c));

      throw new ArgumentException("Argument type mismatch.", nameof(value));
    }

    public override string FormatErrorMessage(string name)
    {
      return $"The value of field '{name}' is not valid urlik path." +
        $" Urlik path must consits only of characters '{Constants._AllowedPathCharacters}'.";
    }
  }
}
