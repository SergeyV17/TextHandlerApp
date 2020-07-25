using System.Collections.Generic;
using System.IO;
using TextHandlerApp.Interfaces;

namespace TextHandlerApp.Models
{
    /// <summary>
    /// Класс записи файла
    /// </summary>
    class FileWriter : IFileWriter
    {
        /// <summary>
        /// Метод записи коллекции строк в файл .txt формата
        /// </summary>
        /// <param name="path">путь к файлу</param>
        /// <param name="lines">коллекция строк для записи в файл</param>
        public void Write(string path, List<string> lines)
        {
            using (StreamWriter sw = new StreamWriter(path))
            {
                foreach (var line in lines)
                {
                    sw.WriteLine(line);
                }
            }
        }
    }
}
