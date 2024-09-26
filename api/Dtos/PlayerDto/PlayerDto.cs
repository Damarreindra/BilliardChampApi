using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace billiardchamps.Dtos
{
    public class PlayerDto
    {
         public int Id {get;set;}

  [Required]
        [MaxLength(64, ErrorMessage ="Username cannot be over 64 character")]
        public string Username {get; set;} = string.Empty;

        public string PhotoUrl {get; set;} = string.Empty;
    
        public int Win {get; set;} 
    }
}