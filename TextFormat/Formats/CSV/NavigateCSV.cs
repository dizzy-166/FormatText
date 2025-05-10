using System;
using TextFormat.Formats.TXT;
using TextFormat.Person;
using TextFormats.WorkLibrary;

namespace TextFormat.Formats.CSV
{
    public class NavigateCSV
    {
        // Основной метод для работы с CSV данными
        public void WorkCSV(string path)
        {
            int error = 1; // Переменная для отслеживания ошибок
            NavigateProgram navigateProgram = new NavigateProgram();
            navigateProgram.NavigateInterface(); // Отображаем интерфейс навигации

            // Считываем выбор пользователя из консоли
            string? csvButton = Console.ReadLine();
            CSVTextFormat csvTextFormat = new CSVTextFormat(); // Создаем объект для работы с CSV
            CommonTextFormat commonTextFormat = new CommonTextFormat(); // Создаем объект для общих операций с текстом

            // Чтение данных из CSV файла
            LibraryCard[]? cards = csvTextFormat.CsvMethodReading(path);

            // Обработка выбора пользователя
            switch (csvButton)
            {
                case "1": // Печать данных
                    // Используем PrPerson для печати информации о библиотечных картах
                    PrPerson printer = new PrPerson();
                    printer.PrintClassPerson(cards); // Печать информации о картах
                    break;

                case "2": // Добавление новой записи
                    Console.Write("ФИО: ");
                    string? fullName = Console.ReadLine(); // Ввод ФИО
                    Console.Write("Дата рождения: ");
                    string? birthDate = Console.ReadLine(); // Ввод даты рождения
                    Console.Write("Номер читательского билета: ");
                    string? cardNumber = Console.ReadLine(); // Ввод номера карты

                    // Генерация нового ID для новой карты
                    int nextId = cards != null && cards.Length > 0
                        ? cards.Max(c => c.Id) + 1 // Если есть существующие карты, берем максимальный ID и увеличиваем его
                        : 1; // Если карт нет, начинаем с ID = 1

                    // Создаем новый объект LibraryCard
                    LibraryCard newCard = new LibraryCard(nextId, fullName, birthDate, cardNumber);

                    // Записываем новую карту в CSV файл
                    error = csvTextFormat.CsvMethodWriting(path, newCard);
                    break;

                case "3": // Сортировка данных
                    if (cards != null)
                    {
                        // Сортировка данных с помощью метода GeneralizedSortTextFormat
                        LibraryCard[] sorted = commonTextFormat.GeneralizedSortTextFormat(path, cards);

                        // Печать отсортированных данных
                        PrPerson sorter = new PrPerson();
                        sorter.PrintClassPerson(sorted); // Используем PrintClassPerson для вывода отсортированных данных
                    }
                    break;

                case "4": // Поиск данных
                    Console.Write("Введите строку для поиска: ");
                    string? searchStr = Console.ReadLine(); // Ввод строки для поиска
                    error = commonTextFormat.GeneralizedStringCheck(searchStr, cards); // Поиск по строке
                    break;

                default:
                    // Если пользователь выбрал неверный пункт
                    Console.WriteLine("Неверный выбор.");
                    break;
            }

            // Если при выполнении операции произошла ошибка, выводим сообщение
            if (error == 0)
            {
                Console.WriteLine("Ошибка выполнения операции.");
            }
        }
    }
}
