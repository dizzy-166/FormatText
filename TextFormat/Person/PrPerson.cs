using System;
using TextFormats.WorkLibrary;

namespace TextFormat.Person
{
    public class PrPerson
    {
        /// Метод вывода информации о читательских карточках на консоль
        public void PrintClassPerson(LibraryCard[]? person)
        {
            // Проверка, если массив пустой или null
            if (person == null || person.Length == 0)
            {
                Console.WriteLine("Нет данных для отображения.");
                return;
            }

            // Вывод данных каждого пользователя
            foreach (var p in person)
            {
                Console.WriteLine($"ID: {p.Id}");
                Console.WriteLine($"ФИО: {p.FullName}");
                Console.WriteLine($"Дата рождения: {p.BirthDate}");
                Console.WriteLine($"Номер карты: {p.CardNumber}");
                Console.WriteLine(new string('-', 40)); // Разделительная линия между пользователями
            }
        }
    }
}
