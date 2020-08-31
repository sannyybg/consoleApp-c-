using System;
using System.Collections.Generic;

namespace ConsoleApplication
{
    internal class Program
    {
        private const int searchValue = 5;

        public static void Main(string[] args)
        {
            for (var i = 0; i < 3; i++)
            {
                Console.WriteLine("\n");

                string input = Console.ReadLine();
                Console.WriteLine($"Input String : {input} ");

                var output = string.Empty;
                try
                {
                    var result = StringToNumberArray(input).ThisDoesntMakeAnySense(filterPredicate, newDelegate);
                    output = result.ToString();

                }
                catch (Exception e)
                {
                    output = e.Message;
                }

                finally
                {
                    Console.WriteLine($"Output : {output} ");
                }
            }

            Console.ReadKey();
        }


        private static bool filterPredicate<T>(T number) where T : IComparable
        {
            
            return number.CompareTo(searchValue) == 0;
        }

        private static int newDelegate()
        {
            Console.WriteLine($"Predicate could not find number {searchValue} , creating new record");
            return searchValue;
        }



        private static int[] StringToNumberArray(string numbersString)
        {
            
            if (string.IsNullOrEmpty(numbersString)) return default;

            var splitString = numbersString.Split(',');

            
            var resultArray = new int[splitString.Length];


            for (var i = 0; i < splitString.Length; i++)
            {
                
                if (int.TryParse(splitString[i], out var parsed))
                {
                    resultArray[i] = parsed;
                }
            }

            return resultArray;

        }
    }


    public static class MyExtensionMethods
    {
        public static T ThisDoesntMakeAnySense<T>(this IEnumerable<T> myArray, Func<T, bool> filterPredicate, Func<T> newDelegate) where T : IComparable
        {

            if (myArray == null) throw new ArgumentException("Massive must not be null");
            if (filterPredicate == null) throw new ArgumentException("Filter predicatate must not be null");
            if (newDelegate == null) throw new ArgumentException("New Record Creator Function must not be null");

            
            foreach (var number in myArray)
            {
                if (filterPredicate(number))
                {
                    Console.WriteLine($"Predicate Found Number : {number} ... Returning Default Value ");
                    return default;
                }
            }

            
            return newDelegate();
        }
    }
}
