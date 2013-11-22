using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace fopro.utils
{
    class TextFormatting
    {
        #region IRC Colors
        public static string Bold(string input)
        {
            return ((char)2 + input + (char)2);
        }

        public static string ColorWhite(string input)
        {
            return ((char)3 + "0" + input + (char)3 + "0");
        }

        public static string ColorBlack(string input)
        {
            return ((char)3 + "1" + input + (char)3 + "1");
        }

        public static string ColorBlue(string input)
        {
            return ((char)3 + "2" + input + (char)3 + "2");
        }

        public static string ColorGreen(string input)
        {
            return ((char)3 + "3" + input + (char)3 + "3");
        }

        public static string ColorRed(string input)
        {
            return ((char)3 + "4" + input + (char)3 + "4");
        }

        public static string ColorBrown(string input)
        {
            return ((char)3 + "5" + input + (char)3 + "5");
        }

        public static string ColorPurple(string input)
        {
            return ((char)3 + "6" + input + (char)3 + "6");
        }

        public static string ColorOrange(string input)
        {
            return ((char)3 + "7" + input + (char)3 + "7");
        }

        public static string ColorYellow(string input)
        {
            return ((char)3 + "8" + input + (char)3 + "8");
        }

        public static string ColorLightGreen(string input)
        {
            return ((char)3 + "9" + input + (char)3 + "9");
        }

        public static string ColorTeal(string input)
        {
            return ((char)3 + "10" + input + (char)3 + "10");
        }

        public static string ColorCyan(string input)
        {
            return ((char)3 + "11" + input + (char)3 + "11");
        }

        public static string ColorLightBlue(string input)
        {
            return ((char)3 + "12" + input + (char)3 + "12");
        }

        public static string ColorPink(string input)
        {
            return ((char)3 + "13" + input + (char)3 + "13");
        }

        public static string ColorGrey(string input)
        {
            return ((char)3 + "14" + input + (char)3 + "14");
        }

        public static string ColorSilver(string input)
        {
            return ((char)3 + "15" + input + (char)3 + "15");
        }
        #endregion
    }
}
