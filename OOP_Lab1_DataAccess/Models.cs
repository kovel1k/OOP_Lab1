using System;
using System.Text.RegularExpressions;

namespace OOP_Lab1_DataAccess.Models
{
    public interface IReciter
    {
        void RecitePoems();
    }

    public interface IPersonRepository
    {
        void SaveToFile(string filePath, Person[] persons);
        Person[] LoadFromFile(string filePath);
    }

    public abstract class Person
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        protected Person(string firstName, string lastName)
        {
            if (!Regex.IsMatch(firstName, @"^[A-ZА-ЯІЇЄ][a-zа-яіїє]+$") || 
                !Regex.IsMatch(lastName, @"^[A-ZА-ЯІЇЄ][a-zа-яіїє]+$"))
                throw new ArgumentException("Ім'я та прізвище повинні містити лише літери та починатися з великої.");

            FirstName = firstName;
            LastName = lastName;
        }

        public abstract void PerformSpecificAction();
    }

    public class Student : Person, IReciter
    {
        public int Course { get; set; }
        public string StudentId { get; set; }
        public bool ServedInArmy { get; set; }
        public string MilitaryId { get; set; }

        public Student(string firstName, string lastName, int course, string studentId, bool servedInArmy, string militaryId = "") 
            : base(firstName, lastName)
        {
            if (course < 1 || course > 6)
                throw new ArgumentException("Курс повинен бути від 1 до 6.");
            if (!Regex.IsMatch(studentId, @"^[A-ZА-ЯІЇЄ]{2}\d{6}$"))
                throw new ArgumentException("Невірний формат студентського квитка.");
            
            Course = course;
            StudentId = studentId;
            ServedInArmy = servedInArmy;
            MilitaryId = militaryId;
        }

        public override void PerformSpecificAction() => Console.WriteLine($"{FirstName} {LastName} навчається в університеті.");
        public void RecitePoems() => Console.WriteLine($"{FirstName} {LastName} виразно декламує вірші на сцені.");
    }

    public class Lawyer : Person, IReciter
    {
        public Lawyer(string firstName, string lastName) : base(firstName, lastName) { }
        public override void PerformSpecificAction() => Console.WriteLine($"{FirstName} {LastName} захищає клієнта в суді.");
        public void RecitePoems() => Console.WriteLine($"{FirstName} {LastName} декламує вірші для ораторських навичок.");
    }

    public class FootballPlayer : Person
    {
        public FootballPlayer(string firstName, string lastName) : base(firstName, lastName) { }
        public override void PerformSpecificAction() => Console.WriteLine($"{FirstName} {LastName} грає у футбол.");
    }
}
