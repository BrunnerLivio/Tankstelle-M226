using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnttittyFramework
{
    /// <summary>
    /// Represents the DbContext
    /// </summary>
    public class DbContext
    {
        private string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), @"fuck\de\jonas");
        /// <summary>
        /// Loads all objects of the given Type
        /// </summary>
        /// <typeparam name="T">The Type to load</typeparam>
        /// <returns>The objects from the DB</returns>
        public List<T> Load<T>()
        {
            List<T> items = new List<T>();
            string objectType = typeof(T).Name;
            string filesPath = Path.Combine(path, objectType);
            //Todo: Kein Try -Catch
            try
            {
                foreach (string file in Directory.GetFiles(filesPath))
                {
                    using (Stream stream = File.Open(Path.Combine(filesPath, file), FileMode.Open))
                    {
                        var binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                        items.Add((T)binaryFormatter.Deserialize(stream));
                    }

                }
            }
            catch
            {

            }
            return items;
        }
        /// <summary>
        /// Deletes everything in the Database
        /// </summary>
        public void ClearDb()
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(path);
            if (Directory.Exists(path))
            {
                foreach (FileInfo file in directoryInfo.GetFiles())
                {
                    file.Delete();
                }
                foreach (DirectoryInfo dir in directoryInfo.GetDirectories())
                {
                    dir.Delete(true);
                }
            }

        }
    }
}
