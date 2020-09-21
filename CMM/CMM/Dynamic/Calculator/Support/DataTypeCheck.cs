namespace CMM.Dynamic.Calculator.Support
{
    using System;
    using System.Runtime.InteropServices;

    public class DataTypeCheck
    {
        public static string[] ArithOperators = new string[] { "^", "*", "/", "%", "+", "-" };
        public static string[] AssignmentOperators = new string[] { ":=" };
        public static string[] ComparisonOperators = new string[] { "<", "<=", ">", ">=", "<>", "=" };
        public static string[] LogicalOperators = new string[] { "and", "or" };
        public static string[] OperandFunctions = new string[] { 
            "avg", "abs", "iif", "lcase", "left", "len", "mid", "right", "round", "sqrt", "ucase", "isnullorempty", "istrueornull", "isfalseornull", "trim", "rtrim", 
            "ltrim", "dateadd", "concat", "date", "rpad", "lpad", "join", "searchstring", "day", "month", "year", "substring", "numericmax", "numericmin", "datemax", "datemin", 
            "stringmax", "stringmin", "contains", "between", "indexof", "now", "replace", "eval", "remove", "quote", "pcase", "sin", "cos", "not", "isalldigits"
         };
        public static string[] ReservedWords = new string[] { 
            "int", "long", "float", "decimal", "currency", "date", "value", "while", "for", "do", "break", "continue", "foreach", "next", "else", "end", 
            "endif", "string", "text", "char", "list", "rule", "expression", "function", "macro", "express", "int", "integer", "list", "sub", "set", ":="
         };

        public static bool AnyPuncuation(string Text, out char PuncMark)
        {
            PuncMark = ' ';
            foreach (char ch in Text)
            {
                if (char.IsPunctuation(ch))
                {
                    PuncMark = ch;
                    return true;
                }
            }
            return false;
        }

        public static bool ContainsOperator(string CheckString, out string sOperand, out string sOperator)
        {
            int num;
            sOperand = "";
            sOperator = "";
            if (string.IsNullOrEmpty(CheckString))
            {
                return false;
            }
            CheckString = CheckString.Trim();
            bool flag = false;
            for (num = 0; num < ArithOperators.Length; num++)
            {
                if (CheckString.EndsWith(ArithOperators[num]))
                {
                    flag = true;
                    sOperator = ArithOperators[num];
                    sOperand = CheckString.Substring(0, CheckString.Length - sOperator.Length);
                    break;
                }
            }
            if (flag)
            {
                return true;
            }
            for (num = 0; num < ComparisonOperators.Length; num++)
            {
                if (CheckString.EndsWith(ComparisonOperators[num]))
                {
                    flag = true;
                    sOperator = ComparisonOperators[num];
                    sOperand = CheckString.Substring(0, CheckString.Length - sOperator.Length);
                    break;
                }
            }
            return flag;
        }

        public static int DecimalCount(string CheckString)
        {
            if (string.IsNullOrEmpty(CheckString))
            {
                return 0;
            }
            CheckString = CheckString.Trim();
            int num = 0;
            foreach (char ch in CheckString)
            {
                if (ch == '.')
                {
                    num++;
                }
            }
            return num;
        }

        public static void FunctionDescription(string FunctionName, out string Syntax, out string Description, out string Example)
        {
            Syntax = "Unknown function";
            Description = "";
            Example = "";
            switch (FunctionName.Trim().ToLower())
            {
                case "cos":
                    Description = "Calculates the cosine of a number.";
                    Syntax = "cos[p1] where p1 can be converted to doubles.";
                    Example = "cos[90] < 0";
                    break;

                case "avg":
                    Description = "Calculates the average of a list of numbers.  The list items must be able to convert to doubles.";
                    Syntax = "avg[p1, ..., pn] where p1,...,pn can be converted to doubles.";
                    Example = "avg[1, 2, 3] = 2";
                    break;

                case "abs":
                    Description = "Calculates the absolute value of a numeric parameter.";
                    Syntax = "abs[p1] where p1 can be converted to a double.";
                    Example = "abs[-10] = 10";
                    break;

                case "iif":
                    Description = "Performs an if-else-end";
                    Syntax = "iif[c, a, b] where c is the condition and must evaluate to a boolean.  The value a is returned if c is true, otherwise, the value b is returned.";
                    Example = "iif[year[now[]] = 2008, \"2008\", \"Not 2008\"]";
                    break;

                case "lcase":
                    Description = "Converts a string to lower case";
                    Syntax = "LCase[a]";
                    Example = "LCase[\"TEST\"] = \"test\"";
                    break;

                case "left":
                    Description = "Returns the left number of characters from a string parameter.";
                    Syntax = "left[s, n] where s is the string and n is the number of characters";
                    Example = "left[\"abcd\", 2] = \"ab\"";
                    break;

                case "len":
                    Description = "Returns te length of a string";
                    Syntax = "len[a] where a is a string variable";
                    Example = "len[\"test\"] = 4";
                    break;

                case "mid":
                    Description = "Calculates the median for a list of numbers";
                    Syntax = "mid[p1, ..., pn] where p1, ..., pn are numberic values";
                    Example = "mid[1, 4, 100] = 4";
                    break;

                case "right":
                    Description = "Returns the right number of characters from a string parameter";
                    Syntax = "right[s, n] where s is the string and n is the number of characters.";
                    Example = "right[\"abcd\", 2] = \"cd\"";
                    break;

                case "round":
                    Description = "Rounds a numeric value to the number of decimal places";
                    Syntax = "round[n, d] where n is the numberic value to be rounded, and d is the number of decimal places.";
                    Example = "round[123.45, 0] = 123";
                    break;

                case "sqrt":
                    Description = "Calculates the square root of a number.";
                    Syntax = "sqrt[a] where a is a numberic parameter";
                    Example = "sqrt[25] = 5";
                    break;

                case "ucase":
                    Description = "Converts a string to upper case";
                    Syntax = "ucase[a]";
                    Example = "ucase[\"test\"] = \"TEST\"";
                    break;

                case "isnullorempty":
                    Description = "Indicates is the parameter is null or empty.";
                    Syntax = "IsNullOrEmpty[a]";
                    Example = "";
                    break;

                case "istrueornull":
                    Description = "Indicates if the parameter has the value true or is null;";
                    Syntax = "isTrueorNull[a]";
                    Example = "";
                    break;

                case "isfalseornull":
                    Description = "Indicates if the parameter has the value false or is null";
                    Syntax = "IsFalseOrNull[a]";
                    Example = "IIF[IsFalseOrNull[a], \"false\", \"not false\"]";
                    break;

                case "trim":
                    Description = "Trims the spaces from the entire string";
                    Syntax = "trim[a]";
                    Example = "";
                    break;

                case "rtrim":
                    Description = "Trims the spaces from the right of a string";
                    Syntax = "rtrim[a]";
                    Example = "";
                    break;

                case "ltrim":
                    Description = "Trims the spaces from the left of a string";
                    Syntax = "ltrim[a]";
                    Example = "";
                    break;

                case "dateadd":
                    Description = "Adds an amount to a date.  Please note that the amount may be negative.";
                    Syntax = "dateadd[date, \"type\", amount] where date is a valid date, and type is \"y\", \"m\", \"d\" or \"b\" (representing year, month, day, or business days) and amount is an integer";
                    Example = "dateadd[now[], \"b\", 5]";
                    break;

                case "concat":
                    Description = "This operand function concatenates the parameters together to make a string.";
                    Syntax = "concat[p1, ..., pn]";
                    Example = "concat[\"This\", \" \", \"is\", \" \", \"a\", \" \", \"test\"] = \"This is a test\"";
                    break;

                case "date":
                    Description = "Create a new date data type";
                    Syntax = "date[m, d, y] where m is an integer and is the month, d is an integer and is the day, and y is an integer and is the year";
                    Example = "date[3, 20, 2007]";
                    break;

                case "rpad":
                    Description = "Pads a string on the right with new values";
                    Syntax = "rpad[a, b, n]  where a and b are string values and n is numeric.  The parameter p will be appended to the right of parameter a, n times.";
                    Example = "rpad[\"test\", \".\", 10] = \"test..........\"";
                    break;

                case "lpad":
                    Description = "Pads a string on the left with new values";
                    Syntax = "lpad[a, b, n]  where a and b are string values and n is numeric.  The parameter p will be appended to the left of parameter a, n times.";
                    Example = "lpad[\"test\", \".\", 10] = \"..........test\"";
                    break;

                case "join":
                    Description = "Joins a list of items together using a delimiter";
                    Syntax = "join[a, b1, ..., bn] where a is the delimiter and b1, ..., bn are the items to be joined.";
                    Example = "join[\" \", \"This\", \"is\", \"a\", \"test\"] = \"This is a test\"";
                    break;

                case "searchstring":
                    Description = "Searches for a string within another string at a specified starting position";
                    Syntax = "SearchString[a, n, b] where a is the string that is being searched, b is the string that is being sought, and n is the start position in a";
                    Example = "SearchString[\"abcdefghijk\", 0, \"efg\"] = 4";
                    break;

                case "day":
                    Description = "Returns the day of a date";
                    Syntax = "day[d1] where d1 is a date value";
                    Example = "day[3.20.1999] = 20";
                    break;

                case "month":
                    Description = "Returns the month of a date";
                    Syntax = "month[d1] where d1 is a date value";
                    Example = "month[now[]] returns the current month";
                    break;

                case "year":
                    Description = "Returns the year of a date";
                    Syntax = "year[d1] where d1 is a date";
                    Example = "year[now[]] returns the current year";
                    break;

                case "substring":
                    Description = "Extracts a substring from a string";
                    Syntax = "SubString[s, a, b] where s is the string, a is the starting point, and b is the number of characters extracted.";
                    Example = "substring[\"abcdefghijk\", 3, 5] = \"defgh\"";
                    break;

                case "numericmax":
                    Description = "Finds the maximum numeric value in a list";
                    Syntax = "NumericMax[p1, ..., pn]";
                    Example = "";
                    break;

                case "numericmin":
                    Description = "Finds the numeric minimum value in a list";
                    Syntax = "NumericMin[p1, ..., pn]";
                    Example = "";
                    break;

                case "datemax":
                    Description = "Returns the maximum date in the list";
                    Syntax = "datemax[d1, ..., dn] where d1, ..., dn are dates";
                    Example = "datemax[ 3.20.1999, 3.20.2005, 3.20.2008] = 3.20.2008";
                    break;

                case "datemin":
                    Description = "Returns the minimum date in the list.";
                    Syntax = "datemin[d1, ..., dn] where d1, ..., dn are dates.";
                    Example = "datemax[ 3.20.1999, 3.20.2005, 3.20.2008] = 3.20.1999";
                    break;

                case "stringmax":
                    Description = "Finds the maximum string in the list";
                    Syntax = "StringMax[p1, ..., pn]";
                    Example = "StringMax[\"Apple\", \"Zebra\"] = \"Zebra\"";
                    break;

                case "stringmin":
                    Description = "Finds the minimum string in the list";
                    Syntax = "StringMax[p1, ..., pn]";
                    Example = "StringMax[\"Apple\", \"Zebra\"] = \"Apple\"";
                    break;

                case "contains":
                    Description = "Indicates if the item is contained in the list.";
                    Syntax = "contains[p1, p2, ...., pn]   If p1 in in the list p2, ..., pn, this function returns \"true\" otherwise, this function returns \"false\".";
                    Example = "contains[state, \"NY\", \"WA\", \"CA\"] = true";
                    break;

                case "between":
                    Description = "Indicates if a value is between the other values.  Please note that the comparison is inclusive.";
                    Syntax = "between[var, val1, val2] where var, val1, and val2 are integers.  if var >= val1 and var <= val2 then the function returns \"true\", otherwise, the function return \"false\".";
                    Example = "between[fico, 400, 700]   Note that fico is a variable.";
                    break;

                case "indexof":
                    Description = "Returns the index of a list item.";
                    Syntax = "indexof[a, b1, ..., bn]  If the list b1, ..., bn contains the value a, the index of the value is returned, otherwise, -1 is returned.  Pleaes note that this is zero based indexing";
                    Example = "iif[indexof[a, \"CA\", \"NY\", \"WA\"] >= 0, \"found state\", \"not found\"]";
                    break;

                case "now":
                    Description = "Returns the current date";
                    Syntax = "now[]  This operand function takes no parameters";
                    Example = "year[now[]] = 2008";
                    break;

                case "replace":
                    Description = "Replaces one string with another string";
                    Syntax = "Replace[a, b, c] where a is the search string, b is the value being replaced, and c is the value that is being inserted";
                    Example = "replace[\"3.20.2008\", \".\", \"-\"] = \"3-20-2008\"";
                    break;

                case "eval":
                    Description = "Evaluates a string rule";
                    Syntax = "eval[r] where r is any valid rule";
                    Example = "eval[concat[\"1\", \"+\", \"2\"]] = 3";
                    break;

                case "remove":
                    Description = "Removes the specified characters from the string";
                    Syntax = "remove[a, b] where a and b are string";
                    Example = "InsertOnSubmit[\"....this...is..a...test...\", \".a\"] = \"thisistest\"";
                    break;

                case "quote":
                    Description = "Returns a double quote";
                    Syntax = "quote[]";
                    Example = "quote[] = \"";
                    break;

                case "pcase":
                    Description = "Converts a string to Proper Case";
                    Syntax = "pcase[a] where a is a string";
                    Example = "join[\" \", pcase[\"dave\"], pcase[\"SMITH\"] ] = \"Dave Smith\"";
                    break;

                case "sin":
                    Description = "Calcuates the sin of a number";
                    Syntax = "sin[a]";
                    Example = "sin[45] = 0.85";
                    break;

                case "isalldigits":
                    Description = "Determine if the parameter contains all digits.";
                    Syntax = "isalldigits[p1] where p1 is a string parameter";
                    Example = "isalldigits[\"12345\"] = true";
                    break;

                case "not":
                    Description = "Performs a NOT on a boolean parameter.";
                    Syntax = "not[p1] where p1 is a boolean parameter";
                    Example = "not[5<10]=false";
                    break;
            }
        }

        public static bool IsAllDigits(string CheckString)
        {
            if (string.IsNullOrEmpty(CheckString))
            {
                return false;
            }
            CheckString = CheckString.Trim();
            foreach (char ch in CheckString)
            {
                if (!char.IsDigit(ch))
                {
                    return false;
                }
            }
            return true;
        }

        public static bool IsBoolean(string CheckString)
        {
            if (string.IsNullOrEmpty(CheckString))
            {
                return false;
            }
            CheckString = CheckString.Trim().ToLower();
            return ((CheckString == "true") || (CheckString == "false"));
        }

        public static bool IsDate(string CheckString)
        {
            if (string.IsNullOrEmpty(CheckString))
            {
                return false;
            }
            CheckString = CheckString.Trim();
            DateTime minValue = DateTime.MinValue;
            return DateTime.TryParse(CheckString, out minValue);
        }

        public static bool IsDouble(string CheckString)
        {
            if (string.IsNullOrEmpty(CheckString))
            {
                return false;
            }
            CheckString = CheckString.Trim();
            bool flag = true;
            int num = 0;
            int num2 = 0;
            foreach (char ch in CheckString)
            {
                if (!char.IsNumber(ch))
                {
                    if (ch == '.')
                    {
                        num++;
                    }
                    else if ((ch != '-') || (num2 != 0))
                    {
                        flag = false;
                        break;
                    }
                }
                num2++;
            }
            return (flag && (num <= 1));
        }

        public static bool IsInteger(string CheckString)
        {
            if (string.IsNullOrEmpty(CheckString))
            {
                return false;
            }
            CheckString = CheckString.Trim();
            int num = 0;
            foreach (char ch in CheckString)
            {
                if (!char.IsNumber(ch))
                {
                    if (ch != '-')
                    {
                        return false;
                    }
                    if (num != 0)
                    {
                        return false;
                    }
                }
                num++;
            }
            return true;
        }

        public static bool IsNULL(string CheckString)
        {
            if (string.IsNullOrEmpty(CheckString))
            {
                return false;
            }
            CheckString = CheckString.Trim().ToLower();
            return (CheckString == "null");
        }

        public static bool IsOperandFunction(string OperandText)
        {
            if (string.IsNullOrEmpty(OperandText))
            {
                return false;
            }
            OperandText = OperandText.Trim().ToLower();
            for (int i = 0; i < OperandFunctions.Length; i++)
            {
                if (OperandText == OperandFunctions[i])
                {
                    return true;
                }
            }
            return false;
        }

        public static bool IsOperator(string OperatorText)
        {
            int num;
            if (string.IsNullOrEmpty(OperatorText))
            {
                return false;
            }
            OperatorText = OperatorText.Trim().ToLower();
            bool flag = false;
            for (num = 0; num < ArithOperators.Length; num++)
            {
                if (OperatorText == ArithOperators[num])
                {
                    flag = true;
                    break;
                }
            }
            if (flag)
            {
                return flag;
            }
            for (num = 0; num < LogicalOperators.Length; num++)
            {
                if (OperatorText == LogicalOperators[num])
                {
                    flag = true;
                    break;
                }
            }
            if (flag)
            {
                return flag;
            }
            for (num = 0; num < ComparisonOperators.Length; num++)
            {
                if (OperatorText == ComparisonOperators[num])
                {
                    flag = true;
                    break;
                }
            }
            if (flag)
            {
                return flag;
            }
            for (num = 0; num < AssignmentOperators.Length; num++)
            {
                if (OperatorText == AssignmentOperators[num])
                {
                    flag = true;
                    break;
                }
            }
            return (flag && flag);
        }

        public static bool IsReservedWord(string OperandText)
        {
            if (string.IsNullOrEmpty(OperandText))
            {
                return false;
            }
            OperandText = OperandText.Trim().ToLower();
            if (string.IsNullOrEmpty(OperandText))
            {
                return false;
            }
            for (int i = 0; i < ReservedWords.Length; i++)
            {
                if (OperandText == ReservedWords[i])
                {
                    return true;
                }
            }
            return false;
        }

        public static bool IsText(string CheckString)
        {
            if (string.IsNullOrEmpty(CheckString))
            {
                return false;
            }
            CheckString = CheckString.Trim();
            if (CheckString.Length == 1)
            {
                return false;
            }
            if (!CheckString.StartsWith("\""))
            {
                return false;
            }
            if (!CheckString.EndsWith("\""))
            {
                return false;
            }
            return true;
        }

        public static string RemoveTextQuotes(string CheckString)
        {
            if (!IsText(CheckString))
            {
                return CheckString;
            }
            return CheckString.Substring(1, CheckString.Length - 2);
        }
    }
}

