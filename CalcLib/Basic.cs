using System;

namespace CalcLib
{
    internal static class Basic
    {
        internal static double Parse(string str, int l, int r)
        {
            double result = 0;
            bool dot = false;
            int extent = 1;
            for (int i = l; i <= r; i++)
            {
                if (IsNumber(str[i]))
                {
                    if (dot)
                    {
                        result += (str[i] - 48) / Math.Pow(10, extent);
                        extent++;
                    }
                    else
                    {
                        result *= 10;
                        result += str[i] - 48;
                    }
                }
                else if (IsDot(str[i]))
                {
                    dot = true;
                    continue;
                }
                else if (IsUppLetter(str[i]))
                {
                    if (i < r && str[i] == 'P' && str[i + 1] == 'I')
                    {
                        return Math.PI;
                    }
                    else if (str[i] == 'E')
                    {
                        result = Math.E;
                    }
                }
                else if (!IsBracket(str[i]))
                {
                    return double.NaN;
                }
            }
            return result;
        }
        internal static bool Nesting(ref int nest, char c)
        {
            if (c == ')')
            {
                nest++;
                return true;
            }
            else if (c == '(')
            {
                nest--;
            }
            return false;
        }
        internal static bool IsLowLetter(char c)
        {
            return c >= 97 && c <= 122;
        }
        internal static bool IsUppLetter(char c)
        {
            return c >= 65 && c <= 90;
        }
        internal static bool IsNumber(char c)
        {
            return c >= 48 && c <= 57;
        }
        internal static bool IsDot(char c)
        {
            return c == ',' || c == '.';
        }
        internal static bool IsBracket(char c)
        {
            return c == '(' || c == ')';
        }
        internal static bool IsNumberOrLowLetter(char c)
        {
            return IsNumber(c) || IsLowLetter(c);
        }
        internal static bool IsSin(string str, int i)
        {
            return str[i - 2] == 's' && str[i - 1] == 'i' && str[i] == 'n';
        }
        internal static bool IsCos(string str, int i)
        {
            return str[i - 2] == 'c' && str[i - 1] == 'o' && str[i] == 's';
        }
        internal static bool IsTan(string str, int i)
        {
            return str[i - 2] == 't' && str[i - 1] == 'a' && str[i] == 'n';
        }
        internal static bool IsCot(string str, int i)
        {
            return str[i - 2] == 'c' && str[i - 1] == 'o' && str[i] == 't';
        }
    }
}
