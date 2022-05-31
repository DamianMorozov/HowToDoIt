// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using MoreLinq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace MoreLinqDemo
{
    internal class Demo
    {
        internal void BatchDemo()
        {
            Stopwatch sw = Stopwatch.StartNew();
            Console.WriteLine();
            Console.WriteLine($"{nameof(BatchDemo)}");

            IEnumerable<int> numbers = Enumerable.Range(1, 100);
            foreach (IEnumerable<int> batch in numbers.Batch(10))
            {
                Console.WriteLine();
                Console.Write($"{batch.Count()} items:\t");
                batch.ForEach(x => Console.Write($"{x}\t"));
            }
            
            sw.Stop();
            Console.WriteLine();
            Console.WriteLine($"Elapse: {sw.Elapsed}");
        }
        
        internal void InterleavedDemo()
        {
            Stopwatch sw = Stopwatch.StartNew();
            Console.WriteLine();
            Console.WriteLine($"{nameof(InterleavedDemo)}");

            Random random = new();
            IEnumerable<double> wholeNumbers = Enumerable.Range(1, 10).Select(_ => (double)random.Next(10));
            Console.WriteLine($"{nameof(wholeNumbers)}");
            wholeNumbers.ForEach(x => Console.Write($"{x}\t")); ;

            IEnumerable<double> fractNumbers = Enumerable.Range(1, 10).Select(_ => random.NextDouble());
            Console.WriteLine();
            Console.WriteLine($"{nameof(fractNumbers)}");
            fractNumbers.ForEach(x => Console.Write($"{x}\t")); ;

            Console.WriteLine();
            Console.WriteLine($"Interleave");
            foreach (double item in wholeNumbers.Interleave(fractNumbers))
            {
                Console.Write($"{item}\t");
            }

            sw.Stop();
            Console.WriteLine();
            Console.WriteLine($"Elapse: {sw.Elapsed}");
        }
    }
}
