using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextFormat.Person; // Для вывода данных о пользователях
using TextFormats.WorkLibrary; // Модель LibraryCard

namespace TextFormat.Formats.XML
{
    public class NavigateXML
    {
        // Основной метод взаимодействия с XML-файлом
        public void WorkXML(string path)
        {
            int error = 1; // Флаг успеха/ошибки
            NavigateProgram navigateProgram = new NavigateProgram();
            navigateProgram.NavigateInterface(); // Вывод доступных действий пользователю

            string? xmlButton = Console.ReadLine(); // Ввод команды
            XMLTextFormat xmlTextFormat = new XMLTextFormat(); // Работа с XML

            // Чтение существующих данных из файла
            LibraryCard[]? cards = xmlTextFormat.XmlMethodReading(path);

            // Обработка выбора пользователя
            switch (xmlButton)
            {
                case "1": // Печать данных
                    PrPerson printer = new PrPerson();
                    printer.PrintClassPerson(cards); // Вывод всех карточек
                    break;

                case "2": // Добавление новой записи
                    Console.Write("ФИО: ");
                    string? fullName = Console.ReadLine(); // Ввод ФИО

                    Console.Write("Дата рождения: ");
                    string? birthDate = Console.ReadLine(); // Ввод даты рождения

                    Console.Write("Номер читательского билета: ");
                    string? cardNumber = Console.ReadLine(); // Ввод номера билета

                    // Генерация следующего ID
                    int nextId = cards != null ? cards.Length + 1 : 1;

                    // Создание новой записи
                    LibraryCard newCard = new LibraryCard(nextId, fullName, birthDate, cardNumber);

                    // Запись в XML
                    error = xmlTextFormat.XmlMethodWriting(path, newCard);
                    break;

                case "3": // Сортировка записей
                    if (cards != null)
                    {
                        // Сортировка массива записей
                        LibraryCard[] sorted = xmlTextFormat.XmlMethodSort(path, cards);

                        // Печать отсортированного списка
                        PrPerson sorter = new PrPerson();
                        sorter.PrintClassPerson(sorted);
                    }
                    break;

                case "4": // Поиск по подстроке
                    Console.Write("Введите строку для поиска: ");
                    string? searchStr = Console.ReadLine(); // Ввод подстроки

                    // Поиск и вывод совпадений
                    error = xmlTextFormat.XmlMethodStringCheck(searchStr, cards);
                    break;

                default: // Некорректный выбор
                    Console.WriteLine("Неверный выбор.");
                    break;
            }

            // Вывод сообщения об ошибке, если операция не удалась
            if (error == 0)
            {
                Console.WriteLine("Ошибка выполнения операции.");
            }
        }
    }
}
