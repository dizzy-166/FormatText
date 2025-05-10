using TextFormat.Formats; 

namespace TextFormat
{
    internal class Programm
    {
        static void Main(string[] args)
        {
            // Вывод приглашения пользователю ввести путь к файлу одного из поддерживаемых форматов
            Console.WriteLine("Введите название файла в одном из форматов (*.txt, *.csv, *.json, *.xml, *.yaml");

            // Считывание пути к файлу, введённого пользователем
            string? path = Console.ReadLine();

            // Проверка, существует ли указанный файл
            if (File.Exists(path))
            {
                // Если файл существует — создаём объект навигационного класса
                NavigateProgram navigateProgram = new NavigateProgram();

                // Передаём путь к файлу в метод Navigate, который вызовет соответствующий обработчик
                navigateProgram.Navigate(path);
            }
            else
            {
                // Если файл не найден — выводим сообщение об ошибке
                Console.WriteLine("Файла не существует или название введено не правильно");
            }
        }
    }
}
