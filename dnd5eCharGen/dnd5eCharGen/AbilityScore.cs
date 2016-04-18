using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dnd5eCharGen
{
    class AbilityScore
    {
        public int score { get; set; }
        public int modifier { get; set; }

        public AbilityScore()
        {
            score = 10;
            modifier = 0;
        }
    }
}
