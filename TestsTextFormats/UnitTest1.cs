using TextFormat.Formats.TXT;
using TextFormats.WorkLibrary;

namespace TestsTextFormats
{
    public class UnitTest1
    {
        [Fact]
        public void TestWriteLibraryCardTXT()
        {
            // Arrange: ������� ������ ������� � �������� ��������, ������ ���� � �����
            CommonTextFormat format = new CommonTextFormat();
            LibraryCard card = new LibraryCard(1, "������ ����", "01.01.1990", "12345");
            string path = "test_write.txt";

            // Act: ���������� �������� � ����
            int result = format.GeneralizedMethodWriting(path, card);

            // Assert: ��������� ���������� ������ � ������������ �����������
            Assert.Equal(1, result); // ����� ������ ������� 1 ��� �������� ������
            string[] lines = File.ReadAllLines(path); // ������ ����
            Assert.Single(lines); // � ����� ������ ���� ���� ������
            Assert.Equal("1;������ ����;01.01.1990;12345", lines[0]); // �������� ������� ������

            // Cleanup: ������� ��������� ���� ����� �����
            File.Delete(path);
        }

        [Fact]
        public void TestReadLibraryCardTXT()
        {
            // Arrange: ������� �������� ���� � ����� �������
            string path = "test_read.txt";
            File.WriteAllText(path, "1;������ ����;02.02.1992;54321");
            CommonTextFormat format = new CommonTextFormat();

            // Act: ������ ���� � ������ ��������
            var cards = format.GeneralizedMethodReading(path);

            // Assert: ���������, ��� ������ ��������� �������
            Assert.NotNull(cards); // ������ �� ������ ���� null
            Assert.Single(cards); // ������ ���� ���� �������
            Assert.Equal(1, cards[0].Id); // �������� ID
            Assert.Equal("������ ����", cards[0].FullName); // �������� �����

            // Cleanup: ������� �������� ����
            File.Delete(path);
        }

        [Fact]
        public void TestSearchLibraryCardBySubstring()
        {
            // Arrange: ������� ������ �������� � �������������� ����� �������
            CommonTextFormat format = new CommonTextFormat();
            LibraryCard[] cards = new[]
            {
                new LibraryCard(1, "������� ���� ���������", "09.09.1993", "001"),
                new LibraryCard(2, "������ ���� ��������", "05.05.1970", "002")
            };
            using var sw = new StringWriter(); // ������������� ����� � �������
            Console.SetOut(sw);

            // Act: ��������� ����� �� ���������
            format.GeneralizedStringCheck("����", cards);

            // Assert: ���������, ��� ������ ������ ������ � ���������� �����
            var output = sw.ToString();
            Assert.Contains("�������� ����", output);
        }

        [Fact]
        public void TestSortLibraryCardsByNameAscending()
        {
            // Arrange: ������� ������ �������� � ������� �������
            CommonTextFormat format = new CommonTextFormat();
            LibraryCard[] cards = new[]
            {
                new LibraryCard(2, "������", "01.01.1991", "222"),
                new LibraryCard(1, "�������", "01.01.1990", "111")
            };
            var input = new StringReader("2\n1\n"); // ����� ���������� �� ����� � �� �����������
            Console.SetIn(input); // ���������� ���������������� ����

            // Act: ��������� ������
            var sorted = format.GeneralizedSortTextFormat(null, cards);

            // Assert: ��������� ������� ����� ����������
            Assert.Equal("�������", sorted[0].FullName);
            Assert.Equal("������", sorted[1].FullName);
        }

        [Fact]
        public void TestSortLibraryCardsByIdDescending()
        {
            // Arrange: ������� ������ �������� � ������� ID
            CommonTextFormat format = new CommonTextFormat();
            LibraryCard[] cards = new[]
            {
                new LibraryCard(1, "�����", "01.01.1995", "100"),
                new LibraryCard(3, "�������", "01.01.1993", "300"),
                new LibraryCard(2, "�����", "01.01.1994", "200")
            };
            var input = new StringReader("1\n2\n"); // ����� ���������� �� ID � �� ��������
            Console.SetIn(input); // ���������� ���������������� ����

            // Act: ��������� ������
            var sorted = format.GeneralizedSortTextFormat(null, cards);

            // Assert: ���������, ��� �������� ������������� �� �������� ID
            Assert.Equal(3, sorted[0].Id);
            Assert.Equal(2, sorted[1].Id);
            Assert.Equal(1, sorted[2].Id);
        }
    }
}
