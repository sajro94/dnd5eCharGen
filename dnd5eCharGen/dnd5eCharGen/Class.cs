using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dnd5eCharGen
{
    class Class
    {
        public string Name { get; set; }
        public string Hitdice { get; set; }
        public string Spellcasting { get; set; }
        public List<Feature> Features { get; set; }
    }
}
