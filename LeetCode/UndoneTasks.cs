using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text.RegularExpressions;
using System.Text;
using System.Threading.Tasks;

namespace LeetCode
{
    public class UndoneTasks
    {
        public static IList<string> GenerateParenthesis(int n)
        {
            return default(List<string>);
        }

        public static string ReverseWords(string s)
        {
            Regex reg = new Regex(@" \s*");

            string cleanString = reg.Replace(s, " ").Trim();

            List<string> words = cleanString.Split(' ').Reverse().ToList();

            return string.Join(' ', words);
        }

        public static int Reverse(int x)
        {
            if (x < 0)
            {
                x *= -1;
                int.TryParse(new string(x.ToString().Reverse().ToList().ToArray()), out int result);
                return -result;
            }
            else
            {
                int.TryParse(new string(x.ToString().Reverse().ToList().ToArray()), out int result);
                return result;
            }
        }

        public static int SearchInsert(int[] nums, int target)
        {
            int left = 0;
            int right = nums.Length - 1;

            while (left <= right)
            {
                int mid = (left + right) / 2;

                if (target > nums[mid])
                {
                    left = mid + 1;
                }
                else if (target < nums[mid])
                {
                    right = mid - 1;
                }
                else
                {
                    return mid;
                }
            }

            return left;
        }

        public static int RemoveDuplicates(int[] nums)
        {
            int len = nums.Length;

            if (len == 0)
            {
                return 0;
            }

            List<int> indexs = new List<int>();
            indexs.Add(0);

            for (int i = 1; i < len; i++)
            {
                if (nums[i] != nums[i - 1])
                {
                    indexs.Add(i);
                }
            }

            int res = indexs.Count;
            int pointer = 0;

            foreach (int index in indexs)
            {
                nums[pointer] = nums[index];
                ++pointer;
            }

            return res;
        }

        public static bool IsValid(string s)
        {
            Stack<char> st = new Stack<char>();

            foreach (char c in s)
            {
                bool check = false;
                char result;
                switch (c)
                {
                    case '(':
                    case '{':
                    case '[':
                        st.Push(c);
                        break;
                    case ')':
                        check = st.TryPeek(out result);
                        if (check && result == '(')
                        {
                            st.Pop();
                        }
                        else
                        {
                            return false;
                        }
                        break;
                    case '}':
                        check = st.TryPeek(out result);
                        if (check && result == '{')
                        {
                            st.Pop();
                        }
                        else
                        {
                            return false;
                        }
                        break;
                    case ']':
                        check = st.TryPeek(out result);
                        if (check && result == '[')
                        {
                            st.Pop();
                        }
                        else
                        {
                            return false;
                        }
                        break;
                    default:
                        break;
                }
            }
            return st.Count == 0;
        }

        public static int RomanToInt(string s)
        {
            Dictionary<char, int> romanDict = new Dictionary<char, int>()
            {
                { 'I', 1 },
                { 'V', 5 },
                { 'X', 10 },
                { 'L', 50 },
                { 'C', 100 },
                { 'D', 500 },
                { 'M', 1000 },
            };

            int res = romanDict[s[0]];
            int buffer = res;

            for (int i = 1; i < s.Length; i++)
            {
                int cur = romanDict[s[i]];

                if (romanDict[s[i - 1]] == cur)
                {
                    res += cur;
                    buffer += cur;
                }
                else if (romanDict[s[i - 1]] < cur)
                {
                    res += (cur - 2 * buffer);
                    buffer = 0;
                }
                else
                {
                    res += cur;
                    buffer = cur;
                }
            }

            return res;
        }

        public static string LongestCommonPrefix(string[] strs)
        {
            List<string> lst = strs.ToList();

            string firstStr = lst[0];

            for (int i = 0; i < firstStr.Length; i++)
            {
                int newLen = lst.Where(x => x.ElementAtOrDefault(i) == firstStr.ElementAtOrDefault(i)).ToList().Count;

                if (newLen != lst.Count)
                {
                    return firstStr.Substring(0, i);
                }
            }

            return firstStr;
        }

        public static int[] TwoSum(int[] nums, int target)
        {
            int len = nums.Length;
            int[] index = new int[len];

            for (int i = 0; i < len; i++)
            {
                index[i] = i;
            }

            Array.Sort(nums, index);

            for (int i = 0; i < len - 1; i++)
            {
                for (int j = i + 1; j < len; j++)
                {
                    if (nums[i] + nums[j] == target)
                    {
                        return new int[] { index[i], index[j] };
                    }
                    else if (nums[i] + nums[j] > target)
                    {
                        break;
                    }
                }
            }

            return new int[2];
        }
    }
}
