using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using TextHandlerApp.Interfaces;

namespace TextHandlerApp.Models
{
    /// <summary>
    /// Класс обработки текстовых файлов
    /// </summary>
    class TextHanlderService : IFileHandler
    {
        IFileReader fileReader;
        IFileWriter fileWriter;

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

        /// <summary>
        /// Метод обработки текстового файла (файлов)
        /// </summary>
        /// <param name="documentsToHandle">коллекция текстовых файлов</param>
        /// <param name="toFolderPath">путь к папке для записи обработанных текстовых файлов</param>
        /// <returns>признак успешной обработки</returns>
        public void HandleDocuments(List<Document> documentsToHandle, string toFolderPath, bool removePunctuationMarks, int minWordLength)
        {
            foreach (var document in documentsToHandle)
            {
                string saveFilePath = string.Format(@"{0}\handled_{1}", toFolderPath, document.Name);

                // Отдельная обработка каждого документа
                HandleDocument(document.Path, saveFilePath, removePunctuationMarks, minWordLength);
            }
        }

        ///// <summary>
        ///// Метод обработки текстового файла
        ///// </summary>
        ///// <param name="fromPath">путь к файлу источнику</param>
        ///// <param name="toPath">путь к файлу после обработки</param>
        ///// <returns></returns>
        public void HandleDocument(string fromPath, string toPath, bool removePunctuationMarks, int minWordLength)
        {
            // Чтение файла и сохранение прочитанных строк в коллекцию
            List<string> lines = fileReader.Read(fromPath);

            for (int i = 0; i < lines.Count; i++)
            {
                string[] currentLine;

                // Условие в зависимости от включения опции удаления знаков препинания и разбитие целой строки на массив слов
                if (removePunctuationMarks)
                    currentLine = Regex.Replace(lines[i], @"[^\w\s]", " ").Split(' '); // регулярное выражение заменяющее всё на " " кроме букв и пробелов
                else
                    currentLine = lines[i].Split(' ');

                // Цикл проверки слов в строке
                for (int j = 0; j < currentLine.Length; j++)
                {
                    // Если в наборе символов есть символы помимо букв,
                    // то опция удаления по количеству символов в данном наборе символов НЕ учитывается
                    if (currentLine[j].All(Char.IsLetter))
                    {
                        // Если у слова меньше символов, чем задано в опциях, то слово подлежит удалению
                        if (currentLine[j].Length < minWordLength)
                            currentLine[j] = "";
                    }
                }

                // Соединение слов прошедших обработку в строку
                lines[i] = string.Join(" ", currentLine).Trim();
            }

            // Запись обработанных строк в файл
            fileWriter.Write(toPath, lines);
        }
    }
}
