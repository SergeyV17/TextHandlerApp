namespace TextHandlerApp.Models
{
    /// <summary>
    /// Структура для хранения настроек обработки текстовых файлов
    /// </summary>
    struct Options
    {
        public bool RemovePunctuationMarks { get; private set; }
        public int MinWordLength { get; private set; }

        /// <summary>
        /// Конструктор опций
        /// </summary>
        /// <param name="RemovePunctuationMarks">признак удаления пунктуации</param>
        /// <param name="MinWordLength">минимально допустимая длина слова</param>
        public Options(bool RemovePunctuationMarks, int MinWordLength)
            : this()
        {
            this.RemovePunctuationMarks = RemovePunctuationMarks;
            this.MinWordLength = MinWordLength;
        }
    }
}
