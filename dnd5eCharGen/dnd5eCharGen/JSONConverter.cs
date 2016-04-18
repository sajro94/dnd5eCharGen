using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace dnd5eCharGen
{
    class JSONConverter
    {
        private readonly string _classFolder = Directory.GetCurrentDirectory() + "/classes";
        private readonly string _characterFolder = Directory.GetCurrentDirectory() + "/characters";
        public string[] ClassFiles { get; set; }
        public string[] CharacterFiles { get; set; }
        

        public JSONConverter()
        {
            if (!Storage.classLoad)
            {
                LoadClasses();
                LoadCharacters("");
                Storage.classLoad = true;
            }
        }

        public void LoadCharacters(string notUsed)
        {
            CharacterFiles = Directory.GetFiles(_characterFolder, "*.json");
            foreach(string file in CharacterFiles)
            {
                if (File.Exists(file))
                {
                    string CharacterJson = File.ReadAllText(file);
                    TextReader txtReader = new StringReader(CharacterJson);
                    JsonTextReader reader = new JsonTextReader(txtReader);
                    Storage.Characters.Add(new JsonSerializer().Deserialize<Character>(reader));
                }
            }

        }

        public void SaveCharacters()
        {
            foreach (Character ch in Storage.Characters)
            {
                string output = JsonConvert.SerializeObject(ch);
                string[] lines = output.Split(',');
                for (int u = 0; u < lines.Length-1; u++)
                {
                    lines[u] += ",";
                }
                System.IO.File.WriteAllLines(_characterFolder + "/" + ch.Name + ".json", lines);
            }
        }

        private void LoadClasses()
        {
            ClassFiles = Directory.GetFiles(_classFolder, "*.json");
            foreach (string file in ClassFiles)
            {
                if (File.Exists(file))
                {
                    string ClassJson = File.ReadAllText(file);
                    TextReader txtReader = new StringReader(ClassJson);
                    JsonTextReader reader = new JsonTextReader(txtReader);
                    Storage.Classes.Add(new JsonSerializer().Deserialize<Class>(reader));
                }
            }
        }
        
    }
}
