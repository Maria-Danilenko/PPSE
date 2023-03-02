using System;
using System.Collections;
using System.Text;
using System.Xml;

class Program
{
    private static void Main(string[] args)
    {
        //Example using System.Collections to create an array
        ArrayList list = new ArrayList();
        list.Add("one");
        list.Add(2);
        list.Add(true);

        Console.WriteLine("Using System.Collections:");
        foreach (var item in list)
        {
            Console.WriteLine(item);
        }

        //Example using System.Text to encode and decode string
        string str = "Danylenko M.V.";

        byte[] utf8Bytes = Encoding.UTF8.GetBytes(str);
        Console.WriteLine("\nUsing System.Text: \nEncoded string:");
        foreach (byte b in utf8Bytes)
        {
            Console.Write("{0:X2} ", b);
        }

        string decodedString = Encoding.UTF8.GetString(utf8Bytes);
        Console.WriteLine("\nDecoded string: {0}", decodedString);

        //Example using System.IO to read from file
        string text = File.ReadAllText("C:\\Users\\Rin\\source\\repos\\ConsoleApp2\\ConsoleApp2\\Lorem_ipsum.txt");
        Console.WriteLine("\nUsing System.IO:" + "\n"+text);

        //Example using System.Exception to output an error text
        Console.WriteLine("\nUsing System.Exception:");
        try
        {
            int zero = 0;
            int num = 100/zero;
        }
        catch (Exception ex)
        {
            Console.WriteLine("Exception: " + ex.Message);
        }

        //Example using System.Xml to read .xml file
        Console.WriteLine("\nUsing System.Xml:");
        XmlDocument doc = new XmlDocument();
        doc.Load("C:\\Users\\Rin\\source\\repos\\ConsoleApp2\\ConsoleApp2\\XMLFile.xml");
        XmlNodeList nodes = doc.DocumentElement.SelectNodes("/catalog/book");

        foreach (XmlNode node in nodes)
        {
            string author = node.SelectSingleNode("author").InnerText;
            string title = node.SelectSingleNode("title").InnerText;

            Console.WriteLine("\nAuthor: " + author);
            Console.WriteLine("Title: " + title);
        }
    }
}