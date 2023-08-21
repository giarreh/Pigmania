using Microsoft.AspNetCore.Identity;

namespace Pigmania.Models
{
    public class ApplicationUser : IdentityUser
    {
       public string Playername { get; set; }
       public int TruffleCoins { get; set; }
       public int TruffleClickPower { get; set; }
    }
    
}