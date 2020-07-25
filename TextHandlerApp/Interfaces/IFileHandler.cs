using System.Collections.Generic;
using TextHandlerApp.Models;

namespace TextHandlerApp.Interfaces
{
    /// <summary>
    /// Интерфейс определяющий логику обработки файла
    /// </summary>
    interface IFileHandler
    {
        /// <summary>
        /// Обработка нескольких документов
        /// </summary>
        /// <param name="documents">список файлов</param>
        /// <param name="toPath">путь к папке для сохранения обработанных файлов</param>
        /// <param name="removePunctuationMarks">опция удаления пунктуации</param>
        /// <param name="minWordLength">опция удаления по длине слова</param>
        void HandleDocuments(List<Document> documents, string toPath, bool removePunctuationMarks, int minWordLength);

        /// <summary>
        /// Обработка документа
        /// </summary>
        /// <param name="fromPath">путь к исходному файлу</param>
        /// <param name="toPath">путь к папке для сохранения обработанного файла</param>
        /// <param name="removePunctuationMarks">опция удаления пунктуации</param>
        /// <param name="minWordLength">опция удаления по длине слова</param>
        void HandleDocument(string fromPath, string toPath, bool removePunctuationMarks, int minWordLength);
    }
}
