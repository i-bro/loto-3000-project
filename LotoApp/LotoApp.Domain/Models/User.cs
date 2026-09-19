using LotoApp.Domain.Enums;
using System.Data;

namespace LotoApp.Domain.Models
{
    public class User : BaseEntity
    {
        public string Username { get; set; } 
        public string FirstName { get; set; }
        public string LastName { get; set; } 
        public string Password { get; set; }
        public  Role Role  { get; set; }
    }
}
