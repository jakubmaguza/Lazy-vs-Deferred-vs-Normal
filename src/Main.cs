using System;
using System.Collections.Generic;
using System.Threading;

namespace ConsoleApplication3
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Starting Application");
            PrepareString p = new PrepareString();
            Console.WriteLine("Object initialized");
            var normalList = p.NormalList;
            Console.WriteLine("p.LazyList.IsValueCreated " + p.LazyList.IsValueCreated);
            var lazyList = p.LazyList.Value;
            Console.WriteLine("p.LazyList.IsValueCreated " + p.LazyList.IsValueCreated);
            var deferredList = p.DeferredList;
            int check = 0;
            int upTo = 3;
            Console.WriteLine("NORMAL LIST ENUMERATING");
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            foreach (var s in normalList)
            {
                Console.WriteLine(s);
                check++;
                if (check == 3)
                    break;
            }
            Console.ResetColor();
            check = 0;
            Console.WriteLine("LAZY LIST ENUMERATING");
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            foreach (var s in lazyList)
            {
                Console.WriteLine(s);
                check++;
                if (check == 3)
                    break;
            }
            Console.ResetColor();
            check = 0;
            Console.WriteLine("DEFERRED LIST ENUMERATING");
            
            foreach (var s in deferredList)
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine(s);
                check++;
                if (check == 3)
                    break;
            }
            Console.ReadLine();
        }
    }
	
    public class PrepareString
    {
        public IEnumerable<string> NormalList = CreateList("NormalList", ConsoleColor.DarkGreen);
        public Lazy<IEnumerable<string>> LazyList = new Lazy<IEnumerable<string>>(() => CreateList("LazyList", ConsoleColor.DarkYellow));
        public IEnumerable<string> DeferredList = CreateDeferredList("DeferredList", ConsoleColor.Blue);
        private static IEnumerable<string> CreateList(string type, ConsoleColor c)
        {
            List<string> _l = new List<string>();
            Console.ForegroundColor = c;
            Console.WriteLine($"Starting {type}");
            for (int i = 0; i < 5; i++)
            {
                _l.Add(ComputeString(i, type, c));
            }
            return _l;
        }
        public static IEnumerable<string> CreateDeferredList(string type, ConsoleColor c)
        {
            Console.ForegroundColor = c;
            Console.WriteLine("Starting DefferedList");
            for (int i = 0; i < 5; i++)
            {
                yield return ComputeString(i, type, c);
            }
        }
        private static string ComputeString(int iteration, string type, ConsoleColor c)
        {
            Thread.Sleep(100);
            Console.ForegroundColor = c;
            Console.WriteLine($"Producing {iteration} for {type}");
            Console.ResetColor();
            return $"Return {iteration}";
        }
    }
}
