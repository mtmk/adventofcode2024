// https://adventofcode.com/2024/day/4/input

using System.Text.RegularExpressions;

foreach (var line in File.ReadAllLines("input"))
{
    var strings = Regex.Split(line, @"\s+");

    foreach (var s in strings)
    {
        Console.WriteLine(s);
    }
}