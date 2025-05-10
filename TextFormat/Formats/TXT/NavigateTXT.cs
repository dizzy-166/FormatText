using System;
using TextFormat.Formats.TXT; // Подключение обработчика формата TXT
using TextFormat.Person; // Подключение класса для вывода данных о пользователях
using TextFormats.WorkLibrary; // Подключение модели LibraryCard

namespace TextFormat.Formats.TXT
{
    public class NavigateTXT
    {
        // Метод для работы с TXT форматом
        public void WorkTXT(string path)
        {
            int error = 1; // Переменная для отслеживания ошибок
            NavigateProgram navigateProgram = new NavigateProgram(); // Создаём объект для навигации
            navigateProgram.NavigateInterface();  // Отображаем интерфейс с возможными действиями

            // Ввод выбора пользователя
            string? txtButton = Console.ReadLine();
            CommonTextFormat commonTextFormat = new CommonTextFormat(); // Создаём объект для работы с текстовыми данными

            // Чтение данных из файла в зависимости от формата
            LibraryCard[]? cards = commonTextFormat.GeneralizedMethodReading(path); // Чтение данных из файла

            // Обработка выбора пользователя
            switch (txtButton)
            {
                case "1": // Печать данных
                    PrPerson printer = new PrPerson(); // Создаём объект для печати данных
                    printer.PrintClassPerson(cards); // Печать информации о пользователях
                    break;

                case "2": // Добавление нового пользователя
                    Console.Write("ФИО: ");
                    string? fullName = Console.ReadLine(); // Ввод ФИО
                    Console.Write("Дата рождения: ");
                    string? birthDate = Console.ReadLine(); // Ввод даты рождения
                    Console.Write("Номер читательского билета: ");
                    string? cardNumber = Console.ReadLine(); // Ввод номера карты

                    // Генерация следующего ID для нового пользователя
                    int nextId = cards != null ? cards.Length + 1 : 1;
                    // Создаём новый объект LibraryCard
                    LibraryCard newCard = new LibraryCard(nextId, fullName, birthDate, cardNumber);
                    // Записываем новый объект в файл
                    error = commonTextFormat.GeneralizedMethodWriting(path, newCard);
                    break;

                case "3": // Сортировка данных
                    if (cards != null)
                    {
                        // Сортировка данных по выбранному пользователем параметру
                        LibraryCard[] sorted = commonTextFormat.GeneralizedSortTextFormat(path, cards);
                        PrPerson sorter = new PrPerson(); // Создаём объект для вывода отсортированных данных
                        sorter.PrintClassPerson(sorted); // Печать отсортированных данных
                    }
                    break;

                case "4": // Поиск данных
                    Console.Write("Введите строку для поиска: ");
                    string? searchStr = Console.ReadLine(); // Ввод строки для поиска
                    // Выполняем поиск по строке
                    error = commonTextFormat.GeneralizedStringCheck(searchStr, cards);
                    break;

                default:
                    // Если введён некорректный выбор
                    Console.WriteLine("Неверный выбор.");
                    break;
            }

            // Если произошла ошибка
            if (error == 0)
            {
                Console.WriteLine("Ошибка выполнения операции.");
            }
        }
    }
}
