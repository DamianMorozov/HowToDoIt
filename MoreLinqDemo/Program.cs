// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

// https://morelinq.github.io/

using MoreLinqDemo;

Console.WriteLine("MoreLinq Demo");
Console.WriteLine();

Demo demo = new();
demo.BatchDemo();
demo.InterleavedDemo();

Console.WriteLine();
Console.WriteLine("Press any key to exit.");
Console.ReadKey();
