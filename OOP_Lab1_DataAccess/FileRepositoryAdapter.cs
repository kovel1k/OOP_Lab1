using System;
using System.IO;
using System.Text.RegularExpressions;
using OOP_Lab1_DataAccess.Models;

namespace OOP_Lab1_DataAccess
{
    public class FileRepositoryAdapter : IPersonRepository
    {
        public void SaveToFile(string filePath, Person[] persons)
        {
            using (StreamWriter sw = new StreamWriter(filePath, false, System.Text.Encoding.UTF8))
            {
                for (int i = 0; i < persons.Length; i++)
                {
                    string type = persons[i].GetType().Name;
                    sw.WriteLine($"{type} {persons[i].FirstName}{persons[i].LastName}");
                    sw.WriteLine("{");
                    sw.WriteLine($"  \"firstname\": \"{persons[i].FirstName}\",");
                    sw.WriteLine($"  \"lastname\": \"{persons[i].LastName}\"");
                    
                    if (persons[i] is Student student)
                    {
                        sw.WriteLine($",  \"course\": \"{student.Course}\",");
                        sw.WriteLine($"  \"studentId\": \"{student.StudentId}\",");
                        sw.WriteLine($"  \"servedInArmy\": \"{student.ServedInArmy}\",");
                        sw.WriteLine($"  \"militaryId\": \"{student.MilitaryId}\"");
                    }
                    sw.WriteLine("};");
                }
            }
        }

        public Person[] LoadFromFile(string filePath)
        {
            Person[] result = new Person[0];
            if (!File.Exists(filePath)) return result;

            using (StreamReader sr = new StreamReader(filePath, System.Text.Encoding.UTF8))
            {
                string line, currentType = "", fName = "", lName = "", sId = "", mId = "";
                int course = 0;
                bool served = false;

                while ((line = sr.ReadLine()) != null)
                {
                    line = line.Trim();
                    if (line.StartsWith("Student") || line.StartsWith("Lawyer") || line.StartsWith("FootballPlayer"))
                        currentType = line.Split(' ')[0];
                    else if (line.Contains("\"firstname\":")) fName = ExtractValue(line);
                    else if (line.Contains("\"lastname\":")) lName = ExtractValue(line);
                    else if (line.Contains("\"course\":")) int.TryParse(ExtractValue(line), out course);
                    else if (line.Contains("\"studentId\":")) sId = ExtractValue(line);
                    else if (line.Contains("\"servedInArmy\":")) bool.TryParse(ExtractValue(line), out served);
                    else if (line.Contains("\"militaryId\":")) mId = ExtractValue(line);
                    else if (line.StartsWith("};"))
                    {
                        Person p = null;
                        switch (currentType)
                        {
                            case "Student": 
                                p = new Student(fName, lName, course, sId, served, mId); 
                                break;
                            case "Lawyer": 
                                p = new Lawyer(fName, lName); 
                                break;
                            case "FootballPlayer": 
                                p = new FootballPlayer(fName, lName); 
                                break;
                        }

                        if (p != null)
                        {
                            Array.Resize(ref result, result.Length + 1);
                            result[result.Length - 1] = p;
                        }
                    }
                }
            }
            return result;
        }

        private string ExtractValue(string line)
        {
            var match = Regex.Match(line, "\"(.*?)\"");
            if (match.Success && match.NextMatch().Success) return match.NextMatch().Groups[1].Value;
            return "";
        }
    }
}
