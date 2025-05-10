using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextFormats.WorkLibrary
{
    public class LibraryCard
    {
        public int Id { get; set; } // Идентификатор читателя
        public string? FullName { get; set; } // Полное имя читателя
        public string? BirthDate { get; set; } // Дата рождения
        public string? CardNumber { get; set; } // Номер библиотечной карты

        public LibraryCard(int id, string? fullName, string? birthDate, string? cardNumber) // Конструктор записи читателя
        {
            Id = id;
            FullName = fullName;
            BirthDate = birthDate;
            CardNumber = cardNumber;
        }
    }
}
