using System;

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
        /// <param name="path">путь к файлу</param  
        /// <returns>список прочитанных строк</returns>
        void Read(string path, Action<string> handler);
    }
}
