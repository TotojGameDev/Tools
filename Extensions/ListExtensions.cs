using System;
using System.Collections.Generic;
using System.Linq;

namespace TotojGameDev
{
    public static class ListExtensions
    {
        public static void ForEach<T>(this List<T> input, Action<T, int> action)
        {
            for (int i = 0; i < input.Count; i++)
            {
                action(input[i], i);
            }
        }

        /**
     * Get and remove from list the first n elements
     */
        public static List<T> Peek<T>(this List<T> input, int numberOfElement)
        {
            List<T> elements = input.Take(1).ToList();
            input.RemoveAll(it => elements.Contains(it));
            return elements;
        }

        public static bool IsEmpty<T>(this List<T> input)
        {
            return input.Count == 0;
        }
    }
}