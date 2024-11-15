using System;
 
namespace greg
{
public static class PointCalculator 
{

    public static int totalPointsInWord(string word) // Gives more points for longer word
    {
        int total = 0;
        foreach (char c in word)
        {
            total += calculateSingleChar(c);
        }

        int pointsPerExtraLetter = 3;
        total += (word.Length - 3) * pointsPerExtraLetter;
        return total;
    }

    private static int calculateSingleChar(char c) // Scrabble Score 
    {
        c = Char.ToLower(c);
        switch (c){
            case 'a':
            case 'e':
            case 'i':
            case 'l':
            case 'n':
            case 'o':
            case 'r':
            case 's':
            case 't':
            case 'u':
                return 1;
            case 'd':
            case 'g':
                return 2;
            case 'b':
            case 'c':
            case 'm':
            case 'p':
                return 3;
            case 'f':
            case 'h':
            case 'v':
            case 'w':
            case 'y':
                return 4;
            case 'k':
                return 5;
            case 'j':
            case 'x':
                return 8;
            case 'q':
            case 'z':
                return 10;
            default:
                return 0;
        }
    }
}
    
}
