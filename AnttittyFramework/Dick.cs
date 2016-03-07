using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnttittyFramework
{
    public class DBContainer
    {
        private string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.System), "fuck/de/jonas");
        private string SavePath
        {
            get
            {
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                return path;
            }
        }
        public void Save(object obj)
        {
            string a = JsonConvert.SerializeObject(obj);
            
        }
    }
}
