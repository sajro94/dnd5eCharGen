using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dnd5eCharGen
{
    class Character
    {
        public Dictionary<string, int> Classes { get; set; }
        public List<Feature> Features = new List<Feature>(); 
        public string Name { get; set; }
        public int Level { get; set; }
        public int Hitpoints { get; set; }
        public AbilityScore Strength { get; set; }
        public AbilityScore Dexterity { get; set; }
        public AbilityScore Constitution { get; set; }
        public AbilityScore Intelligence { get; set; }
        public AbilityScore Wisdom { get; set; }
        public AbilityScore Charisma { get; set; }

        public Character(string name)
        {
            Name = name;
            Classes = new Dictionary<string, int>();
            Level = Classes.Sum(pair => pair.Value);
            Strength = new AbilityScore();
            Dexterity = new AbilityScore();
            Constitution = new AbilityScore();
            Intelligence = new AbilityScore();
            Wisdom = new AbilityScore();
            Charisma = new AbilityScore();
        }
        
        public void PrintName(string notUsed)
        {
            Console.WriteLine("Current Character is Named: " + Name);
        }

        public void PrintClasses(string notUsed)
        {
            foreach (KeyValuePair<string,int> pair in Classes)
            {
                Console.WriteLine(pair.Key + " - " + pair.Value);
            }
        }

        public void SetAS(string command)
        {
            string[] commandParams = command.Split(' ');
            string Abs = commandParams[1];
            int score = Convert.ToInt32(commandParams[2]);
            double modifier = (score - 10) / 2;
            switch (Abs)
            {
                case "Strength":
                    Strength.score = score;
                    Strength.modifier = Convert.ToInt32(Math.Floor(modifier));
                    break;
                case "Dexterity":
                    Dexterity.score = score;
                    Dexterity.modifier = Convert.ToInt32(Math.Floor(modifier));
                    break;
                case "Constitution":
                    int oldModifier = Constitution.modifier;
                    Constitution.score = score;
                    Constitution.modifier = Convert.ToInt32(Math.Floor(modifier));
                    AdjustHitpoints(oldModifier);
                    break;
                case "Intelligence":
                    Intelligence.score = score;
                    Intelligence.modifier = Convert.ToInt32(Math.Floor(modifier));
                    break;
                case "Wisdom":
                    Wisdom.score = score;
                    Wisdom.modifier = Convert.ToInt32(Math.Floor(modifier));
                    break;
                case "Charisma":
                    Charisma.score = score;
                    Charisma.modifier = Convert.ToInt32(Math.Floor(modifier));
                    break;
            }
        }

        public void PrintFeatures(string notUsed)
        {
            foreach(Feature ft in Features)
            {
                Console.WriteLine(ft.Name);
            }
        }

        public void AddLevel(string command)
        {
            string className = command.Split(' ')[1];
            var matches = Storage.Classes.Where(p => p.Name == className);
            if (matches.Count() == 1)
            {
                var classMatches = Classes.Where(p => p.Key == className);
                if (classMatches.Count() == 1)
                {
                    Classes[className] += 1;
                }
                else
                {
                    Classes.Add(className, 1);
                }

                Class cl = matches.First();
                List<Feature> clFts = cl.Features;
                foreach (Feature ft in clFts)
                {
                    if (ft.Level == Classes[className])
                    {
                        Features.Add(ft);
                    }
                }
            }
            else
            {
                Console.WriteLine("Multiple or no classes with the name: "+className);
            }
            Level++;
        }

        public void PrintHitpoints(string notUsed)
        {
            Console.WriteLine(Name + " currently have a maximum Hitpoint value of " + Hitpoints);
        }

        private void AddHitpoints(string hitdie)
        {
            Roller dieRoller = new Roller();
            if (hitdie[0] == 'd' || hitdie[0] == 'D')
            {
                hitdie = "1" + hitdie;
            }

            if (Classes.Count == 1)
            {
                Hitpoints += dieRoller.MaxDice(hitdie.ToLower()) + Constitution.modifier;
            }
            else
            {
                Hitpoints += dieRoller.RollDice(hitdie.ToLower()) + Constitution.modifier;
            }
            
        }

        private void AdjustHitpoints(int oldMod)
        {
            Level = Classes.Count;
            int extraHitpoints = Level*(Constitution.modifier - oldMod);
            Hitpoints += extraHitpoints;
        }
    }
}
