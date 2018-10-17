using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/*
 *I Honestly feel bad for you.
 */
namespace Lab8
{
    class Program
    {
        static void SortStuff(ref List<List<string>> student,ref List<List<bool>> checker)
        {//Look at me doing manual list sorts and clearing memory and what not, im a real programmer!
            List<string> tempList = new List<string>();
            List<List<string>> newStudent = new List<List<string>>();
            List<List<bool>> newChecker = new List<List<bool>>();
            for (int i = 0; i < student.Count; i++)
            {
                tempList.Add(student[i][0]);
            }
            tempList.Sort();
            for (int i = 0; i < tempList.Count; i++)
            {
                newChecker.Add(new List<bool>());
                newStudent.Add(new List<string>());
                for (int j = 0; j < tempList.Count; j++)
                {
                    if (tempList[i] == student[j][0])
                        for (int s = 0; s < student[j].Count; s++)
                        {
                            newStudent[i].Add(student[j][s]);
                            if (s == student[j].Count - 1)
                                continue;
                            newChecker[i].Add(checker[j][s]);
                        }
                }
            }
            student.Clear();
            student = newStudent;
            checker.Clear();
            checker = newChecker;
        }

        static void print(List<List<string>> students, List<List<bool>> checker, List<List<string>> catas)
        {//just prints the table
            catas[0].ForEach(v => Console.Write(v.PadRight(17)));
            Console.WriteLine();
            Console.WriteLine("=".PadLeft(catas[0].Count*17,'='));
            for (int i = 0; i < students.Count; i++)
            {//Conditional statements inside the command line to determine printing town and food or empty strings
                Console.Write(((i+1) + ") " +students[i][0]).PadRight(16) + "|");
                for (int j = 0; j < catas[0].Count-1; j++)
                    Console.Write(((checker[i][j] == true) ? students[i][j + 1] : " ").PadRight(16) + "|");
                Console.WriteLine();
                //Console.Write(students[i][0].PadRight(16) + "|" + ((checker[i][0] == true) ? students[i][1] : " ").PadRight(16) + "|"+ ((checker[i][1] == true) ? students[i][2]: " ").PadRight(16) + "|\n");
            }
        }
        
        static int FINDER(List<List<string>> item)//Not perfect. entering Michael or Taylor for the second option triggers 'Town' or 'Food'
        {//based on user input searches for a word or number and returns the index
            string word = Console.ReadLine();
            if (int.TryParse(word,out int num))
            {
                return num;                     //straight up return a number if one is entered
            }
            if (item[0].Contains(word))
                return item[0].IndexOf(word);       //works for catas and not student2
            for (int i = 0; i < item.Count; i++)    //this works for student2
                if (item[i][0] == word)
                    return i+1;
            Console.WriteLine("Invalid Input.");
            return FINDER(item);
        }

        static void addCata(List<List<string>> students, List<List<bool>> checker, List<List<string>> catas)
        {
            string temp = "";
            ask3:
            Console.Write("New catagory: ");
            temp = Console.ReadLine();
            if (string.IsNullOrEmpty(temp))
            {
                Console.WriteLine("NO EMPTIES");
                goto ask3;
            }
            catas[0].Add(temp);               //add new category
            for(int i = 0;i < students.Count; i++)
            {
                students[i].Add("[NULL]");                  //add 'empty' new catagory to student list
                checker[i].Add(false);                      //add new category to checker
            }
            Console.WriteLine("New Category Successfully Added!");

        }

        static void updateStudent(List<List<string>> students, List<List<string>> catas)
        {
            string temp = "";
            int choice1 = 0, choice2 = 0;
            Console.WriteLine("=========================");
            for (int i = 0; i < catas[0].Count; i++)
                Console.WriteLine(i+1+ ") " + catas[0][i]);
            Console.Write("Select Catagory: ");                          //select category->
            choice1 = int.Parse(Console.ReadLine())-1;
            Console.WriteLine("========================");
            for (int i = 0; i < students.Count; i++)
                Console.WriteLine(i+1+") " + students[i][0] + ": ");
            Console.Write("Student #: ");                               //select student->
            choice2 = int.Parse(Console.ReadLine())-1;
            ask2:
            Console.Write("Enter Data: ");                             
            temp = Console.ReadLine();            //Enter new Data
            if (string.IsNullOrEmpty(temp))
            {
                Console.WriteLine("NO EMPTIES");
                goto ask2;
            }
            students[choice2][choice1] = temp;
        }

