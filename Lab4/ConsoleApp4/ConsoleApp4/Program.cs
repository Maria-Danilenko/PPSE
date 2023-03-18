using System;
using System.Reflection;

internal class Program
{
    class TestClass
    {
        public int publicIntField;
        private string privateStringField;
        protected bool protectedBoolField;
        internal float internalFloatField;
        protected internal double protectedInternalDoubleField;

        public void ShowMethod()
        {
            Console.WriteLine("Method ShowMethod");
        }

        private string ReturnNumsMethod(int a, double b)
        {
            return $"Num №1: {a}, Num №2: {b} \tSum = {a+b}";
        }

    }
    private static void Main(string[] args)
    {
        //Return type of TestClass 
        Type myClassType = typeof(TestClass);

        //Return info about type
        TypeInfo myClassTypeInfo = myClassType.GetTypeInfo();
        Console.WriteLine("Class Name: " + myClassTypeInfo.Name);
        Console.WriteLine("Is Public: " + myClassTypeInfo.IsPublic);

        //Return info about members in class
        MemberInfo[] myClassMembers = myClassType.GetMembers();
        Console.WriteLine("\nClass Members:");
        foreach (MemberInfo member in myClassMembers)
        {
            Console.WriteLine(member.Name);
        }

        //Return info about non-public and non-static fields in class
        FieldInfo[] myClassFields = myClassType.GetFields(BindingFlags.NonPublic | BindingFlags.Instance);
        Console.WriteLine("\nClass Fields:");
        foreach (FieldInfo field in myClassFields)
        {
            Console.WriteLine(field.Name);
        }

        //Return info about methods
        MethodInfo methodInfo = myClassType.GetMethod("ShowMethod");
        Console.WriteLine("\nClass Methods:");
        Console.WriteLine($"{methodInfo.Name} type - {methodInfo.ReturnType}");
 
        methodInfo = myClassType.GetMethod("ReturnNumsMethod", BindingFlags.NonPublic | BindingFlags.Instance);
        TestClass myObject = new TestClass();
        object[] argsForReturnMethod = { 22, 8.989 };
        string returnMethodResult = (string)methodInfo.Invoke(myObject, argsForReturnMethod);
        Console.WriteLine("ReturnNumsMethod result: " + returnMethodResult);
    }
}