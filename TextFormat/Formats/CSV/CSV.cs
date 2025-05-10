using System;
using System.IO;
using TextFormats.WorkLibrary;

namespace TextFormat.Formats.TXT
{
    public class CSVTextFormat
    {
        // Метод для чтения данных из CSV файла
        public LibraryCard[]? CsvMethodReading(string path)
        {
            try
            {
                int i = 0;

                // Считываем количество строк в файле
                using (StreamReader sr = new StreamReader(path))
                {
                    // Пока не достигнут конец файла, увеличиваем счетчик строк
                    while (sr.ReadLine() != null)
                    {
                        i++;
                    }
                }

                // Создаем массив с нужным количеством элементов
                LibraryCard[] cards = new LibraryCard[i];

                // Чтение данных из файла
                using (StreamReader sr = new StreamReader(path))
                {
                    string? line;
                    int index = 0;

                    // Пока не достигнут конец файла, обрабатываем каждую строку
                    while ((line = sr.ReadLine()) != null)
                    {
                        // Разделяем строку на части по разделителю ";"
                        string[] parts = line.Split(';');

                        // Создаем объект LibraryCard и добавляем его в массив
                        cards[index++] = new LibraryCard(
                            Convert.ToInt32(parts[0]), // ID
                            parts[1],                  // ФИО
                            parts[2],                  // Дата рождения
                            parts[3]                   // Номер карты
                        );
                    }
                }

                return cards; // Возвращаем массив объектов LibraryCard
            }
            catch
            {
                // Если произошла ошибка, возвращаем null
                return null;
            }
        }

        // Метод для добавления нового объекта LibraryCard в CSV файл
        public int CsvMethodWriting(string path, LibraryCard newCard)
        {
            try
            {
                // Открываем файл в режиме добавления (если файл не существует, он будет создан)
                using (StreamWriter sw = new StreamWriter(path, append: true))
                {
                    // Форматируем данные новой карты и записываем их в файл
                    sw.WriteLine($"{newCard.Id};{newCard.FullName};{newCard.BirthDate};{newCard.CardNumber}");
                }

                return 0; // Возвращаем 0, что означает успешную запись
            }
            catch (Exception ex)
            {
                // Если произошла ошибка при записи, выводим сообщение об ошибке
                Console.WriteLine($"Ошибка при записи в файл: {ex.Message}");

                return 1; // Возвращаем 1, что означает ошибку
            }
        }
    }
}
