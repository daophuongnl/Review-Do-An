using SingleData;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace Utility
{
    public static class StringUtil
    {
        public static bool IsInputValid(this string value)
        {
            if (value == null || value.Length == 0) return false;
            return true;
        }

        private static string CombineString(this IEnumerable<string> values, string connector = "\n")
        {
            var sb = new StringBuilder();

            int i = 0;
            foreach (var value in values)
            {
                if (i != 0)
                {
                    sb.Append($"{connector}");
                }
                sb.Append($"{value}");
                i++;
            }
            return sb.ToString();
        }

        public static string CombineString<T>(this IEnumerable<T> values, Func<T, string> getStringFunc = null, string connector = "\n")
        {
            if (values is IEnumerable<string> && getStringFunc == null)
            {
#pragma warning disable CS8604 // Possible null reference argument.
                return (values as IEnumerable<string>).CombineString(connector);
#pragma warning restore CS8604 // Possible null reference argument.
            }

            if (getStringFunc == null)
            {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
                getStringFunc = x => x.ToString();
#pragma warning restore CS8602 // Dereference of a possibly null reference.
            }

            var sb = new StringBuilder();

            int i = 0;
            foreach (var value in values)
            {
                if (i != 0)
                {
                    sb.Append($"{connector}");
                }
                sb.Append($"{getStringFunc(value)}");
                i++;
            }
            return sb.ToString();
        }

        public static string Capitalize(this string value)
        {
            if (value.Length == 0)
            {
                return value;
            }
            value = value.ToLower();
            return char.ToUpper(value[0]) + value.Substring(1);
        }

        public static string FormatLevelString(this string value, bool isCapitalizeFirstLetter = true)
        {
            value = value.Replace("TẦNG", "tầng").Replace("LEVEL", "tầng").Replace("BLOCK", "Block")
                .Replace("TRỆT", "trệt").Replace("KỸ THUẬT", "kỹ thuật").Replace("MÁI", "mái")
                .Replace("HẦM", "hầm").Replace("VỈA HÈ", "vỉa hè").Replace("THƯỢNG", "thượng").Replace("TUM", "tum");

            if (isCapitalizeFirstLetter)
            {
                value = char.ToUpper(value[0]) + value.Substring(1);
            }
            return value;
        }

        public static string GetExcelColumnName(this int i)
        {
            string column = string.Empty;

            if (i / 26m > 1)
            {
                int letter = (int)i / 26;
                column = ((char)(65 + letter - 1)).ToString();
                i -= letter * 26;
            }

            column += ((char)(65 + i - 1)).ToString();

            return column;
        }

        public static Thickness? GetThickness(this string marginString)
        {
            var numbers = new List<double>();
            foreach (var item in marginString.Split(' '))
            {
                double d = -1;
                if (!double.TryParse(item, out d))
                {
                    return null;
                }
                numbers.Add(d);
            }

            if (numbers.Count == 1)
            {
                return new Thickness(numbers[0]);
            }
            else if (numbers.Count == 4)
            {
                return new Thickness(numbers[0], numbers[1], numbers[2], numbers[3]);
            }
            return null;
        }

        public static string GetString(this Thickness margin)
        {
            return $"{margin.Left} {margin.Top} {margin.Right} {margin.Bottom}";
        }

        public static string GetRandomAlphanumericString(int length)
        {
            const string alphanumericCharacters =
                "ABCDEFGHIJKLMNOPQRSTUVWXYZ" +
                "abcdefghijklmnopqrstuvwxyz" +
                "0123456789";
            return GetRandomString(length, alphanumericCharacters);
        }

        public static string GetRandomString(int length, IEnumerable<char> characterSet)
        {
            if (length < 0)
                throw new ArgumentException("length must not be negative", "length");
            if (length > int.MaxValue / 8) // 250 million chars ought to be enough for anybody
                throw new ArgumentException("length is too big", "length");
            if (characterSet == null)
                throw new ArgumentNullException("characterSet");
            var characterArray = characterSet.Distinct().ToArray();
            if (characterArray.Length == 0)
                throw new ArgumentException("characterSet must not be empty", "characterSet");

            var bytes = new byte[length * 8];
            new RNGCryptoServiceProvider().GetBytes(bytes);
            var result = new char[length];
            for (int i = 0; i < length; i++)
            {
                ulong value = BitConverter.ToUInt64(bytes, i * 8);
                result[i] = characterArray[value % (uint)characterArray.Length];
            }
            return new string(result);
        }
    }
}
