using Microsoft.Win32;
using System.IO;
using ConverterLibrary;
using System.Windows;
using Microsoft.WindowsAPICodePack.Dialogs;
using TextHandlerApp.Interfaces;

namespace TextHandlerApp.Models
{
    /// <summary>
    /// Класс взаимодействия с диалоговыми окнами
    /// </summary>
    class DialogService : IDialogService
    {
        public string FileName { get; set; } // имя файла
        public string FileType { get; set; } // тип файла
        public string FileSize { get; set; } // размер файла
        public string FilePath { get; set; } // путь к файлу

        public string FolderPath { get; set; } // путь к папке

        /// <summary>
        /// Метод взаимодействия с диалоговым окном выбора файла
        /// </summary>
        public bool OpenFileDialog()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.Filter = "Document (*.txt)|*.txt"; // Фильтр позволяющий выбрать файлы .txt формата

            if (openFileDialog.ShowDialog() == true)
            {
                // Сохранение информации о выбранном файле в свойства класса
                FileName = openFileDialog.SafeFileName;
                FileType = Path.GetExtension(openFileDialog.FileName);
                FileSize = ByteConverter.KilobyteStringFormat(ByteConverter.ConvertByteToKilobyte(new FileInfo(openFileDialog.FileName).Length));
                FilePath = openFileDialog.FileName;

                return true;
            }

            return false;
        }

        /// <summary>
        /// Метод взаимодействия с диалоговым окном выбора папки
        /// </summary>
        /// <returns></returns>
        public bool SelectFolderDialog()
        {
            CommonOpenFileDialog selectFolderdialog = new CommonOpenFileDialog();
            selectFolderdialog.IsFolderPicker = true;

            if (selectFolderdialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                //Сохранение пути к папке в свойство класса
                FolderPath = selectFolderdialog.FileName;
                return true;
            }

            return false;
        }

        /// <summary>
        /// Метод вызывающий показ информационного сообщения
        /// </summary>
        /// <param name="window">текущее окно</param>
        /// <param name="message">сообщение</param>
        public void ShowInfoMessage(Window window, string message)
        {
            MessageBox.Show(window,
                message,
                window.Title,
                MessageBoxButton.OK,
                MessageBoxImage.Information);
        }

        /// <summary>
        /// Метод вызывающий показ сообщения об ошибке
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
    }
}
