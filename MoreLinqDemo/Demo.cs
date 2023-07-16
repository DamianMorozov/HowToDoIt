// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using MoreLinq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace MoreLinqDemo;

internal class Demo
{
	internal void BatchDemo()
	{
		Stopwatch sw = Stopwatch.StartNew();
		Console.WriteLine();
		Console.WriteLine($"{nameof(BatchDemo)}");

		IEnumerable<int> numbers = Enumerable.Range(1, 50);
		foreach (IEnumerable<int> batch in numbers.Batch(10))
		{
			Console.WriteLine();
			Console.Write($"{batch.Count()} items:\t");
			batch.ForEach(x => Console.Write($"{x}\t"));
		}
            
		sw.Stop();
		Console.WriteLine();
		Console.WriteLine($"{nameof(sw.Elapsed)}: {sw.Elapsed}");
	}

	internal void InterleavedDemo()
	{
		Stopwatch sw = Stopwatch.StartNew();
		Console.WriteLine();
		Console.WriteLine($"{nameof(InterleavedDemo)}");

		IEnumerable<int> numbers1 = Enumerable.Range(1, 50);
		IEnumerable<int> numbers2 = Enumerable.Range(51, 100);
		foreach (int item in numbers1.Interleave(numbers2))
		{
			Console.Write($"{item}\t");
		}

		sw.Stop();
		Console.WriteLine();
		Console.WriteLine($"{nameof(sw.Elapsed)}: {sw.Elapsed}");
	}
        
	internal void PermDemo()
	{
		Stopwatch sw = Stopwatch.StartNew();
		Console.WriteLine();
		Console.WriteLine($"{nameof(PermDemo)}");

		char[] letters = "word1".ToCharArray();
		foreach (IList<char>? item in letters.Permutations())
		{
			Console.WriteLine($"{new string(item.ToArray())}\t");
		}

		sw.Stop();
		Console.WriteLine();
		Console.WriteLine($"{nameof(sw.Elapsed)}: {sw.Elapsed}");
	}

	internal void SplitDemo()
	{
		Stopwatch sw = Stopwatch.StartNew();
		Console.WriteLine();
		Console.WriteLine($"{nameof(SplitDemo)}");

		IEnumerable<int> numbers = Enumerable.Range(1, 20);
		Console.WriteLine($"{numbers.Count()} items input: {string.Join("\t", numbers)}");
		IEnumerable<IEnumerable<int>>? split = numbers.Split(5);
		foreach (IEnumerable<int>? item in split)
		{
			Console.WriteLine($"{item.Count()} items output: {string.Join("\t", item)}");
		}

		sw.Stop();
		Console.WriteLine();
		Console.WriteLine($"{nameof(sw.Elapsed)}: {sw.Elapsed}");
	}
}