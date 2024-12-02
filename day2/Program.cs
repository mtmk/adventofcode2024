using System.Text.RegularExpressions;

var r1 = 0;
var r2 = 0;

using var reader = new StreamReader("input");
while (reader.ReadLine() is { } line)
{
    var strings = Regex.Split(line, @"\s+");
    var ints = strings.Select(int.Parse).ToArray();
    
    if (IsSafe(ints))
    {
        r1++;
        r2++;
    }
    else
    {
        for (var index = 0; index < ints.Length; index++)
        {
            if (IsSafe(ints, index))
            {
                r2++;
                break;
            }
        }
    }
}

Console.WriteLine(r1);
Console.WriteLine(r2);

bool IsSafe(IEnumerable<int> ints, int? skipIndex = null)
{
    bool first = true;
    bool second = true;
    bool asc = true;
    int p = 0;
    int index = 0;
    foreach (var i in ints)
    {
        if (index++ == skipIndex)
            continue;
        
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