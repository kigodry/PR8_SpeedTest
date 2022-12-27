using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PR8_SpeedTest
{
    class TypingData
    {
        public string Name { get; set; }
        public int Min { get; set; }
        public int Sec { get; set; }
        public int SecNext { get; set; }
        public TypingData(string name, int min = 0, int sec = 0)
        {
            Name = name;
            Min = min;
            Sec = sec;
        }

        public void Save(TypingMenu menu)
        {
            string url = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
            string dir = "Practic8.json";
            string dirs = url + "/" + dir;

            List<TypingData> list = new List<TypingData>();
            if (!File.Exists(dirs))
            {
                using StreamWriter sw = File.CreateText(dirs);
                list.Add(this);
                sw.WriteLine(JsonConvert.SerializeObject(list));
            }
            else
            {
                list = JsonConvert.DeserializeObject<List<TypingData>>(File.ReadAllText(dirs));
                list.Add(this);

                using StreamWriter sw = File.CreateText(dirs);
                sw.WriteLine(JsonConvert.SerializeObject(list));
            }
            menu.Load(list);
        }
    }
}
