using System.Collections.Generic;
using TextHandlerApp.Models;

namespace TextHandlerApp.Interfaces
{
    /// <summary>
    /// Интерфейс определяющий логику обработки файла
    /// </summary>
    interface ITextHandler
    {
        /// <summary>
        /// Метод обработки текстовых файлов
        /// </summary>
        /// <param name="documents">коллекция документов</param>
        /// <param name="folderPath">путь к папке для обработанных документов</param>
        /// <param name="removePunctuation">опция удаления пунктуации</param>
        /// <param name="minWordLength">опция удаление слов по заданному количеству символов</param>
        void HandleDocuments(List<Document> documents, string folderPath, Options options);

        void HandleDocument(string fromPath);

        void TextLineHandler(string line);
    }
}
