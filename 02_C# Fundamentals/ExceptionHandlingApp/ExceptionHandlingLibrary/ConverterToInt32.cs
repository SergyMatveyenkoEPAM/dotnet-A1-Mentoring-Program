using System;

namespace ExceptionHandlingLibrary
{
    public class ConverterToInt32
    {
        public static int GetInt32(string number)
        {
            try
            {
                return Convert.ToInt32(number);
            }
            catch (FormatException e)
            {
                throw new ConvertException("Input string is not a sequence of digits.");
            }
            catch (OverflowException e)
            {
                throw new ConvertException("The number cannot fit in an Int32.");
            }
        }
    }
}
