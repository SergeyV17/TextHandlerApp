using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using TextHandlerApp.Interfaces;

namespace TextHandlerApp.Models
{
    /// <summary>
    /// Класс обработки текстовых файлов
    /// </summary>
    class TextHanlderService : ITextHandler
    {
        #region Поля

        readonly IFileReader fileReader;
        readonly IFileWriter fileWriter;

        private string saveFilePath;
        private Options options;

        #endregion

        #region Конструктор

        /// <summary>
        /// Конструктор обработчика текста
        /// </summary>
        /// <param name="FileReader">класс считывающий текст из файла</param>
        /// <param name="FileWriter">класс записывающий текст в файл</param>
        public TextHanlderService(IFileReader FileReader, IFileWriter FileWriter)
        {
            this.fileReader = FileReader;
            this.fileWriter = FileWriter;
        }

        #endregion

        #region Методы

        ///// <summary>
        ///// Метод обработки текстового файла (файлов)
        ///// </summary>
        ///// <param name="documents">коллекция текстовых файлов</param>
        ///// <param name="folderPath">путь к папке для записи обработанных текстовых файлов</param>
        public void HandleDocuments(List<Document> documents, string folderPath, Options options)
        {
            foreach (var document in documents)
            {
                string saveFilePath = string.Format(@"{0}\handled_{1}_{2}", folderPath, Path.GetRandomFileName(), document.Name);

                // Сохранение текущих настроек по обработке
                this.saveFilePath = saveFilePath;
                this.options = options;

                // Обработка каждого документа
                HandleDocument(document.Path);
            }
        }

        /// <summary>
        /// Метод обработки текстового файла
        /// </summary>
        /// <param name="fromPath">путь к файлу</param>
        public void HandleDocument(string fromPath)
        {
            // Чтение, обработка, запись
            fileReader.Read(fromPath, TextLineHandler);
        }

        /// <summary>
        /// Метод обработки строки текстового файла
        /// </summary>
        /// <param name="currentLine">строка</param>
        public void TextLineHandler(string currentLine)
        {
            int lastCharIndex = currentLine.Length - 1;

            var completeLine = new StringBuilder();
            var tempWordLine = new StringBuilder();

            string handledLine = "";

            for (int i = 0; i < currentLine.Length; i++)
            {
                CharHandleConditions(completeLine, tempWordLine, currentLine[i], currentIndex: i, lastCharIndex);

                handledLine = options.RemovePunctuationMarks ?
                    HandleLineWithPunctuation(completeLine, currentLine[i]) :
                    HandleLineWithoutPunctuation(completeLine, currentLine[i]);
            }

            // Запись обработанной строки в текстовый файл
            fileWriter.Write(saveFilePath, handledLine);
        }

        /// <summary>
        /// Условия проверки текущего символа
        /// </summary>
        /// <param name="completeLine">обработанная линия</param>
        /// <param name="tempWordLine">линия обработки текущего слова</param>
        /// <param name="currentChar">текущий символ</param>
        /// <param name="currentIndex">индекс символа</param>
        /// <param name="lastCharIndex">индекс последнего символа</param>
        private void CharHandleConditions(StringBuilder completeLine, StringBuilder tempWordLine, char currentChar, int currentIndex, int lastCharIndex)
        {
            // Если символ не пробел и не пунктуация
            if (!char.IsWhiteSpace(currentChar) && !char.IsPunctuation(currentChar))
                tempWordLine.Append(currentChar);

            // Если символ пробел или пунктуация или является последним символом в строке
            if (char.IsWhiteSpace(currentChar) || char.IsPunctuation(currentChar) || currentIndex == lastCharIndex)
            {
                // Если хотя бы один символ в формирующей строке не является буквой, 
                // то данный набор символов не является словом, обработка опускается, набор символов добавляется в строку
                if (tempWordLine.ToString().Any(s => !char.IsLetter(s)))
                {
                    completeLine.Append(tempWordLine);
                }
                else
                {
                    // Если сформированное слово больше либо равно минимально допустимой длине слова, то слово добавляется в строку
                    if (tempWordLine.Length >= options.MinWordLength)
                        completeLine.Append(tempWordLine);
                }

                // Очистка после обработки слова
                tempWordLine.Clear();
            }
        }

        /// <summary>
        /// Обработка документа без удаления пунктуации
        /// </summary>
        /// <param name="completeLine">обработанная линия</param>
        /// <returns>обработанная строка(линия)</returns>
        private string HandleLineWithoutPunctuation(StringBuilder completeLine, char currentChar)
        {
            // Если обрабатываемый символ пунктуация или пробел, то символ добавляется в строку
            if (char.IsWhiteSpace(currentChar) || char.IsPunctuation(currentChar))
                completeLine.Append(currentChar);

            return completeLine.ToString();
        }

        /// <summary>
        /// Обработка документа с удалением пунктуации
        /// </summary>
        /// <param name="completeLine">будущая линия после обработки</param>
        /// <returns>обработанная строка(линия)</returns>
        private string HandleLineWithPunctuation( StringBuilder completeLine, char currentChar)
        {
            // Если обрабатываемый пробел, то символ добавляется в строку
            if(char.IsWhiteSpace(currentChar))
                completeLine.Append(currentChar);

            return completeLine.ToString();
        }

        #endregion
    }
}
