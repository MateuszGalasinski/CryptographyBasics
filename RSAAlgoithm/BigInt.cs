using System;
using System.Collections.Generic;
using System.Linq;

namespace RSAAlgoithm
{
    public class BigInt
    {
        private int[] value;

        public BigInt(int[] value)
        {
            Array.Copy(value, this.value, value.Length);
        }

        public int[] Value
        {
            get => value;
        }

        //lower index lower power
        public static int[] Add(int[] firstArg, int[] secondArg)
        {
            int[] second;
            int[] first;
            int[] result;
            int flag = 0;
            int tmpResult;

            if (firstArg.Length < secondArg.Length)
            {
               second = firstArg;
               first = secondArg;
               
            }
            else
            {
                first = firstArg;
                second = secondArg;
            }

            result = new int[first.Length + 1];

            int j = 0;
            for(int i =0; i < first.Length; i++)
            {
                if (j < second.Length)
                {
                    tmpResult = first[i] + second[j];
                    j++;
                }     
                else
                    tmpResult = first[i];

                if (flag > 0)
                {
                    tmpResult += flag;

                    if(tmpResult >= 10)
                    {
                        flag = 1;
                        result[i] = tmpResult % 10;
                    }
                    else
                    {
                        flag = 0;
                        result[i] = tmpResult;
                    }
                }
                else
                {
                    if (tmpResult >= 10)
                    {
                        flag = 1;
                        result[i] = tmpResult % 10;
                    }
                    else
                    {
                        flag = 0;
                        result[i] = tmpResult;
                    }
                }
            }

            if (flag == 1)
            {
                result[first.Length] = 1;
                return result;
            }
            else
            {
                int[] trimmedResult = new int[first.Length];
                Array.Copy(result, trimmedResult, trimmedResult.Length);
                return trimmedResult;
            }
        }

        public static int[] Substract(int[] greater, int[] smaller)
        {
            int[] result = new int[greater.Length];
            int reverseIndex = 1;
            int carry = 0;
            int currentDigit;

            // subtract shorter part
            while (reverseIndex <= smaller.Length)
            {
                currentDigit = greater[greater.Length - reverseIndex] - smaller[smaller.Length - reverseIndex] - carry;
                carry = currentDigit < 0 ? 1 : 0;
                currentDigit = currentDigit + (carry == 1 ? 10 : 0);
                result[greater.Length - reverseIndex] = currentDigit;
                reverseIndex++;
            }

            // carry rippling
            while (reverseIndex <= greater.Length)
            {
                currentDigit = greater[greater.Length - reverseIndex] - carry;
                carry = currentDigit < 0 ? 1 : 0;
                currentDigit = currentDigit + (carry == 1 ? 10 : 0);
                result[greater.Length - reverseIndex] = currentDigit;
                reverseIndex++;
            }

            // 3. //find index of first non-zero digit
            int i;
            for (i = 0; i < result.Length - 1 && result[i] == 0;)
            {
                i++;
            }

            int[] trimmedResult = new int[result.Length - i];
            Array.Copy(result, i, trimmedResult, 0, trimmedResult.Length);
            return trimmedResult;
        }

        public static int[] Multiply(int[] first, int[] second)
        {
            int flag = 0;
            int tmpNumber = 0;
            List<int> firstList = first.ToList();
            List<int> secondList = second.ToList();
            List<int> tmpList = new List<int>();
            List<int> resultList = new List<int>();
            resultList.Add(0);

            for (int i = 0; i < firstList.Count; i++)
            {
                flag = 0;
                for (int j = 0; j < i; j++)
                {
                    tmpList.Add(0);
                }

                for (int j = 0; j < secondList.Count; j++)
                {
                    tmpNumber = firstList[i] * secondList[j] + flag;
                    flag = tmpNumber / 10;
                    tmpNumber = tmpNumber % 10;
                    tmpList.Add(tmpNumber);

                }

                if (flag != 0)
                {
                    tmpList.Add(flag);
                }

                if (tmpList.Count > resultList.Count)
                {
                    resultList = Add(tmpList.ToArray(), resultList.ToArray()).ToList();
                }
                else
                {
                    resultList = Add(resultList.ToArray(), tmpList.ToArray()).ToList();
                }
                tmpList.Clear();

            }
            return resultList.ToArray();

        }
        //public static int[] Subtract(int[] first, int[] second)
        //{
        //    if (first.Length < second.Length)
        //        return new int[] { -1 };

        //    int flag = 0;
        //    int[] result = new int[first.Length];

        //    int j = 0;

        //    for (int i = 0; i < first.Length; i++)
        //    {
        //        if (flag == -1)
        //            result[i] -= 1;

        //        int tmpResult;
        //        if (j < second.Length)
        //            tmpResult = first[i] - second[j];
        //        else
        //            tmpResult = first[i];

        //        if (tmpResult < 0) //&& flag == 0)
        //        {
        //            flag = -1;
        //            if (j < second.Length)
        //                result[i] += 9 - Math.Abs(tmpResult); //first[i] + 10 - second[j];
        //            if (result[i] < 0)
        //                flag += -1;


        //        }
        //        else  //if(tmpResult > 9 && flag == 1)
        //        {
        //            flag = 0;
        //            result[i] += tmpResult;
        //        }
        //        j++;
        //    }

        //    int zeros = 0;
        //     j = result.Length - 1;
        //    while (result[j] == 0)
        //    {
        //        j--;
        //        zeros++;
        //    }

        //    if (zeros != 0)
        //    {
        //        int newLength = result.Length - zeros;
        //        int[] newResult = new int[result.Length - zeros];

        //        for (int k = 0; k < newLength; k++)
        //        {
        //            newResult[k] = result[k];
        //        }

        //        return newResult;

        //    }
        //    else
        //    {
        //        return result;
        //    }

        //}

       

        public static int[] Mod(int[] first, int[] second)
        {
            if (first.Length < second.Length)
                return first;

            int[] previousStep = new int[first.Length];
            Array.Copy(first, previousStep, first.Length);

            int comparison;

            while ((comparison = Compare(previousStep, second)) != -1)
            {
                if (comparison == 0)
                    return new[] {0};
                if (comparison == 1)
                {
                    previousStep = Substract(previousStep, second);
                }
            }

            return previousStep;
        }

        public static int Compare(int[] first, int[] second)
        {
            if (first.Length > second.Length)
                return 1;
            if (first.Length < second.Length)
                return -1;
            int i = first.Length;
            do
            {
                i--;
                if (first[i] > second[i])
                    return 1;
                if (first[i] < second[i])
                    return -1;
            } while (i != 0);

            return 0;
        }
    }
}