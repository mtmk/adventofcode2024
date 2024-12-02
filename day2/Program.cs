var r1 = 0;
var r2 = 0;

using var reader = new StreamReader("input");
while (reader.ReadLine() is { } line)
{
    var ints = line.Split(' ').Select(int.Parse).ToList();
    
    if (IsSafe(ints))
    {
        r1++;
        r2++;
    }
    else
    {
        for (var index = 0; index < ints.Count; index++)
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

bool IsSafe(List<int> ints, int? skipIndex = null)
{
    bool first = true;
    bool second = true;
    bool ascending = true;
    int previous = 0;
    for (var index = 0; index < ints.Count; index++)
    {
        if (skipIndex == index)
            continue;
        
        var current = ints[index];
        
        if (first)
        {
            first = false;
        }
        else
        {
            if (second)
            {
                second = false;
                ascending = previous < current;
            }

            if (ascending)
            {
                if (IsUnsafePair(previous, current))
                    return false;
            }
            else
            {
                if (IsUnsafePair(current, previous))
                    return false;
            }
        }

        previous = current;
    }

    return true;
}

bool IsUnsafePair(int previous, int current)
    => previous >= current || current - previous > 3;