using ClosedXML.Excel;

namespace WebApplication4.Services
{
    public interface IAPIVersService
    {
        int GetRandomInteger();
        string GetRandomText();
        byte[] GenerateExcelFile();
    }

    public class APIVers1Service : IAPIVersService
    {
        private static readonly Random _random = new Random();

        public int GetRandomInteger() 
        {
            return _random.Next();
        }

        public string GetRandomText()
        {
            return null; // not used in version 1.0
        }

        public byte[] GenerateExcelFile()
        {
            return null; // not used in version 1.0
        }
    }

    public class APIVers2Service : IAPIVersService
    {
        public int GetRandomInteger()
        {
            return 0; // not used in version 2.0
        }

        public string GetRandomText()
        {
            return "Lorem ipsum dolor sit amet, consectetur adipiscing elit," +
             " sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.";
        }

        public byte[] GenerateExcelFile()
        {
            return null; // not used in version 2.0
        }
    }

    public class APIVers3Service : IAPIVersService
    {
        public int GetRandomInteger()
        {
            return 0; // not used in version 3.0
        }

        public string GetRandomText()
        {
            return null; // not used in version 3.0
        }

        public byte[] GenerateExcelFile()
        {
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Report");

                // Add some data
                worksheet.Cell("A1").Value = "Hello";
                worksheet.Cell("B1").Value = "World!";

                // Generate the file in memory and return it as byte array
                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    return stream.ToArray();
                }
            }
        }
    }

}