        static void adding(List<List<string>> students, List<List<bool>> checker, List<List<string>> catas)
        {//Handles add an entire student itself. Also calls updatestudent() and andcata()
            string t = "";
            Console.Write("Add student press 1, update student press 2, catagory press 3:");
            t = Console.ReadLine();
            if (t == "3")
            {
                addCata(students, checker, catas);//to go addCata() and return
                return;
            }
            else if (t == "2")
            {
                updateStudent(students, catas); // go to updateStudent() and return
                return;
            }
            else if (t == "1")
            {
                students.Add(new List<string>());      //add new 'student' at bottom of list
                checker.Add(new List<bool>());
                string temp = "";
                for (int i = 0; i < catas[0].Count; i++)
                {
                ask1:
                    Console.Write(catas[0][i] + ": ");
                    temp = Console.ReadLine();
                    if (string.IsNullOrEmpty(temp))
                    {
                        Console.WriteLine("NO EMPTIES");
                        goto ask1;
                    }
                    students[students.Count - 1].Add(temp);
                    checker[students.Count - 1].Add(false);
                }
            }
        }

        static void Main(string[] args)
        {
            string[,] students = new string[15, 3] {{"Michael", "Canton", "Chicken Wings" },{ "Taylor", "Caro", "Cordon Bleu" },{ "Joshua", "Taylor", "Turkey" },
            { "Lin-Z", "Toledo", "Ice Cream" },{ "Madelyn", "Oxford", "Croissent" },{ "Nana", "Guana", "Empanadas"},{ "Rochelle", "Mars", "Space Cheese"},
            { "Shah", "Newark", "Chicken Wings"},{ "Tim", "Detroit", "Chicken Parm"},{ "Abby", "Traverse City", "Soup"},{ "Blake", "Los Angeles", "Cannolis"},
            { "Bob", "St. Clair Shores", "Pizza"},{ "Jordan", "Warren", "Burgers"},{ "Jay", "Macomb", "Pickles"},{ "Jon", "Huntington Woods", "Ribs"},};
            List<List<string>> students2 = new List<List<string>>();
            List<List<string>> catas = new List<List<string>>();//made this list of a list of string to access the FINDER() function.
            catas.Add(new List<string>());
            catas[0].Add("Name");
            catas[0].Add("Town");
            catas[0].Add("Food");
            List<List<bool>> checker2 = new List<List<bool>>();
            for (int i = 0; i < students.GetLength(0); i++)
            {
                students2.Add(new List<string>());
                checker2.Add(new List<bool>());
                for (int j = 0; j < students.GetLength(1); j++)
                {
                    students2[i].Add(students[i, j]);
                    if (j == students.GetLength(1) - 1) //I wan't there to be 1 less category then student because of the name.
                        continue;
                    checker2[i].Add(false);
                }
            }
            SortStuff(ref students2, ref checker2);

            string temp = "";
            int studentNumber, category = 0;
                while (true)
                {
                try
                {
                    Console.Write("1 to add, any to skip: ");
                    if (Console.ReadLine() == "1")
                    {
                        adding(students2, checker2, catas);
                        SortStuff(ref students2, ref checker2);
                    }
                    print(students2, checker2, catas);
                    Console.Write("Enter student name OR 1-" + students2.Count + ": ");
                    studentNumber = FINDER(students2) - 1;
                    for (int i = 1; i < catas[0].Count; i++)
                        Console.Write(catas[0][i] + " or " + (i) + " | ");
                    category = FINDER(catas) - 1;
                    checker2[studentNumber][category] = true;//tells the index of student to display on the table
                }                                      //also triggers the try/catch for format and index exceptions
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    continue;
                }
                    print(students2, checker2,catas);
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