using System;
using System.IO;
using System.Windows;
using TextHandlerApp.Interfaces;

namespace TextHandlerApp.Models
{
    /// <summary>
    /// Класс считывающий текст из файла с информацией
    /// </summary>
    class InfoFileReader : IInfoFileService
    {
        public string FilePath { get; set; } // путь к файлу

        /// <summary>
        /// Метод вызывающий окно сообщения с информацией об ошибке
        /// </summary>
        /// <param name="window">текущее окно</param>
        /// <param name="message">сообщение</param>
        public void ShowInfoMessage(Window window, string filePath)
        {
            MessageBox.Show(window,
                File.ReadAllText(filePath),
                window.Title,
                MessageBoxButton.OK,
                MessageBoxImage.Information);
        }

        /// <summary>
        /// Метод вызывающий окно сообщения с информацией об ошибке
        /// </summary>
        /// <param name="window">текущее окно</param>
        /// <param name="message">сообщение</param>
        public void ShowErrorMessage(Window window, string message)
        {
            MessageBox.Show(window,
                message,
                window.Title,
                MessageBoxButton.OK,
                MessageBoxImage.Error);
        }

        /// <summary>
        /// Метод получающий путь к файлу с информацией
        /// </summary>
        /// <param name="filePath">путь к файлу</param>
        public void GetPath(string filePath)
        {
            FilePath = Path.Combine(Path.GetDirectoryName(Directory.GetParent(Environment.CurrentDirectory).ToString()), filePath);
        }
    }
}
