using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FluxSYS_backend.Domain.Models.PrincipalModels
{
    public class UserToken
    {
        [Key]
        public int Id { get; set; }
        public string Token { get; set; }
        public int UserId { get; set; }

        [ForeignKey("UserId")]
        public Users User { get; set; }
        public DateTime ExpirationDate { get; set; }
    }
}