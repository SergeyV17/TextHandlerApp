using System.Collections.Generic;
using TextHandlerApp.Models;

namespace TextHandlerApp.Interfaces
{
    /// <summary>
    /// Интерфейс определяющий логику обработки документа
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

        /// <summary>
        /// Метод обработки документа
        /// </summary>
        /// <param name="fromPath">путь к файлу</param>
        void HandleDocument(string fromPath);

        /// <summary>
        /// Метод обработки строки документа
        /// </summary>
        /// <param name="line">строка</param>
        void TextLineHandler(string line);
    }
}
