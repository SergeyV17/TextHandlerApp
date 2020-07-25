using System;

namespace ConverterLibrary
{
    /// <summary>
    /// Класс конвертирования значения в байтах
    /// </summary>
    public static class ByteConverter
    {
        /// <summary>
        /// Метод конвертирования байты в килобайты
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static double ConvertByteToKilobyte(long value)
        {
            return value / 1024f;
        }

        /// <summary>
        /// Метод возвращающий значение в килобайтах в строковом формате
        /// </summary>
        /// <param name="kilobyte">значение в килобайтах</param>
        /// <returns>значение килобайта в строковом формате</returns>
        public static string KilobyteStringFormat(double kilobyte)
        {
            return string.Format("{0:N} KB", Math.Round(kilobyte, 2));
        }
    }
}
