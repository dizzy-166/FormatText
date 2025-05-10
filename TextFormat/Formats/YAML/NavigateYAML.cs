using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextFormat.Person; // Для вывода объектов LibraryCard
using TextFormats.WorkLibrary; // Модель данных LibraryCard

namespace TextFormat.Formats.YAML
{
    public class NavigateYAML
    {
        // Главный метод взаимодействия с YAML файлом
        public void WorkYAML(string path)
        {
            int error = 1; // Флаг успеха/ошибки операции
            NavigateProgram navigateProgram = new NavigateProgram();

            navigateProgram.NavigateInterface();  // Вывод интерфейса выбора действия

            string? yamlButton = Console.ReadLine(); // Считываем выбор пользователя
            YAMLTextFormat yamlTextFormat = new YAMLTextFormat();

            // Чтение текущих данных из файла
            LibraryCard[]? cards = yamlTextFormat.YamlMethodReading(path);

            // Обработка выбранной пользователем команды
            switch (yamlButton)
            {
                case "1": // Печать всех записей
                    PrPerson printer = new PrPerson();
                    printer.PrintClassPerson(cards); // Вывод всех объектов LibraryCard
                    break;

                case "2": // Добавление новой записи
                    Console.Write("ФИО: ");
                    string? fullName = Console.ReadLine(); // Ввод ФИО
                    Console.Write("Дата рождения: ");
                    string? birthDate = Console.ReadLine(); // Ввод даты рождения
                    Console.Write("Номер читательского билета: ");
                    string? cardNumber = Console.ReadLine(); // Ввод номера билета

                    // Определение следующего ID (автоинкремент)
                    int nextId = cards != null ? cards.Length + 1 : 1;

                    // Создание новой карточки
                    LibraryCard newCard = new LibraryCard(nextId, fullName, birthDate, cardNumber);

                    // Запись новой карточки в файл
                    error = yamlTextFormat.YamlMethodWriting(path, newCard);
                    break;

                case "3": // Сортировка данных
                    if (cards != null)
                    {
                        // Выполнение сортировки и получение отсортированного массива
                        LibraryCard[] sorted = yamlTextFormat.YamlMethodSort(path, cards);

                        // Вывод отсортированных записей
                        PrPerson sorter = new PrPerson();
                        sorter.PrintClassPerson(sorted);
                    }
                    break;

                case "4": // Поиск по подстроке
                    Console.Write("Введите строку для поиска: ");
                    string? searchStr = Console.ReadLine(); // Ввод строки
                    error = yamlTextFormat.YamlMethodStringCheck(searchStr, cards); // Выполнение поиска
                    break;

                default: // Обработка некорректного ввода
                    Console.WriteLine("Неверный выбор.");
                    break;
            }

            // Обработка ошибки (если вернулся флаг 0)
            if (error == 0)
            {
                Console.WriteLine("Ошибка выполнения операции.");
            }
        }
    }
}
