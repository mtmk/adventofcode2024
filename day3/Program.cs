// https://adventofcode.com/2024/day/3/input

using System.Text.RegularExpressions;

{
    // Regex way
    {
        var result1 = 0;
        var result2 = 0;
        var enabled = true;
        foreach (var line in File.ReadAllLines("input"))
        {
            foreach (Match match in Regex.Matches(line, @"mul\((\d+),(\d+)\)|do\(\)|don't\(\)"))
            {
                if (match.Groups[0].Value.StartsWith("mul"))
                {
                    var i1 = int.Parse(match.Groups[1].Value);
                    var i2 = int.Parse(match.Groups[2].Value);
                    result1 += i1 * i2;
                    if (enabled) result2 += i1 * i2;
                }
                else if (match.Groups[0].Value == "do()")
                {
                    enabled = true;
                }
                else if (match.Groups[0].Value == "don't()")
                {
                    enabled = false;
                }
            }
        }

        Console.WriteLine(result1);
        Console.WriteLine(result2);
    }

    // State machine way
    {
        var result1 = 0;
        var result2 = 0;

        using var sr = File.OpenText("input");
        int s = 0;
        int i1 = 0;
        int i2 = 0;
        int e = 1;
        while (true)
        {
            var read = sr.Read();
            if (read == -1) break;
            var c = (char)read;
            
            if (s == 0 && c == 'm') s = 1;
            else if (s == 1 && c == 'u') s = 2;
            else if (s == 2 && c == 'l') s = 3;
            else if (s == 3 && c == '(') s = 4;
            else if (s == 4 && c >= '0' && c <= '9') i1 = i1 * 10 + (c - '0');
            else if (s == 4 && c == ',') s = 5;
            else if (s == 5 && c >= '0' && c <= '9') i2 = i2 * 10 + (c - '0');
            else if (s == 5 && c == ')')
            {
                result1 += i1 * i2;
                result2 += i1 * i2 * e;
                s = i1 = i2 = 0;
            }
            else if (s == 0 && c == 'd') s = 6;
            else if (s == 6 && c == 'o') s = 7;
            else if (s == 7 && c == '(') s = 8;
            else if (s == 8 && c == ')')
            {
                s = 0;
                e = 1;
            }
            else if (s == 7 && c == 'n') s = 9;
            else if (s == 9 && c == '\'') s = 10;
            else if (s == 10 && c == 't') s = 11;
            else if (s == 11 && c == '(') s = 12;
            else if (s == 12 && c == ')') s = e = 0;
            else s = i1 = i2 = 0;
        }

        Console.WriteLine(result1);
        Console.WriteLine(result2);
    }
}