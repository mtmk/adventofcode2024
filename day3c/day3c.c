// https://adventofcode.com/2024/day/3/input

#include <stdio.h>

int main(int argc, char* argv[])
{
    FILE* file = NULL;
    const errno_t err = fopen_s(&file, "input", "r");
    if (err != 0)
    {
        printf("Failed to open file\n");
        return 1;
    }

    char c;
    int r1 = 0, r2 = 0, i1 = 0, i2 = 0, s = 0, e = 1;
    while ((c = (char)fgetc(file)) != EOF)
    {
        if (s == 0 && c == 'm') s = 1;
        else if (s == 1 && c == 'u') s = 2;
        else if (s == 2 && c == 'l') s = 3;
        else if (s == 3 && c == '(') s = 4;
        else if (s == 4 && c >= '0' && c <= '9') i1 = i1 * 10 + c - '0';
        else if (s == 4 && c == ',') s = 5;
        else if (s == 5 && c >= '0' && c <= '9') i2 = i2 * 10 + c - '0';
        else if (s == 5 && c == ')')
        {
            r1 += i1 * i2;
            r2 += i1 * i2 * e;
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
        else if (s == 12 && c == ')')
        {
            e = 0;
            s = 0;
        }
        else s = i1 = i2 = 0;
    }

    printf("%d\n", r1);
    printf("%d\n", r2);

    return 0;
}
