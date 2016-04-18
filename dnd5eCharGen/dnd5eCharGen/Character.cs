using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dnd5eCharGen
{
    class Character
    {
        public List<Class> Classes { get; set; }
        public string Name { get; set; }
        public int Level { get; set; }

        void updateLevel()
        {
            Level = Classes.Count;
        }
    }
}
