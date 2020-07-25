using System.Windows;

namespace TextHandlerApp.Interfaces
{
    /// <summary>
    /// Интерфейс определяющий логику при взаимодействии с диалоговыми окнами
    /// </summary>
    interface IDialogService
    {
        string FileName { get; set; } // имя файла
        string FileType { get; set; } // тип файла
        string FileSize { get; set; } // размер файла
        string FilePath { get; set; } // путь к файлу

        string FolderPath { get; set; } // путь к папке

        /// <summary>
        /// Метод запускающий диалог открытия (выбора) файла
        /// </summary>
        bool OpenFileDialog();

        /// <summary>
        /// Метод запускающий диалог выбора папки
        /// </summary>
        bool SelectFolderDialog();

        /// <summary>
        /// Метод вызывающий окно сообщения с информацией
        /// </summary>
        /// <param name="window">текущее окно</param>
        /// <param name="message">сообщение</param>
        void ShowInfoMessage(Window window, string message);

        /// <summary>
        /// Метод вызывающий окно сообщения с информацией об ошибке
        /// </summary>
        /// <param name="window">текущее окно</param>
        /// <param name="message">сообщение</param>
        void ShowErrorMessage(Window window, string message);
    }
}
