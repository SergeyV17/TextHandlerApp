using System;

namespace TextHandlerApp.Models
{
    /// <summary>
    /// Класс документа
    /// </summary>
    public class Document : IEquatable<Document>
    {
        /// <summary>
        /// Конструктор документа
        /// </summary>
        /// <param name="Name">имя файла</param>
        /// <param name="Type">тип файла</param>
        /// <param name="Size">размер файла</param>
        /// <param name="Path">путь к файлу</param>
        public Document(string Name, string Type, string Size, string Path)
        {
            this.Name = Name;
            this.Type = Type;
            this.Size = Size;
            this.Path = Path;
        }

        public string Name { get; set; } // имя файла
        public string Type { get; set; } // тип файла
        public string Size { get; set; } // размер файла
        public string Path { get; set; } // путь к файлу

        /// <summary>
        /// Реализация интерфейса IEquatable для исключения попытки добавления одного и того же файла
        /// </summary>
        /// <param name="other">другой файл</param>
        public bool Equals(Document other)
        {
            if (other != null)
                return this.Path == other.Path;

            return false;
        }
    }
}
