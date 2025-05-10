using System;
using TextFormat.Formats.JSON;
using TextFormat.Person;
using TextFormats.WorkLibrary;

namespace TextFormat.Formats.JSON
{
    public class NavigateJSON
    {
        // Метод для обработки работы с JSON файлом
        public void WorkJSON(string path)
        {
            int error = 1; // Переменная для отслеживания ошибок
            NavigateProgram navigateProgram = new NavigateProgram();
            navigateProgram.NavigateInterface();  // Отображаем интерфейс навигации

            // Считываем выбор пользователя из консоли
            string? jsonButton = Console.ReadLine();
            JSONTextFormat jsonTextFormat = new JSONTextFormat();

            // Чтение данных из JSON файла
            LibraryCard[]? cards = jsonTextFormat.JsonMethodReading(path);

            // Обработка различных команд на основе выбора пользователя
            switch (jsonButton)
            {
                case "1": // Печать всех данных
                    PrPerson printer = new PrPerson();
                    printer.PrintClassPerson(cards); // Печать всех библиотечных карт
                    break;

                case "2": // Добавление новой записи
                    Console.Write("ФИО: ");
                    string? fullName = Console.ReadLine(); // Ввод ФИО
                    Console.Write("Дата рождения: ");
                    string? birthDate = Console.ReadLine(); // Ввод даты рождения
                    Console.Write("Номер читательского билета: ");
                    string? cardNumber = Console.ReadLine(); // Ввод номера карты

                    // Генерация нового ID для новой карты
                    int nextId = cards != null ? cards.Length + 1 : 1;

                    // Создание нового объекта LibraryCard с введенными данными
                    LibraryCard newCard = new LibraryCard(nextId, fullName, birthDate, cardNumber);

                    // Запись новой карты в файл
                    error = jsonTextFormat.JsonMethodWriting(path, newCard);
                    break;

                case "3": // Сортировка данных
                    if (cards != null)
                    {
                        // Сортировка данных по выбранному параметру
                        LibraryCard[] sorted = jsonTextFormat.JsonMethodSort(path, cards);

                        // Печать отсортированных данных
                        PrPerson sorter = new PrPerson();
                        sorter.PrintClassPerson(sorted);
                    }
                    break;

                case "4": // Поиск по строке
                    Console.Write("Введите строку для поиска: ");
                    string? searchStr = Console.ReadLine(); // Ввод строки для поиска
                    error = jsonTextFormat.JsonMethodStringCheck(searchStr, cards); // Поиск в данных
                    break;

                default:
                    // Если введен неверный вариант
                    Console.WriteLine("Неверный выбор.");
                    break;
            }

            // Если произошла ошибка при выполнении операции, выводим сообщение об ошибке
            if (error == 0)
            {
                Console.WriteLine("Ошибка выполнения операции.");
            }
        }
    }
}
