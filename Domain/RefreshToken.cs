using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.POCOs;

namespace Domain;

public class RefreshToken
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public string Token { get; set; }

    public string JwtId { get; set; }
    
    public DateTime CreationDate { get; set; }
    
    public DateTime ExpireDate { get; set; }

    public bool Used { get; set; }
    
    public bool Invalidated { get; set; }

    public string UserId { get; set; }
    
    [ForeignKey(nameof(UserId))]
    public ApplicationUser User { get; set; }
}