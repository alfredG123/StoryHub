namespace Web.Miscellaneous
{
    public static class StringMethods
    {
        #region Parse
        /// <summary>
        /// Parse the given text as a float
        /// </summary>
        /// <param name="value_text"></param>
        /// <returns></returns>
        public static float ParseTextAsFloat(string? value_text)
        {
            // Parse the specified text
            // If the text cannot be parse, default the value as -1
            if (!(float.TryParse(value_text, out float value)))
            {
                value = -1;
            }

            return (value);
        }

        /// <summary>
        /// Parse the given text as an integer
        /// </summary>
        /// <param name="value_text"></param>
        /// <returns></returns>
        public static int ParseTextAsInt(string? value_text)
        {
            // Parse the specified text
            // If the text cannot be parse, default the value as -1
            if (!(int.TryParse(value_text, out int value)))
            {
                value = -1;
            }

            return (value);
        }
        #endregion
    }
}
