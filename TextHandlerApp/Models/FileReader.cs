using System.Collections.Generic;
using System.IO;
using TextHandlerApp.Interfaces;

namespace TextHandlerApp.Models
{
    /// <summary>
    ///  Класс считывания текста с файла .txt. формата
    /// </summary>
    class FileReader : IFileReader
    {
        /// <summary>
        /// Метод считывания текста с файла .txt формата
        /// </summary>
        /// <param name="path">путь к файлу</param>
        /// <returns>список считанных строк</returns>
        public List<string> Read(string path)
        {
            List<string> lines = new List<string>();

            using (StreamReader sr = new StreamReader(path))
            {
                while (!sr.EndOfStream)
                {
                    lines.Add(sr.ReadLine());
                }
            }

            return lines;
        }
    }
}
