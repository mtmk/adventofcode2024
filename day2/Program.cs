using System.Text.RegularExpressions;

List<int[]> list = new();
int max = 0;

using var reader = new StreamReader("input");
while (reader.ReadLine() is { } line)
{
    max++;
    var strings = Regex.Split(line, @"\s+");
    list.Add(strings.Select(int.Parse).ToArray());
}

var r1 = 0;
var r2 = 0;
foreach (var s in list)
{
    if (IsSafe(s))
    {
        r1++;
        r2++;
    }
    else
    {
        for (var index = 0; index < s.Length; index++)
        {
            var tmp = s.ToList();
            tmp.RemoveAt(index);
            if (IsSafe(tmp.ToArray()))
            {
                r2++;
                break;
            }
        }
    }
}

Console.WriteLine(r1);
Console.WriteLine(r2);

bool IsSafe(int[] ints)
{
    bool first = true;
    bool second = true;
    bool asc = true;
    int p = 0;
    foreach (var i in ints)
    {
        if (first)
        {
            first = false;
        }
        else
        {
            if (second)
            {
                second = false;
                asc = p < i;
            }

            if (asc)
            {
                if (IsUnsafePair(p, i))
                    return false;
            }
            else
            {
                if (IsUnsafePair(i, p))
                    return false;
            }
        }

        p = i;
    }

    return true;
}

bool IsUnsafePair(int previous, int current)
    => previous >= current || current - previous > 3;