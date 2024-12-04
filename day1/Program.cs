// https://adventofcode.com/2024/day/1/input

using System.Text.RegularExpressions;

List<int> list1 = new();
List<int> list2 = new();
int max = 0;

using var reader = new StreamReader("input");
while (reader.ReadLine() is { } line)
{
    max++;
    var strings = Regex.Split(line, @"\s+");
    list1.Add(int.Parse(strings[0]));
    list2.Add(int.Parse(strings[1]));
}

list1.Sort();
list2.Sort();

int result1 = 0;

// for (int i = 0; i < max; i++)
// {
//     result += Math.Abs(list1[i] - list2[i]);
// }
//
// foreach (var (left, right) in list1.Zip(list2))
// {
//     result += Math.Abs(left - right);
// }

result1 = list1.Zip(list2).Sum(x => Math.Abs(x.First - x.Second));

Console.WriteLine(result1);

int result2 = 0;
// foreach (var i in list1)
// {
//     similar += i * list2.Count(x => x == i);
// }

result2 = list1.Sum(x => x * list2.Count(y => y == x));

Console.WriteLine(result2);