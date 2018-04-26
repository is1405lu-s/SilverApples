using System;

namespace SilverApplesConsole2
{
    public class Program
    {
        static void Main(string[] args)
        {
            SilverApplesWebServiceConsole2.SilverApples.Controller client = new SilverApplesWebServiceConsole2.SilverApples.Controller();
            int i = 0;
            do
            {
                Console.WriteLine("Enter 1 to view all the customers \nor \nEnter 2 to view a specific customer" +
                " \nor \nEnter 0 to quit");
                string choice = Console.ReadLine();

                if (choice.Equals("0"))
                {
                    i++;
                }
                else if (choice.Equals("1"))
                {
                    string[][] custArray = client.GetCustomers();
                    for (int k = 0; k < custArray.Length; k++)
                    {
                        foreach (string s in custArray[k])
                        {
                            Console.Write(s + " ");
                        }
                        Console.WriteLine("\n");
                    }
                }
                else if (choice.Equals("2"))
                {
                    Console.WriteLine("Enter Personal Nr: ");
                    string cPnr = Console.ReadLine();
                    string[][] custArray = client.GetCustomers();
                    bool found = false;
                    for (int k = 0; k < custArray.Length; k++)
                    {
                        if (cPnr.Equals(custArray[k][0]))
                        {
                            foreach (string d in custArray[k])
                            {
                                found = true;
                                Console.Write(d + " ");
                            }
                            Console.WriteLine("\n");
                        }
                    }
                    if (found == false)
                    {
                        Console.WriteLine("Wrong Personal Nr. entered, try again.");
                    }
                }
                else
                {
                    Console.WriteLine("Wrong input, try again.");
                }
            } while (i != 1);
        }
    }
}
