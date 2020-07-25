using System.Collections.Generic;

namespace TextHandlerApp.Interfaces
{
    /// <summary>
    /// Интерфейс определяющий логику чтения файла
    /// </summary>
    interface IFileReader
    {
        /// <summary>
        /// Метод считывания файла
        /// </summary>
        /// <param name="path">путь к файлу</param>
        /// <returns>список прочитанных строк</returns>
        List<string> Read(string path);
    }
}
