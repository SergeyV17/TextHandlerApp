using System;
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
        /// <param name="textLineHandler">метод обработки текущей строки</param>
        /// <returns>список считанных строк</returns>
        public void Read(string path, Action<string> textLineHandler)
        {
            using (StreamReader sr = new StreamReader(path))
            {
                while (!sr.EndOfStream)
                {
                    string line = sr.ReadLine();
                    textLineHandler?.Invoke(line);
                }
            }
        }
    }
}
