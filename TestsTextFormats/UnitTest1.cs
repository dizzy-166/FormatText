using TextFormat.Formats.TXT;
using TextFormats.WorkLibrary;

namespace TestsTextFormats
{
    public class UnitTest1
    {
        [Fact]
        public void TestWriteLibraryCardTXT()
        {
            // Arrange: создаем объект формата и тестовую карточку, задаем путь к файлу
            CommonTextFormat format = new CommonTextFormat();
            LibraryCard card = new LibraryCard(1, "Иванов Иван", "01.01.1990", "12345");
            string path = "test_write.txt";

            // Act: записываем карточку в файл
            int result = format.GeneralizedMethodWriting(path, card);

            // Assert: проверяем успешность записи и соответствие содержимого
            Assert.Equal(1, result); // метод должен вернуть 1 при успешной записи
            string[] lines = File.ReadAllLines(path); // читаем файл
            Assert.Single(lines); // в файле должна быть одна строка
            Assert.Equal("1;Иванов Иван;01.01.1990;12345", lines[0]); // проверка формата строки

            // Cleanup: удаляем временный файл после теста
            File.Delete(path);
        }

        [Fact]
        public void TestReadLibraryCardTXT()
        {
            // Arrange: создаем тестовый файл с одной строкой
            string path = "test_read.txt";
            File.WriteAllText(path, "1;Петров Петр;02.02.1992;54321");
            CommonTextFormat format = new CommonTextFormat();

            // Act: читаем файл в массив объектов
            var cards = format.GeneralizedMethodReading(path);

            // Assert: проверяем, что данные корректно считаны
            Assert.NotNull(cards); // массив не должен быть null
            Assert.Single(cards); // должен быть один элемент
            Assert.Equal(1, cards[0].Id); // проверка ID
            Assert.Equal("Петров Петр", cards[0].FullName); // проверка имени

            // Cleanup: удаляем тестовый файл
            File.Delete(path);
        }

        [Fact]
        public void TestSearchLibraryCardBySubstring()
        {
            // Arrange: создаем массив карточек и перенаправляем вывод консоли
            CommonTextFormat format = new CommonTextFormat();
            LibraryCard[] cards = new[]
            {
                new LibraryCard(1, "Смолова Анна Сергеевна", "09.09.1993", "001"),
                new LibraryCard(2, "Иванов Иван Иванович", "05.05.1970", "002")
            };
            using var sw = new StringWriter(); // перехватываем вывод в консоль
            Console.SetOut(sw);

            // Act: выполняем поиск по подстроке
            format.GeneralizedStringCheck("Анна", cards);

            // Assert: проверяем, что нужная запись попала в консольный вывод
            var output = sw.ToString();
            Assert.Contains("Смирнова Анна", output);
        }

        [Fact]
        public void TestSortLibraryCardsByNameAscending()
        {
            // Arrange: создаем массив карточек с разными именами
            CommonTextFormat format = new CommonTextFormat();
            LibraryCard[] cards = new[]
            {
                new LibraryCard(2, "Виктор", "01.01.1991", "222"),
                new LibraryCard(1, "Алексей", "01.01.1990", "111")
            };
            var input = new StringReader("2\n1\n"); // выбор сортировки по имени и по возрастанию
            Console.SetIn(input); // симулируем пользовательский ввод

            // Act: сортируем массив
            var sorted = format.GeneralizedSortTextFormat(null, cards);

            // Assert: проверяем порядок после сортировки
            Assert.Equal("Алексей", sorted[0].FullName);
            Assert.Equal("Виктор", sorted[1].FullName);
        }

        [Fact]
        public void TestSortLibraryCardsByIdDescending()
        {
            // Arrange: создаем массив карточек с разными ID
            CommonTextFormat format = new CommonTextFormat();
            LibraryCard[] cards = new[]
            {
                new LibraryCard(1, "Ольга", "01.01.1995", "100"),
                new LibraryCard(3, "Дмитрий", "01.01.1993", "300"),
                new LibraryCard(2, "Мария", "01.01.1994", "200")
            };
            var input = new StringReader("1\n2\n"); // выбор сортировки по ID и по убыванию
            Console.SetIn(input); // симулируем пользовательский ввод

            // Act: сортируем массив
            var sorted = format.GeneralizedSortTextFormat(null, cards);

            // Assert: проверяем, что элементы отсортированы по убыванию ID
            Assert.Equal(3, sorted[0].Id);
            Assert.Equal(2, sorted[1].Id);
            Assert.Equal(1, sorted[2].Id);
        }
    }
}
