using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace LeetCode
{
    class Program
    {
        static void Main(string[] args)
        {
            //int[][] matrix = new int[][]
            //{
            //    new int[] { 1, 2, 3, 4, 5 },
            //    new int[] { 6, 7 , 8, 9, 10 },
            //    new int[] { 11, 12, 13, 14, 15 },
            //    new int[] { 16, 17, 18, 19, 20 },
            //    new int[] { 21, 22, 23, 24, 25 }
            //};
            //Rotate(matrix);
            //foreach (int[] row in matrix)
            //{
            //    foreach (int col in row)
            //    {
            //        Console.Write($"{col}, ");
            //    }
            //    Console.WriteLine();
            //}

            //IEnumerable<IEnumerable<char>> res = GetPermutations("BBAA".ToCharArray().ToList(), 4);
            //foreach (IEnumerable<char> item in res)
            //{
            //    string output = new string(item.ToList().ToArray());
            //    Console.WriteLine(output);
            //}
            // Console.WriteLine(MaxSliceSum(new int[] { -10, 8, 5, -3, 16 }));

            //List<int> elements = Enumerable.Range(1, 2).ToList();
            int[][] board = new int[][]
            {
                new int[] { 3, 1, 1 },
                new int[] { 2, 5, 1 },
                new int[] { 1, 5, 5 },
                new int[] { 2, 1, 1 }
            };
            Console.WriteLine(BombCleaner(board));
        }

        public static int BombCleaner(int[][] board)
        {
            if (board.Length == 0) return 0;

            int cols = board[0].Length;

            return BombCleanerUtil(board, 0, 0, cols - 1, new Hashtable());
        }

        private static int BombCleanerUtil(int[][] board, int current, int first, int second, Hashtable memo)
        {
            Console.WriteLine($"{board[current][first]}, {board[current][second]}");

            if (current == board.Length - 1)
            {
                return board[current][first] + board[current][second];
            }

            int cols = board[current].Length;

            int nextFirst = first;
            int nextSecond = second;

            List<Tuple<int, int>> available = new List<Tuple<int, int>>();
            int[] currentRow = board[current + 1];

            if (second - first == 1)
            {
                available.Add(new Tuple<int, int>(first, currentRow[first]));
                available.Add(new Tuple<int, int>(second, currentRow[second]));
                if (first != 0)
                {
                    available.Add(new Tuple<int, int>(first - 1, currentRow[first - 1]));
                }
                if (second != cols - 1)
                {
                    available.Add(new Tuple<int, int>(second + 1, currentRow[second + 1]));
                }
                nextFirst = available.Aggregate((x, y) => x.Item2 > y.Item2 ? x : y).Item1;
                available.RemoveAt(available.FindIndex((x) => x.Item1 == nextFirst));
                nextSecond = available.Aggregate((x, y) => x.Item2 > y.Item2 ? x : y).Item1;
            }
            else if (second - first == 2)
            {
                List<Tuple<int, int>> available1 = new List<Tuple<int, int>>
                {
                    new Tuple<int, int>(first, currentRow[first]),
                    new Tuple<int, int>(first + 1, currentRow[first + 1])
                };
                List<Tuple<int, int>> available2 = new List<Tuple<int, int>>
                {
                    new Tuple<int, int>(second, currentRow[second]),
                    new Tuple<int, int>(second - 1, currentRow[second - 1])
                };

                if (first != 0)
                {
                    available1.Add(new Tuple<int, int>(first - 1, currentRow[first - 1]));
                }
                if (second != cols - 1)
                {
                    available2.Add(new Tuple<int, int>(second + 1, currentRow[second + 1]));
                }
                nextFirst = available1.Aggregate((x, y) => x.Item2 > y.Item2 ? x : y).Item1;
                available1.RemoveAt(available1.FindIndex((x) => x.Item1 == nextFirst));
                int candidate1 = available1.Aggregate((x, y) => x.Item2 > y.Item2 ? x : y).Item1;

                nextSecond = available2.Aggregate((x, y) => x.Item2 > y.Item2 ? x : y).Item1;
                available2.RemoveAt(available2.FindIndex((x) => x.Item1 == nextSecond));
                int candidate2 = available2.Aggregate((x, y) => x.Item2 > y.Item2 ? x : y).Item1;

                if (nextFirst == nextSecond)
                {
                    if (currentRow[candidate1] > currentRow[candidate2])
                    {
                        nextFirst = candidate1;
                    }
                    else
                    {
                        nextSecond = candidate2;
                    }
                }
            }
            else
            {
                List<Tuple<int, int>> available1 = new List<Tuple<int, int>>
                {
                    new Tuple<int, int>(first, currentRow[first]),
                    new Tuple<int, int>(first + 1, currentRow[first + 1])
                };
                List<Tuple<int, int>> available2 = new List<Tuple<int, int>>
                {
                    new Tuple<int, int>(second, currentRow[second]),
                    new Tuple<int, int>(second - 1, currentRow[second - 1])
                };

                if (first != 0)
                {
                    available1.Add(new Tuple<int, int>(first - 1, currentRow[first - 1]));
                }
                if (second != cols - 1)
                {
                    available2.Add(new Tuple<int, int>(second + 1, currentRow[second + 1]));
                }
                nextFirst = available1.Aggregate((x, y) => x.Item2 > y.Item2 ? x : y).Item1;
                nextSecond = available2.Aggregate((x, y) => x.Item2 > y.Item2 ? x : y).Item1;
            }

            return board[current][first] + board[current][second] + BombCleanerUtil(board, current + 1, nextFirst < nextSecond ? nextFirst : nextSecond, nextFirst > nextSecond ? nextFirst : nextSecond, memo);
        }

        public static int BinaryGap(int N)
        {
            int count = 0, gap = 0;
            string findingBinStr = Convert.ToString(N, 2).TrimEnd('0');
            foreach (char c in findingBinStr)
            {
                if (c == '1')
                {
                    if (gap > count)
                    {
                        count = gap;
                    }
                    gap = 0;
                }
                else
                {
                    ++gap;
                }
            }
            return count;
        }

        static List<List<int>> comb;
        static bool[] used;
        static void GetCombinationSample()
        {
            int[] arr = { 10, 2, 2 };
            used = new bool[arr.Length];
            comb = new List<List<int>>();
            List<int> c = new List<int>();
            GetComb(arr, 0, c);
            foreach (var item in comb)
            {
                foreach (var x in item)
                {
                    Console.Write(x + ",");
                }
                Console.WriteLine("");
            }
        }
        static void GetComb(int[] arr, int colindex, List<int> c)
        {

            if (colindex >= arr.Length)
            {
                comb.Add(new List<int>(c));
                return;
            }
            for (int i = 0; i < arr.Length; i++)
            {
                if (!used[i])
                {
                    used[i] = true;
                    c.Add(arr[i]);
                    GetComb(arr, colindex + 1, c);
                    c.RemoveAt(c.Count - 1);
                    used[i] = false;
                }
            }
        }

        public static int Task3(String S)
        {
            int count = 0;
            List<char> chars = S.ToCharArray().ToList();
            IEnumerable<IEnumerable<char>> res = GetPermutations(chars, chars.Count);
            foreach (IEnumerable<char> item in res)
            {
                string output = new string(item.ToList().ToArray());
                if (!IsStartVowel(output) && !IsConsecutiveVowels(output) && !IsConsecutiveConsonants(output))
                {
                    ++count;
                    Console.WriteLine(output);
                }
            }
            return count;
        }

        private static IEnumerable<IEnumerable<T>> GetPermutations<T>(IEnumerable<T> list, int length)
        {
            if (length == 1) return list.Select(t => new T[] { t });
            return GetPermutations(list, length - 1)
                .SelectMany(t => list.Where(o => !t.Contains(o)),(t1, t2) => t1.Concat(new T[] { t2 }));
        }

        private static bool IsStartVowel(string S)
        {
            List<char> vowels = new List<char>() { 'A', 'E', 'I', 'O', 'U' };
            return vowels.Contains(S[0]);
        }

        private static bool IsConsecutiveVowels(String S)
        {
            // AEIOU
            List<char> vowels = new List<char>() { 'A', 'E', 'I', 'O', 'U' };
            int prevIndex = -1;
            for (int i = 0; i < S.Length; i++)
            {
                if (vowels.Contains(S[i]))
                {
                    if (prevIndex == -1)
                    {
                        prevIndex = i;
                    }
                    else
                    {
                        return true;
                    }
                }
                else
                {
                    prevIndex = -1;
                }
            }
            return false;
        }

        private static bool IsConsecutiveConsonants(String S)
        {
            List<char> vowels = new List<char>() { 'A', 'E', 'I', 'O', 'U' };
            int prevIndex = -1;
            for (int i = 0; i < S.Length; i++)
            {
                if (!vowels.Contains(S[i]))
                {
                    if (prevIndex == -1)
                    {
                        prevIndex = i;
                    }
                    else
                    {
                        return true;
                    }
                }
                else
                {
                    prevIndex = -1;
                }
            }
            return false;
        }

        public static int Task2(int N)
        {
            string s = N.ToString();
            int foundIndex = 0;
            if (N < 0)
            {
                s = s[1..];
            }
            for (int i = 0; i < s.Length; i++)
            {
                int num = s[i] - '0';
                if (N < 0)
                {
                    if (num >= 5)
                    {
                        break;
                    }
                }
                else
                {
                    if (num <= 5)
                    {
                        break;
                    }
                }
                foundIndex++;
            }
            int result = int.Parse(s.Insert(foundIndex, "5"));
            return N < 0 ? -result: result;
        }

        public static int MissingInteger(int[] A)
        {
            int result = 0;
            A = A.Where(x => x > 0).OrderBy(x => x).ToArray();

            foreach (int item in A)
            {
                if (result + 1 < item)
                {
                    return result + 1;
                }
                result = item;
            }

            return result + 1;
        }

        public static int[] SmallerNumbersThanCurrent(int[] nums)
        {
            return nums.Select(num => SmallerNumbersCount(nums, num)).ToArray();
        }

        private static int SmallerNumbersCount(int[] nums, int theNum)
        {
            return nums.Aggregate(0, (count, num) => num < theNum ? count + 1 : count);
        }
    }
}
