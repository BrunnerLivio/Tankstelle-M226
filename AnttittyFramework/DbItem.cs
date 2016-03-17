using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnttittyFramework
{
    /// <summary>
    /// Represents a DbItem which can be Saved or Deleted 
    /// </summary>
    [Serializable]
    public abstract class DbItem
    {
        private string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), @"fuck\de\jonas");

        private string guid;

        /// <summary>
        /// Gives the Path to Save back
        /// </summary>
        private string SavePath
        {
            get
            {
                string savepath = Path.Combine(path, GetType().Name);
                if (!Directory.Exists(savepath))
                {
                    Directory.CreateDirectory(savepath);
                }
                return savepath;
            }
        }
        /// <summary>
        /// Saves or Updates the Object to the Database
        /// </summary>
        public void Save()
        {
            string fileToUpdate = null;
            if (!String.IsNullOrEmpty(guid))
            {
                fileToUpdate = Directory.GetFiles(SavePath).Where(f => Path.GetFileName(f).StartsWith(guid)).FirstOrDefault();
            }

            if (fileToUpdate == null)
            {
                //Add
                guid = Guid.NewGuid().ToString();
                string filePath = Path.Combine(SavePath, String.Format("{0}.bin", guid));

                using (Stream stream = File.Open(filePath, FileMode.Create))
                {
                    var binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                    binaryFormatter.Serialize(stream, this);
                }
            }
            else
            {
                using (Stream stream = File.Open(fileToUpdate, FileMode.Create))
                {
                    var binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                    binaryFormatter.Serialize(stream, this);
                }
            }

        }
        /// <summary>
        /// Removes the Object from the Database
        /// </summary>
        public void Remove()
        {
            string objectType = GetType().Name;
            string filesPath = Path.Combine(path, objectType);
            string fileToRemove = Directory.GetFiles(filesPath).Where(f => Path.GetFileName(f).StartsWith(guid)).Single();
            File.Delete(fileToRemove);
        }
    }
}
