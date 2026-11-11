using System;
using OOP_Lab1_DataAccess;
using OOP_Lab1_DataAccess.Models;

namespace OOP_Lab1_Console
{
    public class ConsoleMenu
    {
        private readonly IPersonRepository _repository;
        private readonly string _filePath = "database.txt";

        public ConsoleMenu(IPersonRepository repository)
        {
            _repository = repository;
        }

        public void Start()
        {
            Person[] database = new Person[]
            {
                new Student("Олександр", "Іванов", 5, "КВ123456", true, "МК654321"),
                new Student("Марія", "Коваленко", 1, "КВ987654", false),
                new Student("Дмитро", "Максименко", 5, "КВ111111", true, "МК222222"),
                new Lawyer("Олексій", "Петров"),
                new FootballPlayer("Андрій", "Шевченко")
            };

            _repository.SaveToFile(_filePath, database);
            Console.WriteLine("Дані успішно збережено у файл (до специфічного формату).\n");

            Person[] loadedData = _repository.LoadFromFile(_filePath);
            
            ProcessVariantTask(loadedData);
            DemonstrateAbstractions(loadedData);
        }

        private void ProcessVariantTask(Person[] persons)
        {
            int targetCount = 0;
            Console.WriteLine("--- ЗАВДАННЯ ВАРІАНТУ 4 ---");
            Console.WriteLine("Студенти 5-го курсу, які служили в армії:");
            
            for (int i = 0; i < persons.Length; i++)
            {
                if (persons[i] is Student student && student.Course == 5 && student.ServedInArmy)
                {
                    targetCount++;
                    Console.WriteLine($"- {student.FirstName} {student.LastName} (Військовий квиток: {student.MilitaryId})");
                }
            }
            Console.WriteLine($"Загальна кількість: {targetCount}\n");
        }

        private void DemonstrateAbstractions(Person[] persons)
        {
            Console.WriteLine("--- ДЕМОНСТРАЦІЯ ВМІНЬ ТА ПОЛІМОРФІЗМУ ---");
            for (int i = 0; i < persons.Length; i++)
            {
                persons[i].PerformSpecificAction();
                if (persons[i] is IReciter reciter)
                {
                    reciter.RecitePoems();
                }
                Console.WriteLine();
            }
        }
    }
}
