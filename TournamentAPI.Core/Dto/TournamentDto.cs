using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TournamentAPI.Core.Dto
{
    public class TournamentDto
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(30, ErrorMessage ="The Title can only be most of 50 characters long")]
        public string Title { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate 
        {
            get 
            { 
                return StartDate.AddMonths(3);
            }
        }
    }
}
