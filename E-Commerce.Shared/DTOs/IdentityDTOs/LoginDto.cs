using System.ComponentModel.DataAnnotations ;

namespace E_Commerce.Shared.DTOs.IdentityDTOs ;

public record LoginDto ( [ EmailAddress ] string Email , string Password ) ;