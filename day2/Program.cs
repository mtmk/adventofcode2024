using System.Text.RegularExpressions;

List<string> list1 = new();
int max = 0;

using var reader = new StreamReader("input");
while (reader.ReadLine() is { } line)
{
    max++;
    var strings = Regex.Split(line, @"\s+");
    list1.Add(strings[0]);
}

foreach (var s in list1)
{
    Console.WriteLine(s);
}

for (int i = 0; i < max; i++)
{
    Console.WriteLine(list1[i]);
}
