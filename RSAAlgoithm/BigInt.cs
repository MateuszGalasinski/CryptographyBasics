using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace BigIntTest
{
    class BigInt
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
                    result[i] += 1;

                if (j < second.Length)
                    tmpResult = first[i] + second[j];
                else
                {
                    tmpResult = first[i];
                }

                if (tmpResult >= 9) //&& flag == 0)
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

        public static int[] Subtract(int[] first, int[] second)
        {
            if (first.Length < second.Length)
                return new int[] { -1 };

            int flag = 0;
            int[] result = new int[first.Length];

            int j = 0;

            for (int i = 0; i < first.Length; i++)
            {
                if (flag == -1)
                    result[i] -= 1;

                int tmpResult;
                if (j < second.Length)
                    tmpResult = first[i] - second[j];
                else
                    tmpResult = first[i];

                if (tmpResult < 0) //&& flag == 0)
                {
                    flag = -1;
                    result[i] += first[i] + 10 - second[j];
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

            return result;

        }

        //public static int[][] DivideWithRemainder(int[] first, int[] second)
        //{
        //    //if (first.Length < second.Length)
        //    //    return new int[] { -1 };

        //    int[] result = new int[first.Length];
        //    int[] previousStep;

        //    do
        //    {
        //        int[] step = Subtract(first, second);
        //        if (step[0] == -1)
        //            break;




        //    } while (true);

        //}
    }
}
