using System;
using System.Linq.Expressions;
using static System.Runtime.InteropServices.JavaScript.JSType;

public class MenuTask
{
    protected string menuPoint; //Selected menu item

    public MenuTask()
    {
        bool loop = true; //Variable for cycle control
        while (loop)
        {
            Console.WriteLine("1 - Print the words of \"Lorem ipsum\"" +
                "\n2 - Performing a mathematical operation \n3 - Close the menu");

            Console.Write("\nEnter number of menu point: ");

            this.menuPoint = Console.ReadLine(); //Reading the point selected by the user
            loop = false;

            Menu();
        }
        
    }

    public void Menu()
    {
            int point;  //Menu point
            bool success = int.TryParse(menuPoint, out point); //Checking whether user input int

            if (success) //If point is int
            {
                switch (point)
                {
                    case 1:
                        PrintWords();
                        break;

                    case 2:
                        Console.WriteLine(MathExpression());
                        break;

                    case 3:
                        Console.WriteLine("Closed!");
                        //loop = false;

                        break;
                }

            }
            else
            {
                Console.WriteLine("Incorrect number of menu point entered");
            }
    }

    //Printing Lorem_ipsum
    public void PrintWords()
    {
        Console.WriteLine("Enter the number of words to output: ");

        //Checking for errors
        try
        {
            string numOfWordsStr = Console.ReadLine(); //Reading number of words needed to be printing
            int numOfWords; //Number of words needed to be printing
            bool success = int.TryParse(numOfWordsStr, out numOfWords); //Checking whether user input int

            if (success) //If user input is int
            {
                //Reading all text from a file
                string text = File.ReadAllText("C:\\Users\\Rin\\source\\repos\\ConsoleApp1\\ConsoleApp1\\Lorem_ipsum.txt");
                string[] words = text.Split(' ', ',', '.'); //Splitting text into words

                if (numOfWords < words.Length && numOfWords > -1)
                {
                    for (int i = 0; i < numOfWords; i++)
                    {
                        Console.WriteLine(words[i]); //Outputting words
                    }
                }
            }
            else
            {
                Console.WriteLine("Incorrect number of words entered");
            }
        }
        catch (Exception ex) { } //Catching errors
    }

    //Returning math expression
    public int MathExpression()
    {
        bool loop = true; //Variable for cycle control
        while (loop)
        {
            //Printing menu
            Console.WriteLine("\n1 - Addion \n2 - Subtraction \n3 - Multiplication " +
                "\n4 - Division \n5 - Close the menu");

            Console.Write("\nChoose mathematical operation: ");

            string menu_point = Console.ReadLine(); //Reading the point selected by the user
            int point;  //Menu point
            bool success = int.TryParse(menu_point, out point); //Checking whether user input int

            if (success && point == 5) //If user entered int and it is 5 - menu close
            {
                Console.WriteLine("Closed!");
                loop = false; //Changing for end cycle
                break;
            }

            else if (success)  //If user entered int
            {
                Console.Write("Enter first number: ");
                string firstStr = Console.ReadLine(); //Reading first number

                Console.Write("Enter second number: ");
                string secondStr = Console.ReadLine(); //Reading second number

                int firstNum, secondNum; //Int first and second number
                bool success1 = int.TryParse(firstStr, out firstNum);  //Checking whether user input int
                bool success2 = int.TryParse(secondStr, out secondNum);  //Checking whether user input int

                if (success1 && success2) //If both numbers are int 
                {
                    //Doing with numbers what user chose
                    switch (point)
                    {
                        case 1:
                            return Add(firstNum, secondNum);

                        case 2:
                            return Sub(firstNum, secondNum);

                        case 3:
                            return Mult(firstNum, secondNum);

                        case 4:
                            return Div(firstNum, secondNum);
                    }
                }
                else
                {
                    Console.WriteLine("Incorrect numbers entered");
                }
            }
            else
            {
                Console.WriteLine("Incorrect number of menu point entered");
            }
        }
        return 0; //Returning because of task
    }


    public int Add(int a, int b)
    {
        return a + b;
    }
    public int Sub(int a, int b)
    {
        return a - b;
    }
    public int Mult(int a, int b)
    {
        return a * b;
    }
    public int Div(int a, int b)
    {
        if (b != 0)
        {
            return a / b;
        }
        else
        {
            Console.WriteLine("Divide by zero");
            return 0;
        }
    }

}

internal class Program
{
    private static void Main(string[] args)
    {
        MenuTask menuTask1 = new MenuTask();
    }    
}