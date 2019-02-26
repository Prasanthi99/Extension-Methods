using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Mail;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Extensions
{
    public static class  StringExtensions
    {
        public static string SwapCase(this string input)
        {
            return new string(input.Select (c => char.IsLetter(c) ? (char.IsUpper(c) ?
                                          char.ToLower(c) : char.ToUpper(c)) : c).ToArray());
        }

        public static bool IsPalindrome(this string input)
        {
            var reverse = new string(input.ToCharArray().Reverse().ToArray());
            return input.Equals(reverse);
        }

        public static bool IsEmail(this string input)
        {
            if (String.IsNullOrEmpty(input.Trim()))
                return false;

            MailAddress address = new MailAddress(input);
            return true;
            
        }

        public static int GetWordCount(this string input)
        {
            if (input.Trim().Length == 0)
                return 0;

            return Regex.Split(input, @"\W+").Length;
        }

        public static int GetOccurrenceCount(this string input, string searchText)
        {
            if (input.Trim() == string.Empty && searchText.Trim() == string.Empty)
                return 1;
            else if (input.Trim() == string.Empty || searchText.Trim() == string.Empty)
                return 0;
            else
                return Regex.Matches(input, searchText).Count;
        }

        public static List<KeyValuePair<string,string>> GetObjectInfo(this object input)
        {
            List<KeyValuePair<string, string>> result = new List<KeyValuePair<string, string>>();
            Type type = input.GetType();
            object obj = Activator.CreateInstance(type);

            PropertyInfo[] propertyInfo = type.GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

            foreach (PropertyInfo property in propertyInfo)
            {
                var attrribute = property.GetCustomAttribute(typeof(DisplayNameAttribute)) as DisplayNameAttribute;
                if (attrribute != null)
                {
                    result.Add(new KeyValuePair<string, string>(attrribute.DisplayName, Convert.ToString(property?.GetValue(input, null))));
                }
            }

            return result;
        }

        public static object GetPropValue(object source, string propName)
        {
            return source.GetType().GetProperty(propName).GetValue(source, null);
        }

        public static List<string> GetValues(this object input)
        {
            List<string> result = new List<string>();
            Type type = input.GetType();
            object obj = Activator.CreateInstance(type);
            PropertyInfo[] propertyInfo = type.GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            foreach (PropertyInfo property in propertyInfo)
                 result.Add(Convert.ToString(property?.GetValue(input, null)));
            return result;
        }
    }
}
