using System;
using System.Collections.Generic;
using System.Linq;

namespace RSAAlgoithm
{
    public class BigInt
    {
        public int[] Value { get => value; }
        private int[] value;

        public BigInt(int[] value)
        {
            Array.Copy(value, this.value, value.Length);
        }

        //lower index lower power
        public static int[] Add(int[] first, int[] second)
        {

            if (first.Length < second.Length)
                return new int[] { -1 };
            int flag = 0;
            int[] result;
            if (first[second.Length - 1] + second[second.Length - 1] < 9)
            {
                if (first[first.Length - 1] != 9)
                    result = new int[first.Length];
                else
                    result = new int[first.Length + 1];
            }
            else
            {
                result = new int[first.Length + 1];
            }

            int j = 0;

            for (int i = 0; i < first.Length; i++)
            {
                int tmpResult;
                if (flag == 1)
                {
                    result[i] += 1;
                    tmpResult = 1;
                }
                else
                {
                    tmpResult = 0;
                }
                    

                if (j < second.Length)
                    tmpResult = first[i] + second[j];
                else
                {
                    tmpResult = first[i];
                }

                if (tmpResult > 9) //&& flag == 0)
                {
                    flag = 1;
                    result[i] += tmpResult - 10;
                }
                else  //if(tmpResult > 9 && flag == 1)
                {
                    flag = 0;
                    result[i] += tmpResult;
                }
                j++;
            }

            if (flag == 1)
            {
                result[result.Length - 1] = 1;
            }

            if (result[result.Length - 1] == 0)
            {
                int[] secondResult = new int[result.Length - 1];
                Array.Copy(result, secondResult, result.Length - 1);
                return secondResult;
            }

            return result;

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
                currentDigit = greater[greater.Length - reverseIndex] - (smaller[smaller.Length - reverseIndex]) - carry;
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
                for (int j = 0; j < secondList.Count; j++)
                {
                    tmpNumber = firstList[i] * secondList[j] + flag;
                    flag = tmpNumber / 10;
                    tmpNumber = tmpNumber % 10;
                    tmpList.Add(tmpNumber);
                }

                resultList = Add(tmpList.ToArray(), resultList.ToArray()).ToList();

            }
            return resultList.ToArray();

        }

        public static int[] Mod(int[] first, int[] second)
        {
            if (first.Length < second.Length)
                return new int[] { -1 };

            int[] previousStep = new int[first.Length];
            Array.Copy(first, previousStep, first.Length);

            int comparison;

            while ((comparison = Compare(previousStep, second)) != -1)
            {
                if(comparison == 0)
                    return new int[] { 0 };
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
            else if (first.Length < second.Length)
                return -1;
            else
            {
                int i = first.Length;
                do
                {
                    i--;
                    if (first[i] > second[i])
                        return 1;
                    else if (first[i] < second[i])
                        return -1;
                    
                }while(i != 0);

                return 0;

            }
        }



    }
}
