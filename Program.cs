/*Advanced Expense Tracker for these who don't sleep.
 * create tables, add columns, view .(all)/{table}, count {table}, exit.
 */


using System;
using System.Collections.Generic;

namespace ExpensesTracker
{
    class Program
    {

        static void Main(string[] args)
        {
            CallFunc();
        }
        

        static private int CallFunc()
        {
            Dictionary<string, double> expenseList = new Dictionary<string, double>();

            Console.WriteLine("Start writting a command to call a function");
            string RawInput = Console.ReadLine();
            List<string> Input = SliceInput(RawInput);

            switch (Input[0])
            {
                case "add":
                    expenseList = ExpenseTracker.GetExpenseList(Input[1]);
                    ExpenseTracker.AddPair(expenseList);
                    break;

                case "count":
                    expenseList = ExpenseTracker.GetExpenseList(Input[1]);
                    Console.WriteLine(ExpenseTracker.CountExpenses(expenseList));
                    break;

                case "create":
                    ExpenseTracker.CreateExpenseList(Input[1]);
                    break;

                case "view":
                    ExpenseTracker.View(Input[1]);
                    break;

                case "exit":
                    return 0;
            }

            CallFunc();
            return 0;
        }


        static private List<string> SliceInput(string Input)
        {
            if (Input.Contains(" "))
            {
                List<string> slices = new List<string>();
                string[] inputSplited = Input.Split(" ");
                string command = inputSplited[0];
                string argument = inputSplited[1];
                slices.Add(command);
                slices.Add(argument);

                return slices;
            }
            else
            {
                return new List<string>() { Input, "" };
            }
        }


        static class ExpenseTracker
        {
            static private List<Dictionary<string, Dictionary<string, double>>> expenseList = new List<Dictionary<string, Dictionary<string, double>>>();


            //to be used to add an expense
            static public void AddPair(Dictionary<string, double> Expenses)
            {
                string Name;
                double Cost;

                Console.WriteLine("What did you purchase?");
                Name = Console.ReadLine();

                Console.WriteLine("How much did you spend?");
                Cost = double.Parse(Console.ReadLine());

                Expenses.Add(Name, Cost);

                int i = 1;
                foreach(KeyValuePair<string, double> data in Expenses)
                {
                    Console.WriteLine($"{i}. {data.Key}.....{data.Value}$");
                    i++;
                }
            }


            static public double CountExpenses(Dictionary<string, double> Expenses)
            {
                Dictionary<string, double>.ValueCollection Costs = Expenses.Values;
                double Sum = 0;

                foreach (double value in Costs)
                {
                    Sum += value;
                }

                return Sum;
            }


            static public void CreateExpenseList(string name)
            {
                Dictionary<string, double> emptyValue = new Dictionary<string, double>();
                Dictionary<string, Dictionary<string, double>> list = new Dictionary<string, Dictionary<string, double>>(); //list is a list of expenses<Dictionary<T>>
                list.Add(name, emptyValue);
                expenseList.Add(list);
            }

            
            static public Dictionary<string, double> GetExpenseList(string name)
            {
                for (int i=0; i < expenseList.Count; i++)
                {
                    List<string> keyList = new List<string>(expenseList[i].Keys);
                    string key = keyList[0];

                    if (name == key)
                    {
                        return expenseList[i][key];
                    }
                    else
                    {
                        continue;
                    }
                }
                return null;
            }


            static public void View(string table) //table is the name of adictionary of expenses
            {
                if (table == ".")
                {
                    foreach (Dictionary<string, Dictionary<string, double>> i in expenseList)
                    {
                        string name = new List<string>(i.Keys)[0];
                        Console.WriteLine(name + ":");

                        int num = 1;
                        foreach (KeyValuePair<string, double> pair in i[name])
                        {
                            Console.WriteLine($"\t{num}. {pair.Key}.....{pair.Value}$");
                            num++;
                        }
                    }
                }
                else
                {
                    Dictionary<string, double> expense = GetExpenseList(table);

                    Console.WriteLine(table + ":");

                    int num = 1;
                    foreach (KeyValuePair<string, double> pair in expense)
                    {
                        Console.WriteLine($"\t{num}. {pair.Key}.....{pair.Value}$");
                        num++;
                    }
                }
            }
        }
    }
}
