using System;
using System.Collections.Generic;
using static CalcLib.Basic;

namespace CalcLib
{
    public class Function
    {
        private Dictionary<char, double> values = new Dictionary<char, double>();
        private string func = "";

        #region CONSTRUCTORS
        public Function(string func, params (char letter, double num)[] nums)
        {
            this.func = func;
            for (int i = 0; i < nums.Length; i++)
            {
                if (!values.ContainsKey(nums[i].letter) && IsLowLetter(nums[i].letter))
                {
                    values.Add(nums[i].letter, nums[i].num);
                }
            }
        }
        public Function(string func)
        {
            this.func = func;
        }
        public Function()
        {

        }
        #endregion

        #region METHODS
        public Function Replace(char letter, double num)
        {
            if (IsLowLetter(letter) && values.ContainsKey(letter))
            {
                values[letter] = num;
            }
            return this;
        }
        public Function Replace(string func)
        {
            this.func = func;
            return this;
        }
        public Function Add(char letter, double num)
        {
            if (IsLowLetter(letter) && !values.ContainsKey(letter))
            {
                values.Add(letter, num);
            }
            return this;
        }
        #endregion

        public double Calc()
        {
            return Compute(func);
        }
        private double Compute(string str, int l = 0, int r = -1)
        {
            if (str.Length == 0) return 0;
            else if (r == -1) r = (short)str.Length - 1;//right_border

            if (r - l == 0 && values.ContainsKey(str[l]))
            {
                return values[str[l]];
            }

        CALC:

            #region CALCULATE
            int nest = 0;//nesting_brackets
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
                            if (IsNumberOrLowLetter(str[i - 1]) || str[i - 1] == ')')
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
            //EXP_SQRT
            for (int i = r; i >= l; i--)
            {
                Nesting(ref nest, str[i]);
                if (nest == 0 && i < str.Length - 1)
                {
                    if (str[i] == '^' && i != l)
                    {
                        return Math.Pow(Compute(str, l, i - 1), Compute(str, i + 1, r));
                    }
                    else if (str[i] == 'v')
                    {
                        if (i != l && (IsNumberOrLowLetter(str[i - 1]) || str[i - 1] == ')'))
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
                                return Math.Sin(Compute(str, i + 1, r));
                            }
                            else if (IsCos(str, i))
                            {
                                return Math.Cos(Compute(str, i + 1, r));
                            }
                            else if (IsTan(str, i))
                            {
                                return Math.Tan(Compute(str, i + 1, r));
                            }
                            else if (IsCot(str, i))
                            {
                                return 1 / Math.Tan(Compute(str, i + 1, r));
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