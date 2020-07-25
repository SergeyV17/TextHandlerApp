using System.Collections.Generic;

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
        /// <param name="lines">строки, которые будут записаны в файл</param>
        void Write(string path, List<string> lines);
    }
}
