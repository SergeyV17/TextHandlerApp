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
        /// <param name="line">строка для записи в файл</param>
        public void Write(string path, string line)
        {
            using (StreamWriter sw = new StreamWriter(path, append: true))
            {
                sw.WriteLine(line);
            }
        }
    }
}
