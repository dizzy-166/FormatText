using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization; 
using TextFormats.WorkLibrary; 

namespace TextFormat.Formats.XML
{
    public class XMLTextFormat
    {
        // Метод чтения данных из XML-файла
        public LibraryCard[]? XmlMethodReading(string path)
        {
            try
            {
                // Создаём сериализатор для списка объектов LibraryCard
                XmlSerializer serializer = new XmlSerializer(typeof(List<LibraryCard>));

                // Открываем файл для чтения (если не существует — создастся пустой)
                using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate))
                {
                    // Десериализация содержимого файла в список
                    var list = (List<LibraryCard>)serializer.Deserialize(fs);
                    return list.ToArray(); // Преобразуем в массив и возвращаем
                }
            }
            catch
            {
                // При ошибке — возвращаем null
                return null;
            }
        }

        // Метод записи новой записи в XML-файл
        public int XmlMethodWriting(string path, LibraryCard card)
        {
            try
            {
                // Читаем текущие записи или создаём новый список
                List<LibraryCard> cards = XmlMethodReading(path)?.ToList() ?? new List<LibraryCard>();

                // Добавляем новую запись
                cards.Add(card);

                // Сериализуем список обратно в XML
                XmlSerializer serializer = new XmlSerializer(typeof(List<LibraryCard>));
                using (FileStream fs = new FileStream(path, FileMode.Create)) // Перезаписываем файл
                {
                    serializer.Serialize(fs, cards);
                }

                return 1; // Успешно
            }
            catch
            {
                return 0; // Ошибка
            }
        }

        // Метод поиска по подстроке в записях
        public int XmlMethodStringCheck(string? str, LibraryCard[]? cards)
        {
            try
            {
                if (cards == null || str == null) return 0; // Нет данных — ошибка

                foreach (var card in cards)
                {
                    // Проверка: содержит ли хотя бы одно поле подстроку поиска
                    if (card.Id.ToString().Contains(str) ||
                        (card.FullName?.Contains(str) ?? false) ||
                        (card.BirthDate?.Contains(str) ?? false) ||
                        (card.CardNumber?.Contains(str) ?? false))
                    {
                        // Вывод информации о найденной записи
                        Console.WriteLine($"{card.Id} - {card.FullName}\n" +
                                          $"Дата рождения - {card.BirthDate}\n" +
                                          $"Номер карты - {card.CardNumber}\n");
                    }
                }

                return 1;
            }
            catch
            {
                return 0; // Ошибка
            }
        }

        // Метод сортировки записей
        public LibraryCard[] XmlMethodSort(string? path, LibraryCard[]? cards)
        {
            try
            {
                if (cards == null) return Array.Empty<LibraryCard>(); // Нет данных — вернуть пустой массив

                // Интерфейс выбора параметра сортировки
                Console.WriteLine("Выберите параметр сортировки:\n1 - ID\n2 - Имя\n3 - Дата рождения\n4 - Номер карты");
                string? option = Console.ReadLine();

                // Интерфейс выбора направления сортировки
                Console.WriteLine("1 - По возрастанию\n2 - По убыванию");
                string? direction = Console.ReadLine();

                // Определение ключа сортировки на основе выбора пользователя
                Func<LibraryCard, object>? keySelector = option switch
                {
                    "1" => card => card.Id,
                    "2" => card => card.FullName,
                    "3" => card => card.BirthDate,
                    "4" => card => card.CardNumber,
                    _ => null
                };

                if (keySelector == null) return cards; // Если неверный выбор — вернуть без изменений

                // Сортировка по направлению
                return direction == "2"
                    ? cards.OrderByDescending(keySelector).ToArray()
                    : cards.OrderBy(keySelector).ToArray();
            }
            catch
            {
                return cards ?? Array.Empty<LibraryCard>(); // В случае ошибки — вернуть текущие или пустой массив
            }
        }
    }
}
