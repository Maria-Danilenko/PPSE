
internal class Program
{
    static void MathAdding() {
        Console.WriteLine("\nMath thread has started");

        Thread.Sleep(1000);
        Random rnd = new Random();
        int a = rnd.Next(101);
        int b = rnd.Next(101);
        Console.WriteLine("\nMath answer = " + (a + b));

        Console.WriteLine("Math thread has ended");
    }

    static async Task<string> ReadFromFileAsync()
    {
        Console.WriteLine("\nReadFromFile thread has started");

        using (StreamReader reader = new StreamReader("C:\\Users\\Rin\\source\\repos\\ConsoleApp3\\ConsoleApp3\\Lorem_ipsum.txt"))
        {
            return await reader.ReadToEndAsync();
        }
    }


    static void ArrayModifying()
    {
        Console.WriteLine("\nArrayModifying thread has started");

        string[] array = {"Hello", "little", "princess"};
        string symbol = ":)";
        string word = "prince";

        array[2] = word;
        array[1] = symbol;

        foreach(string str in array) {
            Console.Write(str+" ");
        }

        Console.WriteLine("\nArrayModifying thread has ended");

    }

    private static async Task Main(string[] args)
    {
        Console.WriteLine("\nMain thread");

        //Creating a new thread with the function
        Thread thread1 = new Thread(new ThreadStart(MathAdding));
        Thread thread2 = new Thread(new ThreadStart(ArrayModifying));

        thread1.Start();
        thread2.Start();

        //Async method
        string fileText = await ReadFromFileAsync();
        Console.WriteLine(fileText + "\nReadFromFileAsync thread has ended");

        //Stopping the execution of the main thread until all created threads have completed their work
        thread1.Join();
        thread2.Join();

        Console.WriteLine("\nAll threads have ended");
    }
}