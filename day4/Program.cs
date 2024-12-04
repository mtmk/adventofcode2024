// https://adventofcode.com/2024/day/4/input

using System.Text.RegularExpressions;

List<List<char>> board = new();

foreach (var line in File.ReadAllLines("input"))
{
    List<char> row = new();
    board.Add(row);

    foreach (var c in line)
    {
        row.Add(c);
    }
}

var d = board.Count;
if (d == 0) throw new Exception();

var w = board[0].Count;
foreach (var row in board)
{
    if(row.Count != d) throw new Exception();
}
// Console.WriteLine($"{w}x{d}");

if (d != w) throw new Exception();
var x = d;

List<List<char>> diag1 = new();
List<List<char>> diag2 = new();
for (var i = 0; i < x; i++)
{
    List<char> row1 = new();
    if (x - 1 - i > 0) row1.AddRange(new string('.', x - 1 - i));
    row1.AddRange(board[i]);
    if (i > 0) row1.AddRange(new string('.', i));
    diag1.Add(row1);
    
    List<char> row2 = new();
    if (i > 0) row2.AddRange(new string('.', i));
    row2.AddRange(board[i]);
    if (x - 1 - i > 0) row2.AddRange(new string('.', x - 1 - i));
    diag2.Add(row2);
}
// Console.WriteLine("board");
// PrintBoard(board);
// Console.WriteLine();

// Console.WriteLine("diag1");
// PrintBoard(diag1);
// Console.WriteLine();
//
// Console.WriteLine("diag2");
// PrintBoard(diag2);
// Console.WriteLine();

List<List<char>> tdiag1 = new();
List<List<char>> tdiag2 = new();

for (int i = 0; i < diag1[0].Count; i++)
{
    tdiag1.Add(new List<char>(new string('.', x)));
    tdiag2.Add(new List<char>(new string('.', x)));
}

for (int i = 0; i < diag1.Count; i++)
{
    for (int j = 0; j < diag1[0].Count; j++)
    {
        tdiag1[j][i] = diag1[i][j];
        tdiag2[j][i] = diag2[i][j];
    }
}

List<List<char>> tboard = new();
for (int i = 0; i < board[0].Count; i++)
{
    tboard.Add(new List<char>(new string('.', board.Count)));
}

for (int i = 0; i < board.Count; i++)
{
    for (int j = 0; j < board[0].Count; j++)
    {
        tboard[j][i] = board[i][j];
        tboard[j][i] = board[i][j];
    }
}


// Console.WriteLine("tdiag1");
// PrintBoard(tdiag1);
// Console.WriteLine();
//
// Console.WriteLine("tdiag2");
// PrintBoard(tdiag2);
// Console.WriteLine();

var result1 = 0;

result1 += Search(board);
result1 += Search(tboard);
result1 += Search(tdiag1);
result1 += Search(tdiag2);

Console.WriteLine(result1);

var result2 = 0;
List<List<char>> x33 = new();
x33.Add(new List<char>(new string('.', 3)));
x33.Add(new List<char>(new string('.', 3)));
x33.Add(new List<char>(new string('.', 3)));

for (int i = 0; i < board.Count; i++)
{
    for (int j = 0; j < board[0].Count; j++)
    {
        if (i + 3 <= board.Count && j + 3 <= board[0].Count)
        {
            for (int m = 0; m < 3; m++)
            {
                for (var n = 0; n < 3; n++)
                {
                    x33[m][n] = board[i + m][j + n];
                }
            }

            if (IsXmas(x33)) result2++;
        }
    }
}

Console.WriteLine(result2);

void PrintBoard(List<List<char>> board)
{
    foreach (var row in board)
    {
        foreach (var c in row)
        {
            Console.Write(c);
        }
        Console.WriteLine();
    }
}

int Search(List<List<char>> board)
{
    var c = 0;
    foreach (var row in board)
    {
        var s = new string(row.ToArray());
        c += Regex.Matches(s, "XMAS").Count;
        c += Regex.Matches(s, "SAMX").Count;
    }

    return c;
}

bool IsXmas(List<List<char>> list)
{
    // Console.WriteLine();
    // PrintBoard(list);

    if (list[1][1] != 'A') return false;
    var x = new String(new[] { list[0][0], list[2][2] });
    var y = new String(new[] { list[0][2], list[2][0] });

    // Console.WriteLine($">>> x={x} y={y}");
    return (x == "MS" || x == "SM") && (y == "MS" || y == "SM");
}

/*

 123
 123
 123

 111
 222
 333

 00123
 01230
 12300

 001
 012
 123
 230
 300

  1
  12
  123
  23
  3

M.S
.A.
M.S

  M.S
 .A.
M.S

M.S
 .A.
  M.S

 */