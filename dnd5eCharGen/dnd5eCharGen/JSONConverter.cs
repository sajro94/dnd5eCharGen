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
        public string[] ClassFiles { get; set; }
        public List<Class> Classes { get; set; }

        public JSONConverter()
        {
            Classes = new List<Class>();
            LoadClasses();
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
                    Classes.Add(new JsonSerializer().Deserialize<Class>(reader));
                }
            }
        }
        
    }
}
