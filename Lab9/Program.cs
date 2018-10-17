﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/*
 *Removed 'available()' function because the 'checker' list is enough  asdf
 */
namespace Lab8
{
    class Program
    {
        static void print(List<string> students, List<List<bool>> checker,string temp, List<string> food, List<string> town)
        {//just prints the table
            Console.WriteLine("Name:".PadRight(17) + "Town:".PadRight(17) + "Food:".PadRight(17));
            Console.WriteLine("=".PadLeft(51,'='));
            for (int i = 0; i < students.Count; i++)
            {//Conditional statements inside the command line to determine printing town and food or empty strings
                Console.Write(students[i].PadRight(16) + "|" + ((checker[i][0] == true) ? town[i] : " ").PadRight(16) + "|"+ ((checker[i][1] == true) ? food[i] : " ").PadRight(16) + "|\n");
            }
        }
        
        static int FINDER(List<string> item)//Not perfect. entering Michael or Taylor for the second option triggers 'Town' or 'Food'
        {//based on user input searches for a word or number and returns the index
            string word = Console.ReadLine();
            if (int.TryParse(word,out int num))
            {
                return num-1;
            }
            if (item.Contains(word))
                return item.IndexOf(word);
            Console.WriteLine("Invalid Input.");
            return FINDER(item);
        }

        static void Main(string[] args)
        {
            string[,] students = new string[15, 3] {{"Michael", "Canton", "Chicken Wings" },{ "Taylor", "Caro", "Cordon Bleu" },{ "Joshua", "Taylor", "Turkey" },
            { "Lin-Z", "Toledo", "Ice Cream" },{ "Madelyn", "Oxford", "Croissent" },{ "Nana", "Guana", "Empanadas"},{ "Rochelle", "Mars", "Space Cheese"},
            { "Shah", "Newark", "Chicken Wings"},{ "Tim", "Detroit", "Chicken Parm"},{ "Abby", "Traverse City", "Soup"},{ "Blake", "Los Angeles", "Cannolis"},
            { "Bob", "St. Clair Shores", "Pizza"},{ "Jordan", "Warren", "Burgers"},{ "Jay", "Macomb", "Pickles"},{ "Jon", "Huntington Woods", "Ribs"},};
            List<string> students2 = new List<string>(),town = new List<string>(), food = new List<string>();
            List<List<bool>> checker2 = new List<List<bool>>();
            for (int i = 0; i < students.GetLength(0); i++)
            {
                checker2.Add(new List<bool>());
                checker2[i].Add(false);
                checker2[i].Add(false);
                students2.Add(students[i, 0]);
                town.Add(students[i, 1]);
                food.Add(students[i, 2]);
            }
            for (int i = 0; i < students2.Count; i++)
            {
                Console.WriteLine(string.Join(" ", students2[i]));
            }

            string temp = "";
            int studentNumber, category = 0;
            while (true)
            {
                try
                {
                    print(students2, checker2, temp, food, town);
                    Console.Write("Enter student name OR 1-15: ");
                    studentNumber = FINDER(students2);
                    Console.Write("Town or 1 | Food or 2: ");
                    string word = Console.ReadLine(); ;
                    if (word == "Town" || word == "1")
                        category = 0;
                    else if (word == "Food" || word == "2")
                        category = 1;
                    checker2[studentNumber][category] = true;//tells the index of student to display on the table
                }                                           //also triggers the try/catch for format and index exceptions
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    continue;
                }
                print(students2, checker2, temp,food, town);
                ask2:
                Console.Write("Again(y/n): ");
                temp = Console.ReadLine().ToLower();
                if (temp == "y")
                    continue;
                else if (temp == "n")
                    break;
                else
                    goto ask2;
            }
        }
    }
}