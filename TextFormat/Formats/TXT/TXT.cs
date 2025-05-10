using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TextFormats.WorkLibrary;

namespace TextFormat.Formats.TXT
{
    public class CommonTextFormat
    {
        // Метод для чтения данных из файла и преобразования их в массив объектов LibraryCard
        public LibraryCard[]? GeneralizedMethodReading(string path)
        {
            try
            {
                // Чтение всех строк из файла по указанному пути
                var lines = File.ReadAllLines(path);

                // Инициализация массива для хранения объектов LibraryCard
                LibraryCard[] cards = new LibraryCard[lines.Length];

                // Обработка каждой строки и создание соответствующего объекта LibraryCard
                for (int i = 0; i < lines.Length; i++)
                {
                    var words = lines[i].Split(';');
                    // Заполнение массива объектов LibraryCard
                    cards[i] = new LibraryCard(
                        Convert.ToInt32(words[0]), // Преобразование ID в int
                        words[1],                  // Полное имя
                        words[2],                  // Дата рождения
                        words[3]);                 // Номер карты
                }

                return cards; // Возвращение массива объектов LibraryCard
            }
            catch
            {
                // В случае ошибки (например, файл не найден) возвращаем null
                return null;
            }
        }

        // Метод для записи данных о библиотечной карточке в файл
        public int GeneralizedMethodWriting(string path, LibraryCard card)
        {
            try
            {
                // Формирование строки для записи в файл
                string line = $"{card.Id};{card.FullName};{card.BirthDate};{card.CardNumber}";

                // Открытие файла для добавления новой строки
                using (StreamWriter sw = new StreamWriter(path, true))
                {
                    sw.WriteLine(line); // Запись строки в файл
                }
                return 1; // Возвращаем 1 при успешной записи
            }
            catch
            {
                // В случае ошибки (например, проблемы с правами доступа) возвращаем 0
                return 0;
            }
        }

        // Метод для поиска строк, содержащих заданный подстроку, в массиве библиотечных карт
        public int GeneralizedStringCheck(string? str, LibraryCard[]? cards)
        {
            try
            {
                // Если строка или массив карт равны null, сразу возвращаем 0
                if (cards == null || str == null) return 0;

                // Перебор всех карт и проверка, содержат ли они подстроку
                foreach (var card in cards)
                {
                    if (card.Id.ToString().Contains(str) ||   // Поиск по ID
                        (card.FullName?.Contains(str) ?? false) || // Поиск по полному имени
                        (card.BirthDate?.Contains(str) ?? false) || // Поиск по дате рождения
                        (card.CardNumber?.Contains(str) ?? false)) // Поиск по номеру карты
                    {
                        // Вывод информации о найденной карточке
                        Console.WriteLine($"{card.Id} - {card.FullName}\n" +
                                          $"Дата рождения - {card.BirthDate}\n" +
                                          $"Номер карты - {card.CardNumber}\n");
                    }
                }
                return 1; // Возвращаем 1, если хотя бы одна карточка найдена
            }
            catch
            {
                // В случае ошибки (например, неправильный формат данных) возвращаем 0
                return 0;
            }
        }

        // Метод для сортировки массива LibraryCard по выбранному параметру
        public LibraryCard[] GeneralizedSortTextFormat(string? path, LibraryCard[]? cards)
        {
            try
            {
                // Если массив карт равен null, возвращаем пустой массив
                if (cards == null) return Array.Empty<LibraryCard>();

                // Запрос пользователю на выбор параметра сортировки
                Console.WriteLine("Выберите параметр сортировки:\n1 - ID\n2 - Имя\n3 - Дата рождения\n4 - Номер карты");
                string? option = Console.ReadLine();

                // Запрос пользователю на выбор порядка сортировки (по возрастанию или убыванию)
                Console.WriteLine("1 - По возрастанию\n2 - По убыванию");
                string? direction = Console.ReadLine();

                // Определение ключа для сортировки в зависимости от выбора пользователя
                Func<LibraryCard, object>? keySelector = option switch
                {
                    "1" => card => card.Id,           // Сортировка по ID
                    "2" => card => card.FullName,     // Сортировка по имени
                    "3" => card => card.BirthDate,    // Сортировка по дате рождения
                    "4" => card => card.CardNumber,   // Сортировка по номеру карты
                    _ => null
                };

                // Если ключ для сортировки не был выбран, возвращаем исходный массив карт
                if (keySelector == null) return cards;

                // Сортировка массива в зависимости от выбранного порядка
                return direction == "2"
                    ? cards.OrderByDescending(keySelector).ToArray() // Сортировка по убыванию
                    : cards.OrderBy(keySelector).ToArray();          // Сортировка по возрастанию
            }
            catch
            {
                // В случае ошибки (например, некорректный ввод) возвращаем исходный массив карт
                return cards ?? Array.Empty<LibraryCard>();
            }
        }
    }
}
