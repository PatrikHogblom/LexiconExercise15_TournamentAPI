using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TournamentAPI.Core.Dto
{
    public class GameDto
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(30, ErrorMessage = "The Title can only be a length of 30 characters")]
        public string Title { get; set; }
        public DateTime Time { get; set; }
        public int TournamentId { get; set; }
    }
}
