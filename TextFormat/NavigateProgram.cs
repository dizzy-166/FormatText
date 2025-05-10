using System;
using TextFormat.Formats;
using TextFormat.Formats.TXT;
using TextFormat.Formats.CSV;
using TextFormat.Formats.JSON;
using TextFormat.Formats.XML;
using TextFormat.Formats.YAML;

namespace TextFormat
{
    public class NavigateProgram
    {
        // Метод определения формата файла и передачи управления соответствующему обработчику
        public void Navigate(string? path)
        {
            if (path.Contains(".txt")) // Обработка файлов .txt
            {
                Console.WriteLine("Выбранный формат: TXT");
                NavigateTXT navigateTXT = new NavigateTXT();
                navigateTXT.WorkTXT(path);
            }
            else if (path.Contains(".csv")) // Обработка файлов .csv
            {
                Console.WriteLine("Выбранный формат: CSV");
                NavigateCSV navigateCSV = new NavigateCSV();
                navigateCSV.WorkCSV(path);
            }
            else if (path.Contains(".json")) // Обработка файлов .json
            {
                Console.WriteLine("Выбранный формат: JSON");
                NavigateJSON navigateJSON = new NavigateJSON();
                navigateJSON.WorkJSON(path);
            }
            else if (path.Contains(".xml")) // Обработка файлов .xml
            {
                Console.WriteLine("Выбранный формат: XML");
                NavigateXML navigateXML = new NavigateXML();
                navigateXML.WorkXML(path);
            }
            else if (path.Contains(".yaml")) // Обработка файлов .yaml
            {
                Console.WriteLine("Выбранный формат: YAML");
                NavigateYAML navigateYAML = new NavigateYAML();
                navigateYAML.WorkYAML(path);
            }
            else
            {
                // Сообщение, если формат не поддерживается
                Console.WriteLine("Данный формат не поддерживается");
            }
        }

        // Метод отображения пользовательского интерфейса навигации по действиям
        public void NavigateInterface()
        {
            Console.WriteLine("Выберите действие, которое вы хотите выполнить с библиотечными картами:\n" +
                              "1 - Печать данных о читателях на экран\n" +
                              "2 - Добавление новой библиотеки карты\n" +
                              "3 - Сортировка данных по выбранному параметру\n" +
                              "4 - Поиск данных по подстроке");
        }

        // Метод отображения интерфейса выбора поля сортировки
        public void SortInterface()
        {
            Console.WriteLine("Выберите параметр для сортировки библиотеки карт:\n" +
                              "1 - По ID читателя\n" +
                              "2 - По полному имени читателя\n" +
                              "3 - По номеру читательской карты\n" +
                              "4 - По дате рождения читателя");
        }

        // Метод отображения интерфейса выбора направления сортировки
        public void SortInterfaceStatus()
        {
            Console.WriteLine("Выберите, как вы хотите отсортировать данные:\n" +
                              "1 - По возрастанию\n" +
                              "2 - По убыванию");
        }
    }
}