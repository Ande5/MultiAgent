using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiAgentSystem.ServiceManager
{
    public class FileManager
    {
        private readonly string _path;

        public FileManager(string path) => _path = path;


        /// <summary>
        /// Сохранение карты
        /// </summary>
        /// <param name="mapDepths"></param>
        public void SaveMap(int[,] mapDepths)
        {
            using (StreamWriter fileWriter = new StreamWriter(_path))
            {
                for (int i = 0; i < mapDepths.GetLength(0); i++)
                {
                    for (int k = 0; k < mapDepths.GetLength(1); k++)
                    {
                        fileWriter.WriteLine(mapDepths[i, k]);
                    }

                    fileWriter.WriteLine(";");
                }
            }
        }
    }
}
