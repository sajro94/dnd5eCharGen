using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace dnd5eCharGen
{
    class Roller
    {
        public int RollDice(string dies)
        {
            int result = 0;
            if (Regex.IsMatch(dies,"[0-9]+d[0-9]+"))
            {
                string[] rollParts = dies.Split('d');
                int num = Convert.ToInt32(rollParts[0]);
                int size = Convert.ToInt32(rollParts[1]);
                for (int i = 0; i < num; i++)
                {
                    result += new Random().Next(1, size);
                }
            }
            else if(Regex.IsMatch(dies, "[0-9]+"))
            {
                result = Convert.ToInt32(dies);
            }

            return result;
        }

        public int MaxDice(string dies)
        {
            int result = 0;
            if (Regex.IsMatch(dies, "[0-9]+d[0-9]+"))
            {
                string[] rollParts = dies.Split('d');
                int num = Convert.ToInt32(rollParts[0]);
                int size = Convert.ToInt32(rollParts[1]);
                for (int i = 0; i < num; i++)
                {
                    result += size;
                }
            }
            return result;
        }

        public int AvgDice(string dies)
        {
            int result = 0;
            if (Regex.IsMatch(dies, "[0-9]+d[0-9]+"))
            {
                string[] rollParts = dies.Split('d');
                int num = Convert.ToInt32(rollParts[0]);
                int size = Convert.ToInt32(rollParts[1]);
                for (int i = 0; i < num; i++)
                {
                    double avg = (size + 1)/2;
                    result += Convert.ToInt32(Math.Ceiling(avg));
                }
            }
            return result;
        }
    }
}
