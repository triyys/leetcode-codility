using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCode
{
    public class DoneTasks
    {
        public static int UniquePaths(int m, int n)
        {
            Hashtable memo = new Hashtable();
            memo.Add("tri", 0);
            int result = UniquePathsUtil(m, n, memo);
            Console.WriteLine($"Time Complexity O(m * n): {memo["tri"]}");
            return result;
        }
        private static int UniquePathsUtil(int m, int n, Hashtable memo)
        {
            if (m == 0 || n == 0)
            {
                return 0;
            }
            if (m == 1 && n == 1)
            {
                return 1;
            }
            string key = $"{m}|{n}";
            if (memo.ContainsKey(key))
            {
                return (int)memo[key];
            }
            int count = (int)memo["tri"] + 1;
            memo["tri"] = count;
            memo[key] = UniquePathsUtil(m - 1, n, memo) + UniquePathsUtil(m, n - 1, memo);
            return (int)memo[key];
        }

        public static List<List<T>> GetPermutations<T>(List<T> elements)
        {
            List<List<T>> queue = elements
                .Select((x) => new List<T>() { x })
                .ToList();
            do
            {
                List<T> dequeueElement = queue.First();
                queue.RemoveAt(0);

                foreach (T element in elements)
                {
                    if (!dequeueElement.Contains(element))
                    {
                        queue.Add(dequeueElement.Append(element).ToList());
                    }
                }
            } while (queue.First().Count != elements.Count);
            return queue;
        }

        public static int MaxSliceSum(int[] A)
        {
            int len = A.Length;
            long currentSum = 0;
            if (len == 1)
            {
                return A[0];
            }
            long[] prefixSum = new long[len];
            for (int i = 0; i < len; i++)
            {
                currentSum += A[i];
                prefixSum[i] = currentSum;
            }
            long result = prefixSum[0], leftMin = prefixSum[0], maxValue = prefixSum[0];
            for (int i = 1; i < len; i++)
            {
                if (prefixSum[i] > maxValue)
                {
                    maxValue = prefixSum[i];
                }
                long difference = prefixSum[i] - leftMin;
                if (difference > result)
                {
                    result = difference;
                }
                if (prefixSum[i] < leftMin)
                {
                    leftMin = prefixSum[i];
                }
            }
            return (int)(result > maxValue ? result : maxValue);
        }

        public static int MaxProfit(int[] A)
        {
            int len = A.Length, max = 0;
            if (len == 0)
            {
                return max;
            }
            int leftMin = A[0];
            for (int i = 1; i < len; i++)
            {
                int difference = A[i] - leftMin;
                if (difference > max)
                {
                    max = difference;
                }
                if (A[i] < leftMin)
                {
                    leftMin = A[i];
                }
            }
            return max;
        }

        public static int EquiLeader(int[] A)
        {
            // find leader for array A
            int len = A.Length, count = 0;
            Stack<int> st = new Stack<int>();
            for (int i = 0; i < len; i++)
            {
                if (st.Count == 0 || A[i] == st.Peek())
                {
                    st.Push(A[i]);
                }
                else
                {
                    st.Pop();
                }
            }
            if (st.Count == 0)
            {
                return count;
            }
            int candidate = st.Peek();
            int candidateCount = 0;
            for (int i = 0; i < len; i++)
            {
                if (A[i] == candidate)
                {
                    ++candidateCount;
                }
            }
            if (candidateCount <= len / 2)
            {
                return count;
            }
            // main solution
            int leftLeaderCount = 0;
            for (int i = 0; i < len - 1; i++)
            {
                // [0..i] [i + 1 ... len - 1]
                // i + 1     len - i - 1
                if (A[i] == candidate)
                {
                    ++leftLeaderCount;
                }
                int rightLeaderCount = candidateCount - leftLeaderCount;
                if (leftLeaderCount > (i + 1) / 2 && rightLeaderCount > (len - i - 1) / 2)
                {
                    ++count;
                }
            }
            return count;
        }

        public static int Dominator(int[] A)
        {
            int len = A.Length;
            Stack<int> st = new Stack<int>();
            for (int i = 0; i < len; i++)
            {
                if (st.Count == 0 || st.Peek() == A[i])
                {
                    st.Push(A[i]);
                }
                else
                {
                    st.Pop();
                }
            }
            if (st.Count == 0)
            {
                return -1;
            }
            int candidate = st.Pop();
            int result = 0, count = 0;
            for (int i = 0; i < len; i++)
            {
                if (candidate == A[i])
                {
                    result = i;
                    ++count;
                }
            }
            return count > len / 2 ? result : -1;
        }

        public static int StoneWall(int[] H)
        {
            int len = H.Length, count = 1;
            Stack<int> st = new Stack<int>();
            st.Push(H[0]);
            for (int i = 1; i < len; i++)
            {
                while (st.Count != 0 && H[i] < st.Peek())
                {
                    st.Pop();
                }
                if (st.Count == 0 || H[i] != st.Peek() || H[i] > st.Peek())
                {
                    st.Push(H[i]);
                    ++count;
                }
            }
            return count;
        }

        public static int Nesting(string S)
        {
            int len = S.Length;
            Stack<char> st = new Stack<char>();
            for (int i = 0; i < len; i++)
            {
                if (S[i] == '(')
                {
                    st.Push(S[i]);
                }
                else
                {
                    if (st.Count != 0)
                    {
                        st.Pop();
                    }
                    else
                    {
                        return 0;
                    }
                }
            }
            return st.Count == 0 ? 1 : 0;
        }

        public static int Fish(int[] A, int[] B)
        {
            int len = A.Length, result = len;
            Stack<int> st = new Stack<int>();
            for (int i = 0; i < len; i++)
            {
                if (B[i] == 1)
                {
                    st.Push(A[i]);
                }
                else
                {
                    if (st.Count != 0)
                    {
                        int currentFish = st.Peek();
                        if (currentFish < A[i])
                        {
                            st.Pop();
                            --i;
                        }
                        --result;
                    }
                }
            }
            return result;
        }

        public static int Brackets(string S)
        {
            Stack<char> st = new Stack<char>();
            int len = S.Length;
            for (int i = 0; i < len; i++)
            {
                if (S[i] == '{' || S[i] == '(' || S[i] == '[')
                {
                    st.Push(S[i]);
                }
                else
                {
                    if (st.Count == 0)
                    {
                        return 0;
                    }
                    if (S[i] == '}')
                    {
                        if (st.Pop() != '{')
                        {
                            return 0;
                        }
                    }
                    else if (S[i] == ')')
                    {
                        if (st.Pop() != '(')
                        {
                            return 0;
                        }
                    }
                    else if (S[i] == ']')
                    {
                        if (st.Pop() != '[')
                        {
                            return 0;
                        }
                    }
                }
            }
            return st.Count == 0 ? 1 : 0;
        }

        public static int Triangle(int[] A)
        {
            int len = A.Length;
            if (len < 3)
            {
                return 0;
            }
            List<int> L = new List<int>(A);
            L.Sort();
            for (int i = 0; i < len - 2; i++)
            {
                try
                {
                    if (checked(L[i] + L[i + 1]) > L[i + 2])
                    {
                        return 1;
                    }
                }
                catch (OverflowException e)
                {
                    return L[i] > 0 ? 1 : 0;
                }
            }
            return 0;
        }

        public static int MaxProductOfThree(int[] A)
        {
            List<int> L = new List<int>(A);
            L.Sort();
            int len = A.Length, result = L[len - 1] * L[len - 2] * L[len - 3];
            if (L[len - 1] > 0)
            {
                if (L[0] * L[1] > L[len - 2] * L[len - 3])
                {
                    result = L[0] * L[1] * L[len - 1];
                }
            }
            return result;
        }

        public static int Distinct(int[] A)
        {
            int len = A.Length, count = 1;
            if (len == 0)
            {
                return 0;
            }
            List<int> L = new List<int>(A);
            L.Sort();
            int prev = L[0];
            for (int i = 1; i < len; i++)
            {
                if (L[i] != L[i - 1])
                {
                    ++count;
                }
            }
            return count;
        }

        public static int MinAvgTwoSlice(int[] A)
        {
            int len = A.Length, result = 0;
            float min = (A[0] + A[1]) / 2;
            for (int i = 0; i < len - 2; i++)
            {
                float average2 = (A[i] + A[i + 1]) / 2f;
                float average3 = (A[i] + A[i + 1] + A[i + 2]) / 3f;
                float smaller = average2 < average3 ? average2 : average3;
                if (smaller < min)
                {
                    min = smaller;
                    result = i;
                }
            }
            float lastPair = (A[len - 2] + A[len - 1]) / 2f;
            if (lastPair < min)
            {
                result = len - 2;
            }
            return result;
        }

        public static int[] GenomicRangeQuery(String S, int[] P, int[] Q)
        {
            int N = S.Length, M = P.Length;
            Dictionary<char, int> dict = new Dictionary<char, int>()
            {
                { 'A', 1 },
                { 'C', 2 },
                { 'G', 3 },
                { 'T', 4 },
            };
            int[,] prefixSum = new int[N, 3];
            int[] currentPrefixSum = new int[3];
            for (int i = 0; i < N; i++)
            {
                if (S[i] != 'T')
                {
                    ++currentPrefixSum[dict[S[i]] - 1];
                }
                prefixSum[i, 0] = currentPrefixSum[0];
                prefixSum[i, 1] = currentPrefixSum[1];
                prefixSum[i, 2] = currentPrefixSum[2];
            }
            int[] output = new int[M];
            for (int i = 0; i < M; i++)
            {
                int[] compare = new int[3];
                if (P[i] > 0)
                {
                    compare[0] = prefixSum[Q[i], 0] - prefixSum[P[i] - 1, 0];
                    compare[1] = prefixSum[Q[i], 1] - prefixSum[P[i] - 1, 1];
                    compare[2] = prefixSum[Q[i], 2] - prefixSum[P[i] - 1, 2];
                }
                else
                {
                    compare[0] = prefixSum[Q[i], 0];
                    compare[1] = prefixSum[Q[i], 1];
                    compare[2] = prefixSum[Q[i], 2];
                }
                if (compare[0] != 0)
                {
                    output[i] = 1;
                }
                else if (compare[1] != 0)
                {
                    output[i] = 2;
                }
                else if (compare[2] != 0)
                {
                    output[i] = 3;
                }
                else
                {
                    output[i] = 4;
                }
            }
            return output;
        }

        public static int CountDiv(int A, int B, int K)
        {
            int left = A / K * K + (A % K == 0 ? 0 : 1);
            int right = B / K * K;
            return left > right ? 0 : (right - left) / K + 1;
        }

        public static int[] MaxCounters(int N, int[] A)
        {
            int len = A.Length, currentMax = 0, prevMax = 0;
            bool flag = false;
            int[] output = new int[N];
            for (int i = 0; i < len; i++)
            {
                if (A[i] > N)
                {
                    if (flag)
                    {
                        continue;
                    }
                    else
                    {
                        prevMax = currentMax;
                        flag = true;
                    }
                }
                else
                {
                    if (output[A[i] - 1] < prevMax)
                    {
                        output[A[i] - 1] = prevMax;
                    }
                    ++output[A[i] - 1];
                    if (output[A[i] - 1] > currentMax)
                    {
                        ++currentMax;
                    }
                    flag = false;
                }
            }
            for (int i = 0; i < N; i++)
            {
                if (output[i] < prevMax)
                {
                    output[i] = prevMax;
                }
            }
            return output;
        }

        public static int PassingCars(int[] A)
        {
            int count = 0, passed = 0, len = A.Length;
            for (int i = 0; i < len; i++)
            {
                if (A[i] == 0)
                {
                    ++passed;
                }
                else
                {
                    count += passed;
                }
                if (count > 1000000000)
                {
                    return -1;
                }
            }
            return count;
        }

        public static int PermCheck(int[] A)
        {
            int len = A.Length;
            List<bool> lb = new List<bool>(new bool[len]);
            for (int i = 0; i < len; i++)
            {
                if (A[i] > len || lb[A[i] - 1])
                {
                    return 0;
                }
                lb[A[i] - 1] = true;
            }
            return 1;
        }

        public static int FrogRiverOne(int X, int[] A)
        {
            List<bool> lb = new List<bool>(new bool[X]);
            int len = A.Length;
            for (int i = 0; i < len; i++)
            {
                if (!lb[A[i] - 1])
                {
                    lb[A[i] - 1] = true;
                    --X;
                    if (X == 0)
                    {
                        return i;
                    }
                }
            }
            return -1;
        }

        public static int TapeEquilibrium(int[] A)
        {
            // count sum
            int sum = 0, len = A.Length;
            for (int i = 0; i < len; i++)
            {
                sum += A[i];
            }
            // main solution
            int min = int.MaxValue;
            int currentSum = A[0];
            for (int i = 1; i < len; i++)
            {
                int comp = Math.Abs(sum - 2 * currentSum);
                if (comp < min)
                {
                    min = comp;
                }
                currentSum += A[i];
            }
            return min;
        }

        public static int PermMissingElem(int[] A)
        {
            int len = A.Length;
            if (len == 0)
            {
                return 1;
            }
            List<int> L = A.ToList();
            L.Sort();
            for (int i = 0; i < len; i++)
            {
                if (L[i] != i + 1)
                {
                    return i + 1;
                }
            }
            return L.Last() + 1;
        }

        public static int OddOccurrencesInArray(int[] A)
        {
            int len = A.Length;
            Dictionary<int, int> dict = new Dictionary<int, int>();
            for (int i = 0; i < len; i++)
            {
                if (!dict.ContainsKey(A[i]))
                {
                    dict.Add(A[i], 1);
                }
                else
                {
                    dict[A[i]]++;
                }
            }
            foreach (var item in dict)
            {
                if (item.Value % 2 == 1)
                {
                    return item.Key;
                }
            }
            return -1;
        }

        public static int[] CyclicRotation(int[] A, int K)
        {
            int len = A.Length;
            if (len == 0)
            {
                return A;
            }
            K = K % len;
            List<int> L = A.ToList();
            List<int> B = L.GetRange(len - K, K);
            L.RemoveRange(len - K, K);
            return B.Concat(L).ToList().ToArray();
        }

        public static string DestCity(IList<IList<string>> paths)
        {
            IList<string> cityAList = paths.Select(path => path.First()).ToList();

            foreach (IList<string> path in paths)
            {
                if (!cityAList.Contains(path.Last()))
                {
                    return path.Last();
                }
            }

            return "";
        }

        public static void Rotate(int[][] matrix)
        {
            int n = matrix.Length;
            for (int i = 0; i < n / 2; i++)
            {
                for (int j = i; j < n - 1 - i; j++)
                {
                    int temp = matrix[i][j];
                    // left top = left bottom;
                    matrix[i][j] = matrix[n - 1 - j][i];
                    // left bottom = right bottom;
                    matrix[n - 1 - j][i] = matrix[n - 1 - i][n - 1 - j];
                    // right bottom = right top;
                    matrix[n - 1 - i][n - 1 - j] = matrix[j][n - 1 - i];
                    // right top = temp;
                    matrix[j][n - 1 - i] = temp;
                }
            }
        }

        public static int MaxArea(int[] height)
        {
            int result = 0;
            int left = 0, right = height.Length - 1;

            while (left < right)
            {
                int current = 0;
                if (height[left] < height[right])
                {
                    current = height[left] * (right - left);
                    if (current > result)
                    {
                        result = current;
                    }
                    ++left;
                }
                else
                {
                    current = height[right] * (right - left);
                    if (current > result)
                    {
                        result = current;
                    }
                    --right;
                }
            }

            return result;
        }

        public static bool IsSubsequence(string s, string t)
        {
            int i = 0;
            int len = s.Length;

            foreach (char item in t)
            {
                if (i >= len)
                {
                    return true;
                }

                if (s[i] == item)
                {
                    ++i;
                }
            }

            if (i >= len)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool WordPattern(string pattern, string s)
        {
            List<string> words = s.Split(" ").ToList();

            // Compare 2 sizes of list
            if (words.Count != pattern.Length)
            {
                return false;
            }

            // Split pattern to seperate chars
            char[] chars = pattern.ToCharArray();

            // Combine 2 lists into a list of pairs
            List<Tuple<char, string>> pairs = chars.Zip(words, (x, y) => new Tuple<char, string>(x, y)).ToList();

            // Convert to set of pair to remove duplicates
            HashSet<Tuple<char, string>> sets = new HashSet<Tuple<char, string>>(pairs);

            // Traverse each pair and check whether it is completely different from each other
            int count = sets.Count;
            bool isPatternDup = sets.Select(x => x.Item1).Distinct().Count() == count;
            bool isStringDup = sets.Select(x => x.Item2).Distinct().Count() == count;

            return isPatternDup && isStringDup;
        }

        public static void MoveZeroes(int[] nums)
        {
            int len = nums.Length;
            int current = 0;

            for (int i = 0; i < len; i++)
            {
                if (nums[i] != 0)
                {
                    nums[current] = nums[i];
                    if (current != i)
                    {
                        nums[i] = 0;
                    }
                    ++current;
                }
            }
        }

        public static void Merge(int[] nums1, int m, int[] nums2, int n)
        {
            int[] temp = new int[m];

            for (int i = 0; i < m; i++)
            {
                temp[i] = nums1[i];
            }

            int a = 0, b = 0;

            for (int i = 0; i < m + n; i++)
            {
                if (b >= n || a < m && temp[a] < nums2[b])
                {
                    nums1[i] = temp[a];
                    ++a;
                }
                else
                {
                    nums1[i] = nums2[b];
                    ++b;
                }
            }
        }

        public static string LongestPalindrome(string s)
        {
            s = string.Join('|', s.ToCharArray());

            int len = s.Length;
            string[] longestRadius = new string[len];
            int center = 0;

            while (center < len)
            {
                int radius = 0;

                while (center - (radius + 1) >= 0
                    && center + radius + 1 <= len - 1
                    && s[center - (radius + 1)] == s[center + radius + 1])
                {
                    radius++;
                }

                longestRadius[center] = string.Join("", s.Substring(center - radius, radius * 2 + 1).Split('|'));

                center++;
            }

            return longestRadius.OrderByDescending(x => x.Length).First();
        }

        private static bool IsPalindrome(string s)
        {
            if (s.Length % 2 == 0)
            {
                return s.Substring(0, s.Length / 2) == new string(s.Substring(s.Length / 2).Reverse().ToArray());
            }
            else
            {
                return s.Substring(0, s.Length / 2) == new string(s.Substring(s.Length / 2 + 1).Reverse().ToArray());
            }
        }

        public static bool IsPowerOfThree(int n)
        {
            if (n == 0)
            {
                return false;
            }

            while (n != 1)
            {
                if (n % 3 != 0)
                {
                    return false;
                }

                n /= 3;
            }

            return true;
        }

        public static IList<IList<int>> ThreeSum(int[] nums)
        {
            int len = nums.Length;

            if (len == 0)
            {
                return new List<IList<int>>();
            }

            HashSet<IList<int>> res = new HashSet<IList<int>>();

            for (int i = 0; i < nums.Length - 2; i++)
            {
                for (int j = i + 1; j < nums.Length - 1; j++)
                {
                    for (int k = 0; k < nums.Length; k++)
                    {
                        if (nums[i] + nums[j] + nums[k] == 0)
                        {
                            res.Add(new List<int>() { nums[i], nums[j], nums[k] });


                        }
                    }
                }
            }

            return new List<IList<int>>(res);
        }
    }
}
