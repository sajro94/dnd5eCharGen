using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace dnd5eCharGen
{
    class CLI
    {
        public Dictionary<string, Action<string>> Commands = new Dictionary<string, Action<string>>() ;
        public Character CurrentChar;
        private bool AdditionalCommands = false;

        public CLI()
        {
            Commands.Add("CreateChar [A-Z]*", CreateCharacter);
            Commands.Add("Load [A-Z][A-z]*", LoadCharacter);
        }

        private void CreateCharacter(string command)
        {
            string CharName = command.Split(' ')[1];
            CurrentChar = new Character(CharName);
            InitiateCommands();

        }

        private void SaveCharacter(string notUsed)
        {
            Storage.Characters.Add(CurrentChar);
            new JSONConverter().SaveCharacters();
        }

        private void LoadCharacter(string command)
        {
            string characterName = command.Split(' ')[1];
            var matches = Storage.Characters.Where(p => p.Name == characterName);
            if (matches.Count() == 1)
            {
                CurrentChar = matches.First();
            }
            else
            {
                Console.WriteLine("Multiple or no Characters with the name: " + characterName);
            }
            InitiateCommands();
        }

        private void PrintClasses(string notUsed)
        {
            int i = 1;
            foreach (Class cl in Storage.Classes)
            {
                Console.WriteLine("Class "+i+": "+cl.Name);
                i++;
            }
        }

        private void InitiateCommands()
        {
            if (!AdditionalCommands)
            {
                Commands.Add("AddLevel [A-Z][a-z]*", CurrentChar.AddLevel);
                Commands.Add("PrintName", CurrentChar.PrintName);
                Commands.Add("PrintClasses", CurrentChar.PrintClasses);
                Commands.Add("PrintFeatures", CurrentChar.PrintFeatures);
                Commands.Add("PrintHitpoints", CurrentChar.PrintHitpoints);
                Commands.Add("SetAS (Strength|Dexterity|Constitution|Intelligence|Wisdom|Charisma) [1-9][0-9]*", CurrentChar.SetAS);
                Commands.Add("Save", SaveCharacter);
                Commands.Add("Show Classes", PrintClasses);

                AdditionalCommands = true;
            }
        }
    }
}
