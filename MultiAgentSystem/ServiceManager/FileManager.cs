using System;
using System.IO;

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

        /// <summary>
        /// Загрузка карты
        /// </summary>
        /// <returns></returns>
        public int[,] LoadMap()
        {
            int[,] mapDepths;

            using (StreamReader fileReader = new StreamReader(_path))
            {
                int sizeMapY = Convert.ToInt32(fileReader.ReadLine());
                int sizeMapX = Convert.ToInt32(fileReader.ReadLine());

                mapDepths = new Int32[sizeMapY, sizeMapX];

                for (int i = 0; i < mapDepths.GetLength(0); i++)
                {
                    for (int k = 0; k < mapDepths.GetLength(1); k++)
                    {
                        mapDepths[i, k] = Convert.ToInt32(fileReader.ReadLine());
                    }

                    fileReader.ReadLine();
                }
            }

            return mapDepths;
        }
    }
}
