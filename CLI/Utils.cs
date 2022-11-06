namespace CLI
{
    partial class MainApp
    {
        /// <summary>
        /// Takes a string array and joins them into one
        /// </summary>
        /// <param name="data">string array containing values to join</param>
        /// <param name="join">separator to use to join strings</param>
        /// <returns>a new joined string</returns>
        public static string Join(string[] data, string join)
        {
            string result = data[0];
            for (int i = 1; i < data.Length; i++)
            {
                result += join + data[i];
            }
            return result;
        }

        /// <summary>
        /// Returns whether the given set contains at least one target
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="target"></param>
        /// <param name="set"></param>
        /// <returns></returns>
        public static bool In<T>(T target, T[] set)
        {
            foreach (T item in set)
                if (item.Equals(target)) return true;

            return false;
        }

        /// <summary>
        /// Returns a centered string
        /// </summary>
        /// <param name="data"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string Center(string data, int length)
        {
            if (data.Length == 0) return StringMult(" ", length);
            if (length <= 0) return "";

            int diff = length - data.Length;
            int offset = diff / 2;

            if (diff % 2 == 0) return StringMult(" ", offset) + data + StringMult(" ", offset);
            else return StringMult(" ", offset + 1) + data + StringMult(" ", offset);
        }

        /// <summary>
        /// Returns a pattern which has been multiplied by a certain factor
        /// </summary>
        /// <example>
        /// StringMult("*", 5) == "*****";
        /// StringMult("abc", 3) == "abcabcabc";
        /// </example>
        /// <param name="pattern"></param>
        /// <param name="factor"></param>
        /// <returns></returns>
        public static string StringMult(string pattern, int factor)
        {
            string result = "";
            for (int i = 0; i < factor; i++)
            {
                result += pattern;
            }
            return result;
        }
    }
}