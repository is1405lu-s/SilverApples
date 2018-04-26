using SilverApples.SilverApples;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SilverApplesWebServiceConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            SilverApples.SilverApples.Controller client = new SilverApples.SilverApples.Controller();
            int i = 0;
            do
            {
                Console.WriteLine("Enter: \n1 to view all the customers\n2 to view a certain customer" +
                "\n0 to quit");
                string choice = Console.ReadLine();

                if (choice.Equals("0"))
                {
                    i++;
                }
                else if (choice.Equals("1"))
                {
                    string[][] custArray = client.GetCustomers();
                    for (int k = 0; k < custArray.Length; k++)
                        foreach (string s in custArray[k])
                        {
                            Console.WriteLine(s);
                            //("\nCustomer Pnr: " + c.CPnr + "\nName: " + c.CName + "\nAddress: " + c.CAddress + "\nPhone Nr.: "
                            //+ c.CPhone + "\nMail: " + c.CMail + "\n");
                        }
                }
                else if (choice.Equals("2"))
                {
                    Console.Write("Enter pnr;");
                    string pnr = Console.ReadLine();
                    string[][] custArray = client.GetCustomers();
                    bool found = false;
                    for (int k = 0; k < custArray.Length; k++)
                    {
                        foreach (string d in custArray[k])
                        {
                            if (pnr.Equals(d.Substring(0, 2)))
                            {
                                found = true;
                                Console.WriteLine(d);
                                //Console.WriteLine("\nCustomer Pnr: " + c.CPnr + "\nName: " + c.CName + "\nAddress: " + c.CAddress + "\nPhone Nr.: "
                                //+ c.CPhone + "\nMail: " + c.CMail + "\n");
                            }
                        }
                    }
                    if (found == false)
                    {
                        Console.WriteLine("Wrong input, try again.");
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
