using System;

namespace TextHandlerApp.Interfaces
{
    /// <summary>
    /// Интерфейс определяющий запись в файл
    /// </summary>
    interface IFileWriter
    {
        /// <summary>
        /// Метод записи в файл
        /// </summary>
        /// <param name="path">путь к файлу</param>
        /// <param name="line">строка для записи в файл</param>
        void Write(string path, string line);
    }
}
