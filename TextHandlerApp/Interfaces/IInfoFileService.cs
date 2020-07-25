using System.Windows;

namespace TextHandlerApp.Interfaces
{
    /// <summary>
    /// Интерфейс открытия файла с информацией
    /// </summary>
    interface IInfoFileService
    {
        string FilePath { get; set; } // путь к файлу

        /// <summary>
        /// Метод получающий путь к файлу с информацией
        /// </summary>
        void GetPath(string filePath);

        /// <summary>
        /// Метод вызывающий окно сообщения с информацией об ошибке
        /// </summary>
        /// <param name="window">текущее окно</param>
        /// <param name="message">сообщение</param>
        void ShowInfoMessage(Window window, string filePath);

        /// <summary>
        /// Метод вызывающий окно сообщения с информацией об ошибке
        /// </summary>
        /// <param name="window">текущее окно</param>
        /// <param name="message">сообщение</param>
        void ShowErrorMessage(Window window, string message);
    }
}
