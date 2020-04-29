using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MatchingGame2.models.game
{
    public class GameCreateDto
    {
        [Required]
        public string Name { get; set; }
    }
}
