// https://adventofcode.com/2024/day/2/input

var result1 = 0;
var result2 = 0;

foreach(var line in File.ReadAllLines("input"))
{
    var ints = line.Split(' ').Select(int.Parse).ToList();
    
    if (IsSafe(ints))
    {
        result1++;
        result2++;
        continue;
    }

    for (var index = 0; index < ints.Count; index++)
    {
        if (IsSafe(ints, index))
        {
            result2++;
            break;
        }
    }
}

Console.WriteLine(result1);
Console.WriteLine(result2);

bool IsSafe(List<int> ints, int? skipIndex = null)
{
    bool ascending = true;
    int count = 0;
    int previous = 0;
    for (var index = 0; index < ints.Count; index++)
    {
        if (skipIndex == index)
            continue;
        
        var current = ints[index];
        
        if (++count > 1)
        {
            if (count == 2)
                ascending = previous < current;

            if (ascending ? IsUnsafePair(previous, current) : IsUnsafePair(current, previous))
                return false;
        }

        previous = current;
    }

    return true;
}

bool IsUnsafePair(int previous, int current) => previous >= current || current - previous > 3;