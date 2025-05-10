using System;
using System.Collections.Generic;
using System.IO;
using YamlDotNet.Serialization; // Библиотека для работы с YAML
using TextFormats.WorkLibrary; // Пространство имён с моделью LibraryCard

namespace TextFormat.Formats.YAML
{
    public class YAMLTextFormat
    {
        // Метод чтения данных из YAML файла
        public LibraryCard[]? YamlMethodReading(string path)
        {
            try
            {
                // Считывание содержимого файла в строку
                var yamlString = File.ReadAllText(path);

                // Создание десериализатора
                var deserializer = new DeserializerBuilder().Build();

                // Десериализация строки в список объектов LibraryCard
                var cards = deserializer.Deserialize<List<LibraryCard>>(yamlString);

                // Преобразование в массив и возврат
                return cards?.ToArray();
            }
            catch
            {
                // В случае ошибки возвращаем null
                return null;
            }
        }

        // Метод записи одного объекта LibraryCard в YAML файл
        public int YamlMethodWriting(string path, LibraryCard card)
        {
            try
            {
                // Чтение существующих данных из файла (если есть)
                LibraryCard[] existingCards = YamlMethodReading(path) ?? Array.Empty<LibraryCard>();

                // Добавление нового объекта к списку
                var updatedCards = new List<LibraryCard>(existingCards) { card };

                // Создание сериализатора
                var serializer = new SerializerBuilder().Build();

                // Сериализация списка объектов в YAML формат
                string yamlString = serializer.Serialize(updatedCards);

                // Запись сериализованной строки в файл
                File.WriteAllText(path, yamlString);

                return 1; // Успешная запись
            }
            catch
            {
                return 0; // Ошибка записи
            }
        }

        // Метод поиска подстроки в элементах массива LibraryCard
        public int YamlMethodStringCheck(string? str, LibraryCard[]? cards)
        {
            try
            {
                if (cards == null || str == null) return 0;

                foreach (var card in cards)
                {
                    // Проверка наличия подстроки в любом из полей объекта
                    if (card.Id.ToString().Contains(str) ||
                        (card.FullName?.Contains(str) ?? false) ||
                        (card.BirthDate?.Contains(str) ?? false) ||
                        (card.CardNumber?.Contains(str) ?? false))
                    {
                        // Вывод совпавшей записи на экран
                        Console.WriteLine($"{card.Id} - {card.FullName}\n" +
                                          $"Дата рождения - {card.BirthDate}\n" +
                                          $"Номер карты - {card.CardNumber}\n");
                    }
                }

                return 1; // Поиск выполнен
            }
            catch
            {
                return 0; // Ошибка при поиске
            }
        }

        // Метод сортировки массива LibraryCard по выбранному критерию
        public LibraryCard[] YamlMethodSort(string? path, LibraryCard[]? cards)
        {
            try
            {
                if (cards == null) return Array.Empty<LibraryCard>();

                // Запрос параметра сортировки у пользователя
                Console.WriteLine("Выберите параметр сортировки:\n1 - ID\n2 - Имя\n3 - Дата рождения\n4 - Номер карты");
                string? option = Console.ReadLine();

                // Запрос направления сортировки
                Console.WriteLine("1 - По возрастанию\n2 - По убыванию");
                string? direction = Console.ReadLine();

                // Определение ключа сортировки на основе выбора пользователя
                Func<LibraryCard, object>? keySelector = option switch
                {
                    "1" => card => card.Id,
                    "2" => card => card.FullName,
                    "3" => card => card.BirthDate,
                    "4" => card => card.CardNumber,
                    _ => null // Некорректный выбор
                };

                if (keySelector == null) return cards;

                // Выполнение сортировки и возврат отсортированного массива
                return direction == "2"
                    ? cards.OrderByDescending(keySelector).ToArray()
                    : cards.OrderBy(keySelector).ToArray();
            }
            catch
            {
                // В случае ошибки возвращаем оригинальный массив или пустой
                return cards ?? Array.Empty<LibraryCard>();
            }
        }
    }
}
