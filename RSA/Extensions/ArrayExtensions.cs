using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace RSA.Extensions
{
    public static class ArrayExtensions
    {
        public static string ToTextLines(this uint[] data)
        {
            StringBuilder builder = new StringBuilder();

            for (int i = 0; i < data.Length; i++)
            {
                builder.AppendLine(data[i].ToString());
            }

            return builder.ToString();
        }

        public static uint[] FromTextLines(this string path)
        {
            List<uint> values = new List<uint>();
            foreach (var line in File.ReadAllLines(path))
            {
                values.Add(Convert.ToUInt32(line));
            }

            return values.ToArray();
        }
    }
}
