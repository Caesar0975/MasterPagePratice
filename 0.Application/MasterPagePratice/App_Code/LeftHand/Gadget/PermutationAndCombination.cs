using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LeftHand.Gadget
{
    internal class PermutationAndCombination<T>
    {
        /// <summary>
        /// 交換兩個變量
        /// </summary>
        /// <param name="a">變量1</param>
        /// <param name="b">變量2</param>
        private static void Swap(ref T a, ref T b)
        {
            T temp = a;
            a = b;
            b = temp;
        }

        /// <summary>
        /// 遞歸算法求數組的組合(私有成員)
        /// </summary>
        /// <param name="list">返回的泛型</param>
        /// <param name="t">所求數組</param>
        /// <param name="n">輔助變量</param>
        /// <param name="m">輔助變量</param>
        /// <param name="b">輔助數組</param>
        /// <param name="M">輔助變量M</param>
        private static void GetCombination(ref List<T[]> list, T[] t, int n, int m, int[] b, int M)
        {
            for (int i = n; i >= m; i--)
            {
                b[m - 1] = i - 1;
                if (m > 1)
                {
                    GetCombination(ref list, t, i - 1, m - 1, b, M);
                }
                else
                {
                    if (list == null)
                    {
                        list = new List<T[]>();
                    }
                    T[] temp = new T[M];
                    for (int j = 0; j < b.Length; j++)
                    {
                        temp[j] = t[b[j]];
                    }
                    list.Add(temp);
                }
            }
        }

        /// <summary>
        /// 遞歸算法求排列(私有成員)
        /// </summary>
        /// <param name="list">返回的列表</param>
        /// <param name="t">所求數組</param>
        /// <param name="startIndex">起始標號</param>
        /// <param name="endIndex">結束標號</param>
        private static void GetPermutation(ref List<T[]> list, T[] t, int startIndex, int endIndex)
        {
            if (startIndex == endIndex)
            {
                if (list == null)
                {
                    list = new List<T[]>();
                }
                T[] temp = new T[t.Length];
                t.CopyTo(temp, 0);
                list.Add(temp);
            }
            else
            {
                for (int i = startIndex; i <= endIndex; i++)
                {
                    Swap(ref t[startIndex], ref t[i]);
                    GetPermutation(ref list, t, startIndex + 1, endIndex);
                    Swap(ref t[startIndex], ref t[i]);
                }
            }
        }

        /// <summary>
        /// 求從起始標號到結束標號的排列，其餘元素不變
        /// </summary>
        /// <param name="t">所求數組</param>
        /// <param name="startIndex">起始標號</param>
        /// <param name="endIndex">結束標號</param>
        /// <returns>從起始標號到結束標號排列的泛型</returns>
        public static List<T[]> GetPermutation(T[] t, int startIndex, int endIndex)
        {
            if (startIndex < 0 || endIndex > t.Length - 1)
            {
                return null;
            }
            List<T[]> list = new List<T[]>();
            GetPermutation(ref list, t, startIndex, endIndex);
            return list;
        }

        /// <summary>
        /// 返回數組所有元素的全排列
        /// </summary>
        /// <param name="t">所求數組</param>
        /// <returns>全排列的泛型</returns>
        public static List<T[]> GetPermutation(T[] t)
        {
            return GetPermutation(t, 0, t.Length - 1);
        }

        /// <summary>
        /// 求數組中n個元素的排列
        /// </summary>
        /// <param name="t">所求數組</param>
        /// <param name="n">元素個數</param>
        /// <returns>數組中n個元素的排列</returns>
        public static List<T[]> GetPermutation(T[] t, int n)
        {
            if (n > t.Length)
            {
                return null;
            }
            List<T[]> list = new List<T[]>();
            List<T[]> c = GetCombination(t, n);
            for (int i = 0; i < c.Count; i++)
            {
                List<T[]> l = new List<T[]>();
                GetPermutation(ref l, c[i], 0, n - 1);
                list.AddRange(l);
            }
            return list;
        }


        /// <summary>
        /// 求數組中n個元素的組合
        /// </summary>
        /// <param name="t">所求數組</param>
        /// <param name="n">元素個數</param>
        /// <returns>數組中n個元素的組合的泛型</returns>
        public static List<T[]> GetCombination(T[] t, int n)
        {
            if (t.Length < n)
            {
                return null;
            }
            int[] temp = new int[n];
            List<T[]> list = new List<T[]>();
            GetCombination(ref list, t, t.Length, n, temp, n);
            return list;
        }
    }

    public class PermutationAndCombination
    {
        /// <summary>
        /// 求從起始標號到結束標號的排列，其餘元素不變
        /// </summary>
        /// <param name="t">所求數組</param>
        /// <param name="startIndex">起始標號</param>
        /// <param name="endIndex">結束標號</param>
        /// <returns>從起始標號到結束標號排列的字串陣列</returns>
        public static List<string[]> GetPermutation(string[] t, int startIndex, int endIndex)
        {
            return PermutationAndCombination<string>.GetPermutation(t, startIndex, endIndex);
        }

        /// <summary>
        /// 返回數組所有元素的全排列
        /// </summary>
        /// <param name="t">所求數組</param>
        /// <returns>全排列的陣列</returns>
        public static List<string[]> GetPermutation(string[] t)
        {
            return PermutationAndCombination<string>.GetPermutation(t);
        }

        /// <summary>
        /// 求數組中n個元素的排列
        /// </summary>
        /// <param name="t">所求數組</param>
        /// <param name="n">元素個數</param>
        /// <returns>數組中n個元素的排列陣列</returns>
        public static List<string[]> GetPermutation(string[] t, int n)
        {
            return PermutationAndCombination<string>.GetPermutation(t, n);
        }

        /// <summary>
        /// 求數組中n個元素的組合
        /// </summary>
        /// <param name="t">所求數組</param>
        /// <param name="n">元素個數</param>
        /// <returns>數組中n個元素的組合的陣列</returns>
        public static List<string[]> GetCombination(string[] t, int n)
        {
            return PermutationAndCombination<string>.GetCombination(t, n);
        }

        /// <summary>
        /// 部分階乘計算(例如：M*(M-1)*(M-2).....N)
        /// </summary>
        public static int Factorial(int M, int N)
        {
            int ReturnValue = 1;

            if (M > N)
            { ReturnValue = M * Factorial(M - 1, N); }

            return ReturnValue;
        }

        /// <summary>
        /// 完整階乘計算(例如：M*(M-1)*(M-2).....1)(數字太大可能會溢位)
        /// </summary>
        public static int Factorial(int M)
        {
            return Factorial(M, 0);
        }

        /// <summary>
        /// 取得組合數量
        /// </summary>
        /// <param name="M">相異元素的數量</param>
        /// <param name="N">取數的數量</param>
        public static int GetCombinationCount(int M, int N)
        {
            if (N > M) { return 0; }
            if (N == M) { return 1; }

            //公式： C(M,N) = M!/(N!×N!) 
            //             = ((M)×(M-1)×(M-2)... ×N! )/(N!×N!) 
            //             = ((M)×(M-1)×(M-2)...(N+1))/N! 

            int Numerator = 1; //分子
            int Denominator = 1; //分母

            for (int i = 0; i < N; i++)
            { Numerator *= (M - i); }

            for (int i = 0; i < N; i++)
            { Denominator *= (N - i); }

            return Numerator / Denominator;
        }

        /// <summary>
        /// 將字串轉換成組合(會自動排除同)
        /// </summary>
        /// <param name="MString">相異元素的字串( ex: 1,2|3,4|5,6,7 )</param>
        /// <param name="N">取數的數量</param>
        /// <returns></returns>
        public static List<string[]> StringToCombinations(string DFNString, int N)
        {
            //預計回傳的結果
            List<string[]> Result = new List<string[]>();

            //找出所有的組合項目            
            List<string[]> AllCombinations = GetCombination(DFNString.Replace(',', '|').Split('|'), N);

            if (DFNString.Contains(",") == false) { return AllCombinations; }

            //排除相鄰組合項目

            //產生排除項目表（將DFNString 轉換成 List<List<string>> 樣式）
            List<List<string>> AllColumnNumbers = new List<List<string>>();
            foreach (string Numbers in DFNString.Split('|'))
            {
                List<string> TempColumn = Numbers.Split(',').ToList();
                AllColumnNumbers.Add(TempColumn);
            }

            //將所有的組合項目-排除項目表 = 正確有用的項目
            foreach (string[] Items in AllCombinations)
            {
                bool PassFlag = false;

                foreach (List<string> ColumnNumbers in AllColumnNumbers)
                {
                    if (ColumnNumbers.Count() - ColumnNumbers.Except(Items).Count() >= 2)
                    {
                        PassFlag = true;
                        break;
                    }
                }

                if (PassFlag == false)
                { Result.Add(Items); }
            }

            return Result;
        }
    }
}



