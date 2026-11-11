using System;
using OOP_Lab1_DataAccess;
using OOP_Lab1_DataAccess.Models; // Доданий рядок

namespace OOP_Lab1_Console
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            IPersonRepository repository = new FileRepositoryAdapter();
            ConsoleMenu menu = new ConsoleMenu(repository);

            menu.Start();
            Console.ReadLine();
        }
    }
}