using System;
using static CalcLib.Basic;
namespace CalcLib
{
    public static class Calculator
    {
        public static double Calc(string str)
        {
            return Compute(str);
        }
        private static double Compute(string str, int l = 0, int r = -1)
        {
            if (str.Length == 0) return 0;
            else if (r == -1) r = (short)str.Length - 1;//right border

            CALC:

            #region CALCULATE
            int nest = 0;//nesting brackets
            int rbracket = 0;//right bracket
            //SUM_SUB
            for (int i = r; i >= l; i--)
            {
                Nesting(ref nest, str[i]);
                if (nest == 0 && i < str.Length - 1)
                {
                    if (i != l)
                    {
                        if (str[i] == '+')
                        {
                            return Compute(str, l, i - 1) + Compute(str, i + 1, r);
                        }
                        else if (str[i] == '-')
                        {
                            if (IsNumber(str[i - 1]) || str[i - 1] == ')')
                            {
                                return Compute(str, l, i - 1) - Compute(str, i + 1, r);
                            }
                            else if (i != 1 && str[i - 1] == '-')
                            {
                                return Compute(str, l, i - 2) + Compute(str, i + 1, r);
                            }
                            else if (str[i - 1] == '-')
                            {
                                return Compute(str, i + 1, r);
                            }
                        }
                    }
                }
            }
            nest = 0;
            //MUL_DIV
            for (int i = r; i >= l; i--)
            {
                Nesting(ref nest, str[i]);
                if (nest == 0 && i != l && i < str.Length - 1)
                {
                    if (str[i] == '*')
                    {
                        return Compute(str, l, i - 1) * Compute(str, i + 1, r);
                    }
                    else if (str[i] == '/')
                    {
                        return Compute(str, l, i - 1) / Compute(str, i + 1, r);
                    }
                }
                else if (str[i] == '-')
                {
                    return -Compute(str, i + 1, r);
                }
                else if (str[i] == '+')
                {
                    return Compute(str, i + 1, r);
                }
            }
            nest = 0;
            //EXP_SQRT_NEG
            for (int i = r; i >= l; i--)
            {
                if (Nesting(ref nest, str[i]))
                {
                    rbracket = i;
                }
                if (nest == 0 && i < str.Length - 1)
                {
                    if (str[i] == '^' && i != l)
                    {
                        return Math.Pow(Compute(str, l, i - 1), Compute(str, i + 1, r));
                    }
                    else if (str[i] == 'v')
                    {
                        if (i != l && (IsNumber(str[i - 1]) || str[i - 1] == ')'))
                        {
                            return Math.Pow(Compute(str, i + 1, r), 1 / Compute(str, l, i - 1));
                        }
                        else
                        {
                            return Math.Sqrt(Compute(str, i + 1, r));
                        }
                    }
                    else if (IsLowLetter(str[i]))
                    {
                        if (i + 3 >= l)
                        {
                            if (IsSin(str, i))
                            {
                                return Math.Sin(Compute(str, i + 1, rbracket));
                            }
                            else if (IsCos(str, i))
                            {
                                return Math.Cos(Compute(str, i + 1, rbracket));
                            }
                            else if (IsTan(str, i))
                            {
                                return Math.Tan(Compute(str, i + 1, rbracket));
                            }
                            else if (IsCot(str, i))
                            {
                                return 1 / Math.Tan(Compute(str, i + 1, rbracket));
                            }
                        }
                    }
                }
            }
            #endregion

            if (str[l] == '(' && str[r] == ')')//remove_brackets
            {
                l++;
                r--;
                goto CALC;
            }

            return Parse(str, l, r);
        }
    }
}