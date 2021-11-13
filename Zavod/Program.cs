using System;
using System.Collections.Generic;
using System.IO;

namespace Zavod
{
    class Employee
    {
        public string FullName { get; private set; }
        public string NumberZ { get; private set; }
        public int Money { get; private set; }
        public bool IsDirector { get; private set; }
        public Employee(string name, string z, int money, bool isDirector)
        {
            FullName = name;
            NumberZ = z;
            Money = money;
            IsDirector = isDirector;
        }
        public Employee(string name, string z, int money)
        {
            FullName = name;
            NumberZ = z;
            Money = money;
        }
        public Employee()
        {

        }
        public List<Employee> list;
        public Employee CreateEmployee(string[] str, List<Employee> list)
        {
            if (str.Length == 4)
            {
                var worker = new Employee(str[0], str[1], int.Parse(str[2]), true);
                list.Add(worker);
                return worker;
            }
            else
            {
                return new Employee(str[0], str[1], int.Parse(str[2]));
            }
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            string path = Environment.CurrentDirectory + "\\workers.txt";
            string[] file = File.ReadAllLines(path);
            var list = CreateList(file);
            Console.WriteLine(CountMoney(list));
            Console.WriteLine(SearchBigDirector(list));
        }
        static List<Employee> CreateList(string[] file)
        {
            Employee employee = new Employee();
            var list = new List<Employee>();
            var listForD = new List<Employee>();
            for (int i = 0; i < file.Length; i++)
            {
                string[] str = file[i].Split(';');
                employee = employee.CreateEmployee(str, listForD);
                list.Add(employee);
            }
            CorrectFile(listForD);
            return list;
        }
        static int CountMoney(List<Employee> employees)
        {
            int allMoney = 0;
            foreach (var employee in employees)
            {
                if (!employee.IsDirector)
                    allMoney += employee.Money;
            }
            return allMoney;
        }
        static string SearchBigDirector(List<Employee> list)
        {
            int max = 0;
            int index = 0;
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].IsDirector && list[i].Money > max)
                {
                    max = list[i].Money;
                    index = i;
                }
            }
            return list[index].NumberZ;
        }
        static void CorrectFile(List<Employee> list)
        {
            int z1 = 0;
            int z2 = 0;
            int z3 = 0;
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].NumberZ == "Цех 1")
                {
                    z1++;
                    if (z3 == 0 || z3 == 3)
                    {
                        throw new Exception("У цеха 1 больше директоров!");
                    }
                }
                else if (list[i].NumberZ == "Цех 2")
                {
                    z2++;
                    if (z3 == 0 || z3 == 3)
                    {
                        throw new Exception("У цеха 2 больше директоров!");
                    }
                }
                else if (list[i].NumberZ == "Цех 3")
                {
                    z3++;
                    if (z3 == 0 || z3 == 3)
                    {
                        throw new Exception("У цеха 3 больше директоров!");
                    }
                }
            }
        }
    }
}
