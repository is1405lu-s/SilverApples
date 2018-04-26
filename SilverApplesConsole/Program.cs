using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace SilverApplesConsole1
{
    public class Program
    {
        static void Main(string[] args)
        {
            int i = 1;
            SilverApplesConsole1.SilverApples.Controller client = new SilverApplesConsole1.SilverApples.Controller();
            do
            {
                Console.WriteLine("Enter name of file \nor \nEnter \"0\" to quit. ");
                string choice = "C:\\Users\\isabe\\Desktop\\" + Console.ReadLine();

                if (choice == "C:\\Users\\isabe\\Desktop\\0")
                {
                    i = 0;
                }
                else if (choice.EndsWith(".txt") || choice.Contains(".doc"))
                {
                    try
                    {
                        string result = client.Search(choice);
                        Console.Write(result);
                    }
                    catch
                    {
                        Console.WriteLine("File could not be found.");
                    }
                }
                else
                {
                    Console.WriteLine("File could not be found");
                }
            } while (i != 0);
        }
    }
}
