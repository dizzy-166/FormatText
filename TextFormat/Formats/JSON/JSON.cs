using System;
using System.IO;
using System.Text.Json;
using TextFormats.WorkLibrary;

namespace TextFormat.Formats.JSON
{
    public class JSONTextFormat
    {
        // Метод для чтения данных из JSON файла
        public LibraryCard[]? JsonMethodReading(string path)
        {
            try
            {
                // Чтение всего содержимого файла в строку
                string jsonString = File.ReadAllText(path);

                // Десериализация строки JSON в массив объектов LibraryCard
                LibraryCard[] cards = JsonSerializer.Deserialize<LibraryCard[]>(jsonString) ?? Array.Empty<LibraryCard>();

                return cards; // Возвращаем массив объектов LibraryCard
            }
            catch
            {
                // В случае ошибки (например, файл не найден или некорректный формат JSON) возвращаем null
                return null;
            }
        }

        // Метод для записи данных о библиотечной карточке в JSON файл
        public int JsonMethodWriting(string path, LibraryCard card)
        {
            try
            {
                // Чтение существующих данных из JSON файла
                LibraryCard[] existingCards = JsonMethodReading(path) ?? Array.Empty<LibraryCard>();

                // Создание нового массива, который будет содержать старые данные плюс новая карточка
                var updatedCards = new LibraryCard[existingCards.Length + 1];
                Array.Copy(existingCards, updatedCards, existingCards.Length);
                updatedCards[existingCards.Length] = card; // Добавление новой карты в конец массива

                // Сериализация обновленного массива в строку JSON с отступами для лучшей читаемости
                string jsonString = JsonSerializer.Serialize(updatedCards, new JsonSerializerOptions { WriteIndented = true });

                // Запись строки JSON в файл
                File.WriteAllText(path, jsonString);
                return 1; // Возвращаем 1 при успешной записи
            }
            catch
            {
                // В случае ошибки (например, проблемы с правами доступа или некорректным JSON) возвращаем 0
                return 0;
            }
        }

        // Метод для поиска по строке в массиве библиотечных карт
        public int JsonMethodStringCheck(string? str, LibraryCard[]? cards)
        {
            try
            {
                // Если строка или массив карт равны null, возвращаем 0
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
        public LibraryCard[] JsonMethodSort(string? path, LibraryCard[]? cards)
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
