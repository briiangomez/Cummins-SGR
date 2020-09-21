namespace CMM.Dynamic.Calculator.Evaluate
{
    using CMM.Dynamic.Calculator.Parser;
    using CMM.Dynamic.Calculator.Support;
    using System;
    using System.Diagnostics;
    using System.Runtime.InteropServices;
    using System.Text;

    public class Evaluator
    {
        private Token token;
        private double tokenEvalTime = 0.0;

        public Evaluator(Token Tokens)
        {
            this.token = Tokens;
        }

        private bool Abs(TokenItems Parameters, out TokenItem Result, out string ErrorMsg)
        {
            ErrorMsg = "";
            Result = null;
            if (Parameters.Count != 1)
            {
                ErrorMsg = "Error in operand function Abs[]: Operand Function requires 1 parameter.";
                return false;
            }
            if (DataTypeCheck.IsDouble(Parameters[0].TokenName))
            {
                double num2 = Math.Abs(Parameters[0].TokenName_Double);
                Result = new TokenItem(num2.ToString(), CMM.Dynamic.Calculator.Parser.TokenType.Token_Operand, TokenDataType.Token_DataType_Double, false);
            }
            else
            {
                ErrorMsg = "Error in operand function Abs[]: Operand Function can only evaluate parameters that can be converted to a double.";
                return false;
            }
            return true;
        }

        private bool Avg(TokenItems Parameters, out TokenItem Result, out string ErrorMsg)
        {
            Exception exception;
            ErrorMsg = "";
            Result = null;
            if (Parameters.Count == 0)
            {
                ErrorMsg = "Error in operand function Avg[]: Operand Function requires at least 1 parameter.";
                return false;
            }
            double num = 0.0;
            try
            {
                foreach (TokenItem item in Parameters)
                {
                    if (DataTypeCheck.IsDouble(item.TokenName))
                    {
                        num += item.TokenName_Double;
                    }
                    else
                    {
                        ErrorMsg = "Error in operand function Avg[]: Operand Function can only calculate the average of parameters that can be converted to double.";
                        return false;
                    }
                }
            }
            catch (Exception exception1)
            {
                exception = exception1;
                ErrorMsg = "Error in operand function Avg[]: " + exception.Message;
                return false;
            }
            double num2 = 0.0;
            try
            {
                num2 = num / Convert.ToDouble(Parameters.Count);
            }
            catch (Exception exception2)
            {
                exception = exception2;
                ErrorMsg = "Error in operand function Avg[] while calcuating the average: " + exception.Message;
                return false;
            }
            Result = new TokenItem(num2.ToString(), CMM.Dynamic.Calculator.Parser.TokenType.Token_Operand, TokenDataType.Token_DataType_Double, false);
            return true;
        }

        private bool Between(TokenItems Parameters, out TokenItem Result, out string ErrorMsg)
        {
            ErrorMsg = "";
            Result = null;
            if (Parameters.Count != 3)
            {
                ErrorMsg = "Between[] Operand Function requires 3 parameter.";
                return false;
            }
            for (int i = 0; i < 3; i++)
            {
                if (!DataTypeCheck.IsDouble(Parameters[0].TokenName))
                {
                    ErrorMsg = "Between[] Operand Function requires 3 parameter that can be converted to double.";
                    return false;
                }
            }
            double num2 = Parameters[0].TokenName_Double;
            double num3 = Parameters[1].TokenName_Double;
            double num4 = Parameters[2].TokenName_Double;
            bool flag = false;
            if ((num2 >= num3) && (num2 <= num4))
            {
                flag = true;
            }
            Result = new TokenItem(flag.ToString().ToLower(), CMM.Dynamic.Calculator.Parser.TokenType.Token_Operand, TokenDataType.Token_DataType_Boolean, false);
            return true;
        }

        private bool ConCat(TokenItems Parameters, out TokenItem Result, out string ErrorMsg)
        {
            ErrorMsg = "";
            Result = null;
            if (Parameters.Count == 0)
            {
                ErrorMsg = "Error in operand function ConCat[]: Operand Function requires at least 1 parameter.";
                return false;
            }
            string tokenName = "\"";
            try
            {
                foreach (TokenItem item in Parameters)
                {
                    tokenName = tokenName + DataTypeCheck.RemoveTextQuotes(item.TokenName);
                }
            }
            catch (Exception exception)
            {
                ErrorMsg = "Error in operand function ConCat[]: " + exception.Message;
                return false;
            }
            tokenName = tokenName + "\"";
            Result = new TokenItem(tokenName, CMM.Dynamic.Calculator.Parser.TokenType.Token_Operand, TokenDataType.Token_DataType_String, false);
            return true;
        }

        private bool Contains(TokenItems Parameters, out TokenItem Result, out string ErrorMsg)
        {
            ErrorMsg = "";
            Result = null;
            if (Parameters.Count <= 1)
            {
                ErrorMsg = "Contains[] Operand Function requires at least 2 parameters.";
                return false;
            }
            string tokenName = Parameters[0].TokenName;
            bool flag = false;
            for (int i = 1; i < Parameters.Count; i++)
            {
                if (DataTypeCheck.RemoveTextQuotes(Parameters[i].TokenName) == tokenName)
                {
                    flag = true;
                    break;
                }
            }
            Result = new TokenItem(flag.ToString().ToLower(), CMM.Dynamic.Calculator.Parser.TokenType.Token_Operand, TokenDataType.Token_DataType_Boolean, false);
            return true;
        }

        private bool Cos(TokenItems Parameters, out TokenItem Result, out string ErrorMsg)
        {
            ErrorMsg = "";
            Result = null;
            if (Parameters.Count != 1)
            {
                ErrorMsg = "Error in operand function Cos[]: Operand Function requires 1 parameter.";
                return false;
            }
            if (DataTypeCheck.IsDouble(Parameters[0].TokenName))
            {
                double num2 = Math.Cos(Parameters[0].TokenName_Double);
                Result = new TokenItem(num2.ToString(), CMM.Dynamic.Calculator.Parser.TokenType.Token_Operand, TokenDataType.Token_DataType_Double, false);
            }
            else
            {
                ErrorMsg = "Error in operand function Cos[]: Operand Function can only evaluate parameters that can be converted to a double.";
                return false;
            }
            return true;
        }

        private bool DateAdd(TokenItems Parameters, out TokenItem Result, out string ErrorMsg)
        {
            ErrorMsg = "";
            Result = null;
            if (Parameters.Count != 3)
            {
                ErrorMsg = "DateAdd[] Operand Function requires 3 parameter.";
                return false;
            }
            if (!DataTypeCheck.IsDate(Parameters[0].TokenName))
            {
                ErrorMsg = "DateAdd[] Operand Function requires a date in the first parameter.";
                return false;
            }
            string str = DataTypeCheck.RemoveTextQuotes(Parameters[1].TokenName).Trim().ToLower();
            if (((str != "d") && (str != "m")) && ((str != "y") && (str != "b")))
            {
                ErrorMsg = "DateAdd[] Operand Function requires that the second parameter is a d, m, y, b.";
                return false;
            }
            if (!DataTypeCheck.IsInteger(Parameters[2].TokenName))
            {
                ErrorMsg = "DateAdd[] Operand Function requires an integer in the third parameter.";
                return false;
            }
            int months = Parameters[2].TokenName_Int;
            DateTime time = Parameters[0].TokenName_DateTime;
            switch (str)
            {
                case "d":
                    time = time.AddDays((double) months);
                    break;

                case "m":
                    time = time.AddMonths(months);
                    break;

                case "y":
                    time = time.AddYears(months);
                    break;

                case "b":
                {
                    int num2 = 0;
                    DateTime time2 = time;
                    while (num2 < months)
                    {
                        time2 = time2.AddDays(1.0);
                        if ((time2.DayOfWeek != DayOfWeek.Saturday) && (time2.DayOfWeek != DayOfWeek.Sunday))
                        {
                            num2++;
                            time = time2;
                        }
                    }
                    break;
                }
            }
            Result = new TokenItem(time.ToString("M.d.yyyy"), CMM.Dynamic.Calculator.Parser.TokenType.Token_Operand, TokenDataType.Token_DataType_Date, false);
            return true;
        }

        private bool DateMax(TokenItems Parameters, out TokenItem Result, out string ErrorMsg)
        {
            ErrorMsg = "";
            Result = null;
            if (Parameters.Count == 0)
            {
                ErrorMsg = "DateMax[] Operand Function requires at least 1 parameter.";
                return false;
            }
            DateTime minValue = DateTime.MinValue;
            bool flag = true;
            foreach (TokenItem item in Parameters)
            {
                if (DataTypeCheck.IsDate(item.TokenName))
                {
                    if (flag)
                    {
                        minValue = item.TokenName_DateTime;
                        flag = false;
                    }
                    else if (minValue.Subtract(item.TokenName_DateTime).TotalDays < 0.0)
                    {
                        minValue = item.TokenName_DateTime;
                    }
                }
                else
                {
                    ErrorMsg = "DateMax[] Operand Function expects that all parameters can be converted to date time.";
                    return false;
                }
            }
            Result = new TokenItem(minValue.ToString("M.d.yyyy"), CMM.Dynamic.Calculator.Parser.TokenType.Token_Operand, TokenDataType.Token_DataType_Date, false);
            return true;
        }

        private bool DateMin(TokenItems Parameters, out TokenItem Result, out string ErrorMsg)
        {
            ErrorMsg = "";
            Result = null;
            if (Parameters.Count == 0)
            {
                ErrorMsg = "DateMin[] Operand Function requires at least 1 parameter.";
                return false;
            }
            DateTime minValue = DateTime.MinValue;
            bool flag = true;
            foreach (TokenItem item in Parameters)
            {
                if (DataTypeCheck.IsDate(item.TokenName))
                {
                    if (flag)
                    {
                        minValue = item.TokenName_DateTime;
                        flag = false;
                    }
                    else if (minValue.Subtract(item.TokenName_DateTime).TotalDays > 0.0)
                    {
                        minValue = item.TokenName_DateTime;
                    }
                }
                else
                {
                    ErrorMsg = "DateMin[] Operand Function expects that all parameters can be converted to date time.";
                    return false;
                }
            }
            Result = new TokenItem(minValue.ToString("M.d.yyyy"), CMM.Dynamic.Calculator.Parser.TokenType.Token_Operand, TokenDataType.Token_DataType_Date, false);
            return true;
        }

        private bool Day(TokenItems Parameters, out TokenItem Result, out string ErrorMsg)
        {
            ErrorMsg = "";
            Result = null;
            if (Parameters.Count != 1)
            {
                ErrorMsg = "Error in operand function Day[]: Operand Function requires 1 parameter.";
                return false;
            }
            if (DataTypeCheck.IsDate(Parameters[0].TokenName))
            {
                int day = Parameters[0].TokenName_DateTime.Day;
                Result = new TokenItem(day.ToString(), CMM.Dynamic.Calculator.Parser.TokenType.Token_Operand, TokenDataType.Token_DataType_Int, false);
            }
            else
            {
                ErrorMsg = "Error in operand function Day[]: Operand Function requires the parameter of type date time.";
                return false;
            }
            return true;
        }

        private bool Eval(TokenItems Parameters, out TokenItem Result, out string ErrorMsg)
        {
            ErrorMsg = "";
            Result = null;
            if (Parameters.Count != 1)
            {
                ErrorMsg = "Eval[] Operand Function requires 1 parameter.";
                return false;
            }
            Token tokens = new Token(DataTypeCheck.RemoveTextQuotes(Parameters[0].TokenName));
            if (tokens.AnyErrors)
            {
                ErrorMsg = tokens.LastErrorMessage;
                return false;
            }
            Evaluator evaluator = new Evaluator(tokens);
            string sValue = "";
            if (!evaluator.Evaluate(out sValue, out ErrorMsg))
            {
                return false;
            }
            if (DataTypeCheck.IsInteger(sValue))
            {
                Result = new TokenItem(sValue, CMM.Dynamic.Calculator.Parser.TokenType.Token_Operand, TokenDataType.Token_DataType_Int, false);
            }
            else if (DataTypeCheck.IsDouble(sValue))
            {
                Result = new TokenItem(sValue, CMM.Dynamic.Calculator.Parser.TokenType.Token_Operand, TokenDataType.Token_DataType_Double, false);
            }
            else if (DataTypeCheck.IsDate(sValue))
            {
                Result = new TokenItem(sValue, CMM.Dynamic.Calculator.Parser.TokenType.Token_Operand, TokenDataType.Token_DataType_Date, false);
            }
            else
            {
                Result = new TokenItem(sValue, CMM.Dynamic.Calculator.Parser.TokenType.Token_Operand, TokenDataType.Token_DataType_String, false);
            }
            return true;
        }

        public bool Evaluate(out string sValue, out string ErrorMsg)
        {
            return this.Evaluate(this.token.RPNQueue, out sValue, out ErrorMsg);
        }

        public bool Evaluate(ExQueue<TokenItem> RPNQueue, out string sValue, out string ErrorMsg)
        {
            Exception exception;
            ErrorMsg = "";
            sValue = "";
            this.token.LastEvaluationResult = "";
            Stopwatch stopwatch = Stopwatch.StartNew();
            if (this.token.AnyErrors)
            {
                ErrorMsg = this.token.LastErrorMessage;
                return false;
            }
            ExStack<TokenItem> stack = new ExStack<TokenItem>(this.token.TokenItems.Count);
            int count = RPNQueue.Count;
            int num2 = 0;
            while (num2 < count)
            {
                TokenItem item = RPNQueue[num2];
                num2++;
                Debug.WriteLine(item.TokenName);
                if (item.TokenDataType == TokenDataType.Token_DataType_Variable)
                {
                    if (!item.WillBeAssigned)
                    {
                        if (this.token.Variables.VariableExists(item.TokenName))
                        {
                            stack.Push(new TokenItem(this.token.Variables[item.TokenName].VariableValue, CMM.Dynamic.Calculator.Parser.TokenType.Token_Operand, item.InOperandFunction));
                        }
                        else
                        {
                            stack.Push(new TokenItem("", CMM.Dynamic.Calculator.Parser.TokenType.Token_Operand, item.InOperandFunction));
                        }
                    }
                    else
                    {
                        stack.Push(item);
                    }
                }
                else
                {
                    TokenItem item2;
                    TokenItem item3;
                    TokenItem item4;
                    if (item.TokenType == CMM.Dynamic.Calculator.Parser.TokenType.Token_Operator)
                    {
                        item2 = null;
                        item3 = null;
                        try
                        {
                            if (stack.Count > 0)
                            {
                                item2 = stack.Pop();
                            }
                            if (stack.Count > 0)
                            {
                                item3 = stack.Pop();
                            }
                        }
                        catch (Exception exception1)
                        {
                            exception = exception1;
                            ErrorMsg = "Error in Evaluator.Evaluate() while popping 2 tokens for an operator: " + exception.Message;
                            return false;
                        }
                        if (item2 == null)
                        {
                            ErrorMsg = "Failed to evaluate the rule expression: The right operand token is null: There may be an issue with the rule syntax.";
                            return false;
                        }
                        if (item3 == null)
                        {
                            ErrorMsg = "Failed to evaluate the rule expression: The left operand token is null: There may be an issue with the rule syntax.";
                            return false;
                        }
                        try
                        {
                            item4 = null;
                            if (!this.EvaluateTokens(item3, item2, item, out item4, out ErrorMsg))
                            {
                                return false;
                            }
                            if (item4 == null)
                            {
                                ErrorMsg = "Failed to evaluate the rule expression: The result of an operator is null: There may be an issue with the rule syntax.";
                                return false;
                            }
                            stack.Push(item4);
                        }
                        catch (Exception exception2)
                        {
                            exception = exception2;
                            ErrorMsg = "Failed to evaluate the rule expression: The result of an operator threw an error: " + exception.Message;
                            return false;
                        }
                    }
                    else if (item.TokenType == CMM.Dynamic.Calculator.Parser.TokenType.Token_Operand_Function_Stop)
                    {
                        int num3 = stack.Count;
                        TokenItems parameters = new TokenItems(this.token);
                        try
                        {
                            for (int i = 0; i < num3; i++)
                            {
                                TokenItem operandFunction = stack.Pop();
                                if (operandFunction.TokenType == CMM.Dynamic.Calculator.Parser.TokenType.Token_Operand_Function_Start)
                                {
                                    item4 = null;
                                    if (!this.EvaluateOperandFunction(operandFunction, parameters, out item4, out ErrorMsg))
                                    {
                                        return false;
                                    }
                                    if (item4 == null)
                                    {
                                        ErrorMsg = "Failed to evaluate the rule expression: The result of an operand function is null: There may be an issue with the rule syntax.";
                                        return false;
                                    }
                                    stack.Push(item4);
                                    goto Label_0518;
                                }
                                if (operandFunction.TokenType != CMM.Dynamic.Calculator.Parser.TokenType.Token_Operand_Function_Delimiter)
                                {
                                    parameters.AddToFront(operandFunction);
                                }
                            }
                        }
                        catch (Exception exception3)
                        {
                            exception = exception3;
                            ErrorMsg = "Failed to evaluate the rule expression: The evaluation of an operand function threw an error: " + exception.Message;
                            return false;
                        }
                    Label_0518:;
                    }
                    else if (item.TokenType == CMM.Dynamic.Calculator.Parser.TokenType.Token_Assignemt_Start)
                    {
                        item2 = null;
                        item3 = null;
                        try
                        {
                            if (stack.Count > 0)
                            {
                                item2 = stack.Pop();
                            }
                            if (stack.Count > 0)
                            {
                                item3 = stack.Pop();
                            }
                        }
                        catch (Exception exception4)
                        {
                            exception = exception4;
                            ErrorMsg = "Error in Evaluator.Evaluate() while popping 2 tokens for an operator: " + exception.Message;
                            return false;
                        }
                        if (item2 == null)
                        {
                            ErrorMsg = "Failed to evaluate the rule expression: The right operand token is null: There may be an issue with the rule syntax.";
                            return false;
                        }
                        if (item3 == null)
                        {
                            ErrorMsg = "Failed to evaluate the rule expression: The left operand token is null: There may be an issue with the rule syntax.";
                            return false;
                        }
                        if (!this.token.Variables.VariableExists(item3.TokenName))
                        {
                            ErrorMsg = "Failed to evaluate the rule expression: Failed to find the variable '" + item3.TokenName + "' for the assignment.";
                            return false;
                        }
                        this.token.Variables[item3.TokenName].VariableValue = item2.TokenName;
                    }
                    else if (item.TokenType == CMM.Dynamic.Calculator.Parser.TokenType.Token_Operand_Function_Start)
                    {
                        if (item.TokenName.Trim().ToLower() != "iif[")
                        {
                            stack.Push(item);
                        }
                        else if (!item.CanShortCircuit)
                        {
                            stack.Push(item);
                        }
                        else
                        {
                            item4 = item.ShortCircuit.Evaluate(out ErrorMsg);
                            if (item4 == null)
                            {
                                return false;
                            }
                            stack.Push(item4);
                            num2++;
                        }
                    }
                    else
                    {
                        stack.Push(item);
                    }
                }
            }
            if (stack.Count == 1)
            {
                try
                {
                    TokenItem item6 = stack.Pop();
                    sValue = item6.TokenName;
                    this.token.LastEvaluationResult = sValue;
                }
                catch (Exception exception5)
                {
                    exception = exception5;
                    ErrorMsg = "Failed to evaluate the rule expression after all the tokens have been considered: " + exception.Message;
                    return false;
                }
            }
            else if (stack.Count != 0)
            {
                ErrorMsg = "Invalid Rule Syntax";
                return false;
            }
            stopwatch.Stop();
            this.tokenEvalTime = stopwatch.Elapsed.TotalMilliseconds;
            this.token.LastEvaluationTime = this.tokenEvalTime;
            return true;
        }

        private bool EvaluateOperandFunction(TokenItem OperandFunction, TokenItems Parameters, out TokenItem Result, out string ErrorMsg)
        {
            Exception exception;
            Result = null;
            ErrorMsg = "";
            bool flag = true;
            if (OperandFunction == null)
            {
                ErrorMsg = "Failed to evaluate the operand function: The operand function token is null.";
                return false;
            }
            if (Parameters == null)
            {
                ErrorMsg = "Failed to evaluate the operand function: The parameters collection is null.";
                return false;
            }
            switch (OperandFunction.TokenName.Trim().ToLower())
            {
                case "cos[":
                {
                    try
                    {
                        return this.Cos(Parameters, out Result, out ErrorMsg);
                    }
                    catch (Exception exception1)
                    {
                        exception = exception1;
                        ErrorMsg = "Failed to evaluate the operand function " + OperandFunction.TokenName.Trim().ToLower() + ": " + exception.Message;
                        return false;
                    }
                }
                case "avg[":
                {
                    try
                    {
                        return this.Avg(Parameters, out Result, out ErrorMsg);
                    }
                    catch (Exception exception2)
                    {
                        exception = exception2;
                        ErrorMsg = "Failed to evaluate the operand function " + OperandFunction.TokenName.Trim().ToLower() + ": " + exception.Message;
                        return false;
                    }
                }
                case "abs[":
                {
                    try
                    {
                        return this.Abs(Parameters, out Result, out ErrorMsg);
                    }
                    catch (Exception exception3)
                    {
                        exception = exception3;
                        ErrorMsg = "Failed to evaluate the operand function " + OperandFunction.TokenName.Trim().ToLower() + ": " + exception.Message;
                        return false;
                    }
                }
                case "not[":
                {
                    try
                    {
                        return this.Not(Parameters, out Result, out ErrorMsg);
                    }
                    catch (Exception exception4)
                    {
                        exception = exception4;
                        ErrorMsg = "Failed to evaluate the operand function " + OperandFunction.TokenName.Trim().ToLower() + ": " + exception.Message;
                        return false;
                    }
                }
                case "isalldigits[":
                {
                    try
                    {
                        return this.IsAllDigits(Parameters, out Result, out ErrorMsg);
                    }
                    catch (Exception exception5)
                    {
                        exception = exception5;
                        ErrorMsg = "Failed to evaluate the operand function " + OperandFunction.TokenName.Trim().ToLower() + ": " + exception.Message;
                        return false;
                    }
                }
                case "concat[":
                {
                    try
                    {
                        return this.ConCat(Parameters, out Result, out ErrorMsg);
                    }
                    catch (Exception exception6)
                    {
                        exception = exception6;
                        ErrorMsg = "Failed to evaluate the operand function " + OperandFunction.TokenName.Trim().ToLower() + ": " + exception.Message;
                        return false;
                    }
                }
                case "date[":
                {
                    try
                    {
                        return this.NewDate(Parameters, out Result, out ErrorMsg);
                    }
                    catch (Exception exception7)
                    {
                        exception = exception7;
                        ErrorMsg = "Failed to evaluate the operand function " + OperandFunction.TokenName.Trim().ToLower() + ": " + exception.Message;
                        return false;
                    }
                }
                case "day[":
                {
                    try
                    {
                        return this.Day(Parameters, out Result, out ErrorMsg);
                    }
                    catch (Exception exception8)
                    {
                        exception = exception8;
                        ErrorMsg = "Failed to evaluate the operand function " + OperandFunction.TokenName.Trim().ToLower() + ": " + exception.Message;
                        return false;
                    }
                }
                case "month[":
                {
                    try
                    {
                        return this.Month(Parameters, out Result, out ErrorMsg);
                    }
                    catch (Exception exception9)
                    {
                        exception = exception9;
                        ErrorMsg = "Failed to evaluate the operand function " + OperandFunction.TokenName.Trim().ToLower() + ": " + exception.Message;
                        return false;
                    }
                }
                case "year[":
                {
                    try
                    {
                        return this.Year(Parameters, out Result, out ErrorMsg);
                    }
                    catch (Exception exception10)
                    {
                        exception = exception10;
                        ErrorMsg = "Failed to evaluate the operand function " + OperandFunction.TokenName.Trim().ToLower() + ": " + exception.Message;
                        return false;
                    }
                }
                case "iif[":
                {
                    try
                    {
                        return this.IIF(Parameters, out Result, out ErrorMsg);
                    }
                    catch (Exception exception11)
                    {
                        exception = exception11;
                        ErrorMsg = "Failed to evaluate the operand function " + OperandFunction.TokenName.Trim().ToLower() + ": " + exception.Message;
                        return false;
                    }
                }
                case "lcase[":
                {
                    try
                    {
                        return this.LCase(Parameters, out Result, out ErrorMsg);
                    }
                    catch (Exception exception12)
                    {
                        exception = exception12;
                        ErrorMsg = "Failed to evaluate the operand function " + OperandFunction.TokenName.Trim().ToLower() + ": " + exception.Message;
                        return false;
                    }
                }
                case "pcase[":
                {
                    try
                    {
                        return this.PCase(Parameters, out Result, out ErrorMsg);
                    }
                    catch (Exception exception13)
                    {
                        exception = exception13;
                        ErrorMsg = "Failed to evaluate the operand function " + OperandFunction.TokenName.Trim().ToLower() + ": " + exception.Message;
                        return false;
                    }
                }
                case "left[":
                {
                    try
                    {
                        return this.Left(Parameters, out Result, out ErrorMsg);
                    }
                    catch (Exception exception14)
                    {
                        exception = exception14;
                        ErrorMsg = "Failed to evaluate the operand function " + OperandFunction.TokenName.Trim().ToLower() + ": " + exception.Message;
                        return false;
                    }
                }
                case "len[":
                {
                    try
                    {
                        return this.Length(Parameters, out Result, out ErrorMsg);
                    }
                    catch (Exception exception15)
                    {
                        exception = exception15;
                        ErrorMsg = "Failed to evaluate the operand function " + OperandFunction.TokenName.Trim().ToLower() + ": " + exception.Message;
                        return false;
                    }
                }
                case "numericmax[":
                {
                    try
                    {
                        return this.NumericMax(Parameters, out Result, out ErrorMsg);
                    }
                    catch (Exception exception16)
                    {
                        exception = exception16;
                        ErrorMsg = "Failed to evaluate the operand function " + OperandFunction.TokenName.Trim().ToLower() + ": " + exception.Message;
                        return false;
                    }
                }
                case "mid[":
                {
                    try
                    {
                        return this.Mid(Parameters, out Result, out ErrorMsg);
                    }
                    catch (Exception exception17)
                    {
                        exception = exception17;
                        ErrorMsg = "Failed to evaluate the operand function " + OperandFunction.TokenName.Trim().ToLower() + ": " + exception.Message;
                        return false;
                    }
                }
                case "numericmin[":
                {
                    try
                    {
                        return this.NumericMin(Parameters, out Result, out ErrorMsg);
                    }
                    catch (Exception exception18)
                    {
                        exception = exception18;
                        ErrorMsg = "Failed to evaluate the operand function " + OperandFunction.TokenName.Trim().ToLower() + ": " + exception.Message;
                        return false;
                    }
                }
                case "datemax[":
                {
                    try
                    {
                        return this.DateMax(Parameters, out Result, out ErrorMsg);
                    }
                    catch (Exception exception19)
                    {
                        exception = exception19;
                        ErrorMsg = "Failed to evaluate the operand function " + OperandFunction.TokenName.Trim().ToLower() + ": " + exception.Message;
                        return false;
                    }
                }
                case "datemin[":
                {
                    try
                    {
                        return this.DateMin(Parameters, out Result, out ErrorMsg);
                    }
                    catch (Exception exception20)
                    {
                        exception = exception20;
                        ErrorMsg = "Failed to evaluate the operand function " + OperandFunction.TokenName.Trim().ToLower() + ": " + exception.Message;
                        return false;
                    }
                }
                case "stringmax[":
                {
                    try
                    {
                        return this.StringMax(Parameters, out Result, out ErrorMsg);
                    }
                    catch (Exception exception21)
                    {
                        exception = exception21;
                        ErrorMsg = "Failed to evaluate the operand function " + OperandFunction.TokenName.Trim().ToLower() + ": " + exception.Message;
                        return false;
                    }
                }
                case "stringmin[":
                {
                    try
                    {
                        return this.StringMin(Parameters, out Result, out ErrorMsg);
                    }
                    catch (Exception exception22)
                    {
                        exception = exception22;
                        ErrorMsg = "Failed to evaluate the operand function " + OperandFunction.TokenName.Trim().ToLower() + ": " + exception.Message;
                        return false;
                    }
                }
                case "right[":
                {
                    try
                    {
                        return this.Right(Parameters, out Result, out ErrorMsg);
                    }
                    catch (Exception exception23)
                    {
                        exception = exception23;
                        ErrorMsg = "Failed to evaluate the operand function " + OperandFunction.TokenName.Trim().ToLower() + ": " + exception.Message;
                        return false;
                    }
                }
                case "round[":
                {
                    try
                    {
                        return this.Round(Parameters, out Result, out ErrorMsg);
                    }
                    catch (Exception exception24)
                    {
                        exception = exception24;
                        ErrorMsg = "Failed to evaluate the operand function " + OperandFunction.TokenName.Trim().ToLower() + ": " + exception.Message;
                        return false;
                    }
                }
                case "sqrt[":
                {
                    try
                    {
                        return this.Sqrt(Parameters, out Result, out ErrorMsg);
                    }
                    catch (Exception exception25)
                    {
                        exception = exception25;
                        ErrorMsg = "Failed to evaluate the operand function " + OperandFunction.TokenName.Trim().ToLower() + ": " + exception.Message;
                        return false;
                    }
                }
                case "ucase[":
                {
                    try
                    {
                        return this.UCase(Parameters, out Result, out ErrorMsg);
                    }
                    catch (Exception exception26)
                    {
                        exception = exception26;
                        ErrorMsg = "Failed to evaluate the operand function " + OperandFunction.TokenName.Trim().ToLower() + ": " + exception.Message;
                        return false;
                    }
                }
                case "contains[":
                {
                    try
                    {
                        return this.Contains(Parameters, out Result, out ErrorMsg);
                    }
                    catch (Exception exception27)
                    {
                        exception = exception27;
                        ErrorMsg = "Failed to evaluate the operand function " + OperandFunction.TokenName.Trim().ToLower() + ": " + exception.Message;
                        return false;
                    }
                }
                case "between[":
                {
                    try
                    {
                        return this.Between(Parameters, out Result, out ErrorMsg);
                    }
                    catch (Exception exception28)
                    {
                        exception = exception28;
                        ErrorMsg = "Failed to evaluate the operand function " + OperandFunction.TokenName.Trim().ToLower() + ": " + exception.Message;
                        return false;
                    }
                }
                case "indexof[":
                {
                    try
                    {
                        return this.IndexOf(Parameters, out Result, out ErrorMsg);
                    }
                    catch (Exception exception29)
                    {
                        exception = exception29;
                        ErrorMsg = "Failed to evaluate the operand function " + OperandFunction.TokenName.Trim().ToLower() + ": " + exception.Message;
                        return false;
                    }
                }
                case "isnullorempty[":
                {
                    try
                    {
                        return this.IsNullOrEmpty(Parameters, out Result, out ErrorMsg);
                    }
                    catch (Exception exception30)
                    {
                        exception = exception30;
                        ErrorMsg = "Failed to evaluate the operand function " + OperandFunction.TokenName.Trim().ToLower() + ": " + exception.Message;
                        return false;
                    }
                }
                case "istrueornull[":
                {
                    try
                    {
                        return this.IsTrueOrNull(Parameters, out Result, out ErrorMsg);
                    }
                    catch (Exception exception31)
                    {
                        exception = exception31;
                        ErrorMsg = "Failed to evaluate the operand function " + OperandFunction.TokenName.Trim().ToLower() + ": " + exception.Message;
                        return false;
                    }
                }
                case "isfalseornull[":
                {
                    try
                    {
                        return this.IsFalseOrNull(Parameters, out Result, out ErrorMsg);
                    }
                    catch (Exception exception32)
                    {
                        exception = exception32;
                        ErrorMsg = "Failed to evaluate the operand function " + OperandFunction.TokenName.Trim().ToLower() + ": " + exception.Message;
                        return false;
                    }
                }
                case "trim[":
                {
                    try
                    {
                        return this.Trim(Parameters, out Result, out ErrorMsg);
                    }
                    catch (Exception exception33)
                    {
                        exception = exception33;
                        ErrorMsg = "Failed to evaluate the operand function " + OperandFunction.TokenName.Trim().ToLower() + ": " + exception.Message;
                        return false;
                    }
                }
                case "rtrim[":
                {
                    try
                    {
                        return this.RTrim(Parameters, out Result, out ErrorMsg);
                    }
                    catch (Exception exception34)
                    {
                        exception = exception34;
                        ErrorMsg = "Failed to evaluate the operand function " + OperandFunction.TokenName.Trim().ToLower() + ": " + exception.Message;
                        return false;
                    }
                }
                case "ltrim[":
                {
                    try
                    {
                        return this.LTrim(Parameters, out Result, out ErrorMsg);
                    }
                    catch (Exception exception35)
                    {
                        exception = exception35;
                        ErrorMsg = "Failed to evaluate the operand function " + OperandFunction.TokenName.Trim().ToLower() + ": " + exception.Message;
                        return false;
                    }
                }
                case "now[":
                {
                    try
                    {
                        return this.Now(Parameters, out Result, out ErrorMsg);
                    }
                    catch (Exception exception36)
                    {
                        exception = exception36;
                        ErrorMsg = "Failed to evaluate the operand function " + OperandFunction.TokenName.Trim().ToLower() + ": " + exception.Message;
                        return false;
                    }
                }
                case "dateadd[":
                {
                    try
                    {
                        return this.DateAdd(Parameters, out Result, out ErrorMsg);
                    }
                    catch (Exception exception37)
                    {
                        exception = exception37;
                        ErrorMsg = "Failed to evaluate the operand function " + OperandFunction.TokenName.Trim().ToLower() + ": " + exception.Message;
                        return false;
                    }
                }
                case "replace[":
                {
                    try
                    {
                        return this.Replace(Parameters, out Result, out ErrorMsg);
                    }
                    catch (Exception exception38)
                    {
                        exception = exception38;
                        ErrorMsg = "Failed to evaluate the operand function " + OperandFunction.TokenName.Trim().ToLower() + ": " + exception.Message;
                        return false;
                    }
                }
                case "remove[":
                {
                    try
                    {
                        return this.Remove(Parameters, out Result, out ErrorMsg);
                    }
                    catch (Exception exception39)
                    {
                        exception = exception39;
                        ErrorMsg = "Failed to evaluate the operand function " + OperandFunction.TokenName.Trim().ToLower() + ": " + exception.Message;
                        return false;
                    }
                }
                case "eval[":
                {
                    try
                    {
                        return this.Eval(Parameters, out Result, out ErrorMsg);
                    }
                    catch (Exception exception40)
                    {
                        exception = exception40;
                        ErrorMsg = "Failed to evaluate the operand function " + OperandFunction.TokenName.Trim().ToLower() + ": " + exception.Message;
                        return false;
                    }
                }
                case "quote[":
                    try
                    {
                        flag = true;
                        Result = new TokenItem("\"", CMM.Dynamic.Calculator.Parser.TokenType.Token_Operand, TokenDataType.Token_DataType_String, false);
                    }
                    catch (Exception exception41)
                    {
                        exception = exception41;
                        ErrorMsg = "Failed to evaluate the operand function " + OperandFunction.TokenName.Trim().ToLower() + ": " + exception.Message;
                        flag = false;
                    }
                    return flag;

                case "rpad[":
                {
                    try
                    {
                        return this.RPad(Parameters, out Result, out ErrorMsg);
                    }
                    catch (Exception exception42)
                    {
                        exception = exception42;
                        ErrorMsg = "Failed to evaluate the operand function " + OperandFunction.TokenName.Trim().ToLower() + ": " + exception.Message;
                        return false;
                    }
                }
                case "lpad[":
                {
                    try
                    {
                        return this.LPad(Parameters, out Result, out ErrorMsg);
                    }
                    catch (Exception exception43)
                    {
                        exception = exception43;
                        ErrorMsg = "Failed to evaluate the operand function " + OperandFunction.TokenName.Trim().ToLower() + ": " + exception.Message;
                        return false;
                    }
                }
                case "join[":
                {
                    try
                    {
                        return this.Join(Parameters, out Result, out ErrorMsg);
                    }
                    catch (Exception exception44)
                    {
                        exception = exception44;
                        ErrorMsg = "Failed to evaluate the operand function " + OperandFunction.TokenName.Trim().ToLower() + ": " + exception.Message;
                        return false;
                    }
                }
                case "searchstring[":
                {
                    try
                    {
                        return this.SearchString(Parameters, out Result, out ErrorMsg);
                    }
                    catch (Exception exception45)
                    {
                        exception = exception45;
                        ErrorMsg = "Failed to evaluate the operand function " + OperandFunction.TokenName.Trim().ToLower() + ": " + exception.Message;
                        return false;
                    }
                }
                case "substring[":
                {
                    try
                    {
                        return this.SubString(Parameters, out Result, out ErrorMsg);
                    }
                    catch (Exception exception46)
                    {
                        exception = exception46;
                        ErrorMsg = "Failed to evaluate the operand function " + OperandFunction.TokenName.Trim().ToLower() + ": " + exception.Message;
                        return false;
                    }
                }
                case "sin[":
                {
                    try
                    {
                        return this.Sin(Parameters, out Result, out ErrorMsg);
                    }
                    catch (Exception exception47)
                    {
                        exception = exception47;
                        ErrorMsg = "Failed to evaluate the operand function " + OperandFunction.TokenName.Trim().ToLower() + ": " + exception.Message;
                        return false;
                    }
                }
            }
            ErrorMsg = "Unknown operand function";
            return false;
        }

        private bool EvaluateTokens(TokenItem LeftOperand, TokenItem RightOperand, TokenItem Operator, out TokenItem Result, out string ErrorMsg)
        {
            Exception exception;
            string str;
            string str2;
            Result = null;
            ErrorMsg = "";
            double num = 0.0;
            bool flag = false;
            if (LeftOperand == null)
            {
                ErrorMsg = "Failed to evaluate the operator: The left token is null.";
                return false;
            }
            if (RightOperand == null)
            {
                ErrorMsg = "Failed to evaluate the operator: The right token is null.";
                return false;
            }
            if (Operator == null)
            {
                ErrorMsg = "Failed to evaluate the operator: The operator token is null.";
                return false;
            }
            switch (Operator.TokenName.Trim().ToLower())
            {
                case "^":
                {
                    try
                    {
                        if (DataTypeCheck.IsDouble(LeftOperand.TokenName) && DataTypeCheck.IsDouble(RightOperand.TokenName))
                        {
                            num = Math.Pow(LeftOperand.TokenName_Double, RightOperand.TokenName_Double);
                            Result = new TokenItem(num.ToString(), CMM.Dynamic.Calculator.Parser.TokenType.Token_Operand, TokenDataType.Token_DataType_Double, LeftOperand.InOperandFunction);
                            goto Label_1005;
                        }
                        ErrorMsg = "Syntax Error: Expecting numeric values for exponents.";
                        return false;
                    }
                    catch (Exception exception1)
                    {
                        exception = exception1;
                        ErrorMsg = "Failed to evaluate the Exponent operator: " + exception.Message;
                        return false;
                    }
                }
                case "*":
                {
                    try
                    {
                        if (DataTypeCheck.IsDouble(LeftOperand.TokenName) && DataTypeCheck.IsDouble(RightOperand.TokenName))
                        {
                            num = LeftOperand.TokenName_Double * RightOperand.TokenName_Double;
                            Result = new TokenItem(num.ToString(), CMM.Dynamic.Calculator.Parser.TokenType.Token_Operand, TokenDataType.Token_DataType_Double, LeftOperand.InOperandFunction);
                            goto Label_1005;
                        }
                        ErrorMsg = "Syntax Error: Expecting numeric values to multiply.";
                        return false;
                    }
                    catch (Exception exception2)
                    {
                        exception = exception2;
                        ErrorMsg = "Failed to evaluate the Multiplication operator: " + exception.Message;
                        return false;
                    }
                }
                case "/":
                    try
                    {
                        if (DataTypeCheck.IsDouble(LeftOperand.TokenName) && DataTypeCheck.IsDouble(RightOperand.TokenName))
                        {
                            double num2 = RightOperand.TokenName_Double;
                            if (num2 == 0.0)
                            {
                                ErrorMsg = "Syntax Error: Division by zero.";
                                return false;
                            }
                            num = LeftOperand.TokenName_Double / num2;
                            Result = new TokenItem(num.ToString(), CMM.Dynamic.Calculator.Parser.TokenType.Token_Operand, TokenDataType.Token_DataType_Double, LeftOperand.InOperandFunction);
                        }
                        else
                        {
                            ErrorMsg = "Syntax Error: Expecting numeric values to divide.";
                            return false;
                        }
                    }
                    catch (Exception exception3)
                    {
                        exception = exception3;
                        ErrorMsg = "Failed to evaluate the Division operator: " + exception.Message;
                        return false;
                    }
                    goto Label_1005;

                case "%":
                    try
                    {
                        if (DataTypeCheck.IsDouble(LeftOperand.TokenName) && DataTypeCheck.IsDouble(RightOperand.TokenName))
                        {
                            if (RightOperand.TokenName_Double == 0.0)
                            {
                                ErrorMsg = "Syntax Error: Modulus by zero.";
                                return false;
                            }
                            num = LeftOperand.TokenName_Double % RightOperand.TokenName_Double;
                            Result = new TokenItem(num.ToString(), CMM.Dynamic.Calculator.Parser.TokenType.Token_Operand, TokenDataType.Token_DataType_Double, LeftOperand.InOperandFunction);
                        }
                        else
                        {
                            ErrorMsg = "Syntax Error: Expecting numeric values to modulus.";
                            return false;
                        }
                    }
                    catch (Exception exception4)
                    {
                        exception = exception4;
                        ErrorMsg = "Failed to evaluate the Modulus operator: " + exception.Message;
                        return false;
                    }
                    goto Label_1005;

                case "+":
                    try
                    {
                        if (DataTypeCheck.IsDouble(LeftOperand.TokenName) && DataTypeCheck.IsDouble(RightOperand.TokenName))
                        {
                            num = LeftOperand.TokenName_Double + RightOperand.TokenName_Double;
                            Result = new TokenItem(num.ToString(), CMM.Dynamic.Calculator.Parser.TokenType.Token_Operand, TokenDataType.Token_DataType_Double, LeftOperand.InOperandFunction);
                        }
                        else
                        {
                            ErrorMsg = "Syntax Error: Expecting numeric values to add.";
                            return false;
                        }
                    }
                    catch (Exception exception5)
                    {
                        exception = exception5;
                        ErrorMsg = "Failed to evaluate the Addition operator: " + exception.Message;
                        return false;
                    }
                    goto Label_1005;

                case "-":
                    try
                    {
                        if (DataTypeCheck.IsDouble(LeftOperand.TokenName) && DataTypeCheck.IsDouble(RightOperand.TokenName))
                        {
                            num = LeftOperand.TokenName_Double - RightOperand.TokenName_Double;
                            Result = new TokenItem(num.ToString(), CMM.Dynamic.Calculator.Parser.TokenType.Token_Operand, TokenDataType.Token_DataType_Double, LeftOperand.InOperandFunction);
                        }
                        else
                        {
                            ErrorMsg = "Syntax Error: Expecting numeric values to subtract.";
                            return false;
                        }
                    }
                    catch (Exception exception6)
                    {
                        exception = exception6;
                        ErrorMsg = "Failed to evaluate the Subtraction operator: " + exception.Message;
                        return false;
                    }
                    goto Label_1005;

                case "<":
                    if (!DataTypeCheck.IsDouble(LeftOperand.TokenName) || !DataTypeCheck.IsDouble(RightOperand.TokenName))
                    {
                        if (DataTypeCheck.IsDate(LeftOperand.TokenName) && DataTypeCheck.IsDate(RightOperand.TokenName))
                        {
                            try
                            {
                                flag = LeftOperand.TokenName_DateTime.Subtract(RightOperand.TokenName_DateTime).TotalDays < 0.0;
                                Result = new TokenItem(flag.ToString().ToLower(), CMM.Dynamic.Calculator.Parser.TokenType.Token_Operand, TokenDataType.Token_DataType_Boolean, LeftOperand.InOperandFunction);
                            }
                            catch (Exception exception8)
                            {
                                exception = exception8;
                                ErrorMsg = "Failed to evaluate the Less Than operator on date operands: " + exception.Message;
                                return false;
                            }
                        }
                        else
                        {
                            try
                            {
                                str = DataTypeCheck.RemoveTextQuotes(LeftOperand.TokenName);
                                str2 = DataTypeCheck.RemoveTextQuotes(RightOperand.TokenName);
                                flag = str.CompareTo(str2) < 0;
                                Result = new TokenItem(flag.ToString().ToLower(), CMM.Dynamic.Calculator.Parser.TokenType.Token_Operand, TokenDataType.Token_DataType_Boolean, LeftOperand.InOperandFunction);
                            }
                            catch (Exception exception9)
                            {
                                exception = exception9;
                                ErrorMsg = "Failed to evaluate the Less Than operator on string operands: " + exception.Message;
                                return false;
                            }
                        }
                    }
                    else
                    {
                        try
                        {
                            flag = LeftOperand.TokenName_Double < RightOperand.TokenName_Double;
                            Result = new TokenItem(flag.ToString().ToLower(), CMM.Dynamic.Calculator.Parser.TokenType.Token_Operand, TokenDataType.Token_DataType_Boolean, LeftOperand.InOperandFunction);
                        }
                        catch (Exception exception7)
                        {
                            exception = exception7;
                            ErrorMsg = "Failed to evaluate the Less Than operator on double operands: " + exception.Message;
                            return false;
                        }
                    }
                    goto Label_1005;

                case "<=":
                    if (!DataTypeCheck.IsDouble(LeftOperand.TokenName) || !DataTypeCheck.IsDouble(RightOperand.TokenName))
                    {
                        if (DataTypeCheck.IsDate(LeftOperand.TokenName) && DataTypeCheck.IsDate(RightOperand.TokenName))
                        {
                            try
                            {
                                flag = LeftOperand.TokenName_DateTime.Subtract(RightOperand.TokenName_DateTime).TotalDays <= 0.0;
                                Result = new TokenItem(flag.ToString().ToLower(), CMM.Dynamic.Calculator.Parser.TokenType.Token_Operand, TokenDataType.Token_DataType_Boolean, LeftOperand.InOperandFunction);
                            }
                            catch (Exception exception11)
                            {
                                exception = exception11;
                                ErrorMsg = "Failed to evaluate the Less Than or Equal to operator on date operands: " + exception.Message;
                                return false;
                            }
                        }
                        else
                        {
                            try
                            {
                                str = DataTypeCheck.RemoveTextQuotes(LeftOperand.TokenName);
                                str2 = DataTypeCheck.RemoveTextQuotes(RightOperand.TokenName);
                                flag = str.CompareTo(str2) <= 0;
                                Result = new TokenItem(flag.ToString().ToLower(), CMM.Dynamic.Calculator.Parser.TokenType.Token_Operand, TokenDataType.Token_DataType_Boolean, LeftOperand.InOperandFunction);
                            }
                            catch (Exception exception12)
                            {
                                exception = exception12;
                                ErrorMsg = "Failed to evaluate the Less Than or Equal to operator on string operands: " + exception.Message;
                                return false;
                            }
                        }
                    }
                    else
                    {
                        try
                        {
                            flag = LeftOperand.TokenName_Double <= RightOperand.TokenName_Double;
                            Result = new TokenItem(flag.ToString().ToLower(), CMM.Dynamic.Calculator.Parser.TokenType.Token_Operand, TokenDataType.Token_DataType_Boolean, LeftOperand.InOperandFunction);
                        }
                        catch (Exception exception10)
                        {
                            exception = exception10;
                            ErrorMsg = "Failed to evaluate the Less Than or Equal to operator on double operands: " + exception.Message;
                            return false;
                        }
                    }
                    goto Label_1005;

                case ">":
                    if (!DataTypeCheck.IsDouble(LeftOperand.TokenName) || !DataTypeCheck.IsDouble(RightOperand.TokenName))
                    {
                        if (DataTypeCheck.IsDate(LeftOperand.TokenName) && DataTypeCheck.IsDate(RightOperand.TokenName))
                        {
                            try
                            {
                                flag = LeftOperand.TokenName_DateTime.Subtract(RightOperand.TokenName_DateTime).TotalDays > 0.0;
                                Result = new TokenItem(flag.ToString().ToLower(), CMM.Dynamic.Calculator.Parser.TokenType.Token_Operand, TokenDataType.Token_DataType_Boolean, LeftOperand.InOperandFunction);
                            }
                            catch (Exception exception14)
                            {
                                exception = exception14;
                                ErrorMsg = "Failed to evaluate the Greater Than to operator on date operands: " + exception.Message;
                                return false;
                            }
                        }
                        else
                        {
                            try
                            {
                                str = DataTypeCheck.RemoveTextQuotes(LeftOperand.TokenName);
                                str2 = DataTypeCheck.RemoveTextQuotes(RightOperand.TokenName);
                                flag = str.CompareTo(str2) > 0;
                                Result = new TokenItem(flag.ToString().ToLower(), CMM.Dynamic.Calculator.Parser.TokenType.Token_Operand, TokenDataType.Token_DataType_Boolean, LeftOperand.InOperandFunction);
                            }
                            catch (Exception exception15)
                            {
                                exception = exception15;
                                ErrorMsg = "Failed to evaluate the Greater Than to operator on string operands: " + exception.Message;
                                return false;
                            }
                        }
                    }
                    else
                    {
                        try
                        {
                            flag = LeftOperand.TokenName_Double > RightOperand.TokenName_Double;
                            Result = new TokenItem(flag.ToString().ToLower(), CMM.Dynamic.Calculator.Parser.TokenType.Token_Operand, TokenDataType.Token_DataType_Boolean, LeftOperand.InOperandFunction);
                        }
                        catch (Exception exception13)
                        {
                            exception = exception13;
                            ErrorMsg = "Failed to evaluate the Greater Than to operator on double operands: " + exception.Message;
                            return false;
                        }
                    }
                    goto Label_1005;

                case ">=":
                    if (!DataTypeCheck.IsDouble(LeftOperand.TokenName) || !DataTypeCheck.IsDouble(RightOperand.TokenName))
                    {
                        if (DataTypeCheck.IsDate(LeftOperand.TokenName) && DataTypeCheck.IsDate(RightOperand.TokenName))
                        {
                            try
                            {
                                flag = LeftOperand.TokenName_DateTime.Subtract(RightOperand.TokenName_DateTime).TotalDays >= 0.0;
                                Result = new TokenItem(flag.ToString().ToLower(), CMM.Dynamic.Calculator.Parser.TokenType.Token_Operand, TokenDataType.Token_DataType_Boolean, LeftOperand.InOperandFunction);
                            }
                            catch (Exception exception17)
                            {
                                exception = exception17;
                                ErrorMsg = "Failed to evaluate the Greater Than or Equal to operator on date operands: " + exception.Message;
                                return false;
                            }
                        }
                        else
                        {
                            try
                            {
                                str = DataTypeCheck.RemoveTextQuotes(LeftOperand.TokenName);
                                str2 = DataTypeCheck.RemoveTextQuotes(RightOperand.TokenName);
                                flag = str.CompareTo(str2) >= 0;
                                Result = new TokenItem(flag.ToString().ToLower(), CMM.Dynamic.Calculator.Parser.TokenType.Token_Operand, TokenDataType.Token_DataType_Boolean, LeftOperand.InOperandFunction);
                            }
                            catch (Exception exception18)
                            {
                                exception = exception18;
                                ErrorMsg = "Failed to evaluate the Greater Than or Equal to operator on string operands: " + exception.Message;
                                return false;
                            }
                        }
                    }
                    else
                    {
                        try
                        {
                            flag = LeftOperand.TokenName_Double >= RightOperand.TokenName_Double;
                            Result = new TokenItem(flag.ToString().ToLower(), CMM.Dynamic.Calculator.Parser.TokenType.Token_Operand, TokenDataType.Token_DataType_Boolean, LeftOperand.InOperandFunction);
                        }
                        catch (Exception exception16)
                        {
                            exception = exception16;
                            ErrorMsg = "Failed to evaluate the Greater Than or Equal to operator on double operands: " + exception.Message;
                            return false;
                        }
                    }
                    goto Label_1005;

                case "<>":
                    if (!DataTypeCheck.IsDouble(LeftOperand.TokenName) || !DataTypeCheck.IsDouble(RightOperand.TokenName))
                    {
                        if (DataTypeCheck.IsDate(LeftOperand.TokenName) && DataTypeCheck.IsDate(RightOperand.TokenName))
                        {
                            try
                            {
                                flag = !(LeftOperand.TokenName_DateTime.Subtract(RightOperand.TokenName_DateTime).TotalDays == 0.0);
                                Result = new TokenItem(flag.ToString().ToLower(), CMM.Dynamic.Calculator.Parser.TokenType.Token_Operand, TokenDataType.Token_DataType_Boolean, LeftOperand.InOperandFunction);
                            }
                            catch (Exception exception20)
                            {
                                exception = exception20;
                                ErrorMsg = "Failed to evaluate the Not Equal To operator on date operands: " + exception.Message;
                                return false;
                            }
                        }
                        else if (DataTypeCheck.IsBoolean(LeftOperand.TokenName) && DataTypeCheck.IsBoolean(RightOperand.TokenName))
                        {
                            try
                            {
                                flag = LeftOperand.TokenName_Boolean != RightOperand.TokenName_Boolean;
                                Result = new TokenItem(flag.ToString().ToLower(), CMM.Dynamic.Calculator.Parser.TokenType.Token_Operand, TokenDataType.Token_DataType_Boolean, LeftOperand.InOperandFunction);
                            }
                            catch (Exception exception21)
                            {
                                exception = exception21;
                                ErrorMsg = "Failed to evaluate the Not Equal To operator on boolean operands: " + exception.Message;
                                return false;
                            }
                        }
                        else
                        {
                            try
                            {
                                str = DataTypeCheck.RemoveTextQuotes(LeftOperand.TokenName);
                                str2 = DataTypeCheck.RemoveTextQuotes(RightOperand.TokenName);
                                flag = !str.Equals(str2);
                                Result = new TokenItem(flag.ToString().ToLower(), CMM.Dynamic.Calculator.Parser.TokenType.Token_Operand, TokenDataType.Token_DataType_Boolean, LeftOperand.InOperandFunction);
                            }
                            catch (Exception exception22)
                            {
                                exception = exception22;
                                ErrorMsg = "Failed to evaluate the Not Equal To operator on string operands: " + exception.Message;
                                return false;
                            }
                        }
                    }
                    else
                    {
                        try
                        {
                            flag = !(LeftOperand.TokenName_Double == RightOperand.TokenName_Double);
                            Result = new TokenItem(flag.ToString().ToLower(), CMM.Dynamic.Calculator.Parser.TokenType.Token_Operand, TokenDataType.Token_DataType_Boolean, LeftOperand.InOperandFunction);
                        }
                        catch (Exception exception19)
                        {
                            exception = exception19;
                            ErrorMsg = "Failed to evaluate the Not Equal To operator on double operands: " + exception.Message;
                            return false;
                        }
                    }
                    goto Label_1005;

                case "=":
                    if (!DataTypeCheck.IsDouble(LeftOperand.TokenName) || !DataTypeCheck.IsDouble(RightOperand.TokenName))
                    {
                        if (DataTypeCheck.IsDate(LeftOperand.TokenName) && DataTypeCheck.IsDate(RightOperand.TokenName))
                        {
                            try
                            {
                                flag = LeftOperand.TokenName_DateTime.Subtract(RightOperand.TokenName_DateTime).TotalDays == 0.0;
                                Result = new TokenItem(flag.ToString().ToLower(), CMM.Dynamic.Calculator.Parser.TokenType.Token_Operand, TokenDataType.Token_DataType_Boolean, LeftOperand.InOperandFunction);
                            }
                            catch (Exception exception24)
                            {
                                exception = exception24;
                                ErrorMsg = "Failed to evaluate the Equal To operator on date operands: " + exception.Message;
                                return false;
                            }
                        }
                        else if (DataTypeCheck.IsBoolean(LeftOperand.TokenName) && DataTypeCheck.IsBoolean(RightOperand.TokenName))
                        {
                            try
                            {
                                flag = LeftOperand.TokenName_Boolean == RightOperand.TokenName_Boolean;
                                Result = new TokenItem(flag.ToString().ToLower(), CMM.Dynamic.Calculator.Parser.TokenType.Token_Operand, TokenDataType.Token_DataType_Boolean, LeftOperand.InOperandFunction);
                            }
                            catch (Exception exception25)
                            {
                                exception = exception25;
                                ErrorMsg = "Failed to evaluate the Equal To operator on boolean operands: " + exception.Message;
                                return false;
                            }
                        }
                        else
                        {
                            try
                            {
                                str = DataTypeCheck.RemoveTextQuotes(LeftOperand.TokenName);
                                str2 = DataTypeCheck.RemoveTextQuotes(RightOperand.TokenName);
                                flag = str.Equals(str2);
                                Result = new TokenItem(flag.ToString().ToLower(), CMM.Dynamic.Calculator.Parser.TokenType.Token_Operand, TokenDataType.Token_DataType_Boolean, LeftOperand.InOperandFunction);
                            }
                            catch (Exception exception26)
                            {
                                exception = exception26;
                                ErrorMsg = "Failed to evaluate the Equal To operator on stirng operands: " + exception.Message;
                                return false;
                            }
                        }
                    }
                    else
                    {
                        try
                        {
                            flag = LeftOperand.TokenName_Double == RightOperand.TokenName_Double;
                            Result = new TokenItem(flag.ToString().ToLower(), CMM.Dynamic.Calculator.Parser.TokenType.Token_Operand, TokenDataType.Token_DataType_Boolean, LeftOperand.InOperandFunction);
                        }
                        catch (Exception exception23)
                        {
                            exception = exception23;
                            ErrorMsg = "Failed to evaluate the Equal To operator on double operands: " + exception.Message;
                            return false;
                        }
                    }
                    goto Label_1005;

                case "and":
                    if (!DataTypeCheck.IsBoolean(LeftOperand.TokenName) || !DataTypeCheck.IsBoolean(RightOperand.TokenName))
                    {
                        ErrorMsg = "Syntax Error: Expecting boolean operands to AND.";
                        return false;
                    }
                    try
                    {
                        flag = LeftOperand.TokenName_Boolean && RightOperand.TokenName_Boolean;
                        Result = new TokenItem(flag.ToString().ToLower(), CMM.Dynamic.Calculator.Parser.TokenType.Token_Operand, TokenDataType.Token_DataType_Boolean, LeftOperand.InOperandFunction);
                    }
                    catch (Exception exception27)
                    {
                        exception = exception27;
                        ErrorMsg = "Failed to evaluate the AND operator on boolean operands: " + exception.Message;
                        return false;
                    }
                    goto Label_1005;

                case "or":
                    if (!DataTypeCheck.IsBoolean(LeftOperand.TokenName) || !DataTypeCheck.IsBoolean(RightOperand.TokenName))
                    {
                        ErrorMsg = "Syntax Error: Expecting boolean operands to OR.";
                        return false;
                    }
                    try
                    {
                        flag = LeftOperand.TokenName_Boolean || RightOperand.TokenName_Boolean;
                        Result = new TokenItem(flag.ToString().ToLower(), CMM.Dynamic.Calculator.Parser.TokenType.Token_Operand, TokenDataType.Token_DataType_Boolean, LeftOperand.InOperandFunction);
                    }
                    catch (Exception exception28)
                    {
                        exception = exception28;
                        ErrorMsg = "Failed to evaluate the OR operator on boolean operands: " + exception.Message;
                        return false;
                    }
                    goto Label_1005;

                default:
                    ErrorMsg = "Failed to evaluate the operator: The operator token is null.";
                    return false;
            }
        Label_1005:
            if (Result == null)
            {
                ErrorMsg = "Syntax Error: Failed to evaluate the expression.";
                return false;
            }
            return true;
        }

        private bool IIF(TokenItems Parameters, out TokenItem Result, out string ErrorMsg)
        {
            ErrorMsg = "";
            Result = null;
            if (Parameters.Count != 3)
            {
                ErrorMsg = "Error in operand function IIF[]: Operand Function requires 3 parameter.";
                return false;
            }
            if (!DataTypeCheck.IsBoolean(Parameters[0].TokenName))
            {
                ErrorMsg = "Error in operand function IIF[]: Operand Function requires the first paraemter to be a boolean.";
                return false;
            }
            if (Parameters[0].TokenName_Boolean)
            {
                Result = Parameters[1];
            }
            else
            {
                Result = Parameters[2];
            }
            return true;
        }

        private bool IndexOf(TokenItems Parameters, out TokenItem Result, out string ErrorMsg)
        {
            ErrorMsg = "";
            Result = null;
            if (Parameters.Count <= 1)
            {
                ErrorMsg = "Contains[] Operand Function requires at least 2 parameters.";
                return false;
            }
            string tokenName = Parameters[0].TokenName;
            int num = -1;
            for (int i = 1; i < Parameters.Count; i++)
            {
                if (DataTypeCheck.RemoveTextQuotes(Parameters[i].TokenName) == tokenName)
                {
                    num = i - 1;
                    break;
                }
            }
            Result = new TokenItem(num.ToString(), CMM.Dynamic.Calculator.Parser.TokenType.Token_Operand, TokenDataType.Token_DataType_Int, false);
            return true;
        }

        private bool IsAllDigits(TokenItems Parameters, out TokenItem Result, out string ErrorMsg)
        {
            ErrorMsg = "";
            Result = null;
            if (Parameters.Count != 1)
            {
                ErrorMsg = "Error in operand function IsAllDigits[]: Operand Function requires 1 parameter.";
                return false;
            }
            string str = DataTypeCheck.RemoveTextQuotes(Parameters[0].TokenName);
            bool flag = true;
            foreach (char ch in str)
            {
                if (!char.IsDigit(ch))
                {
                    flag = false;
                    break;
                }
            }
            Result = new TokenItem(flag.ToString().ToLower(), CMM.Dynamic.Calculator.Parser.TokenType.Token_Operand, TokenDataType.Token_DataType_Boolean, false);
            return true;
        }

        private bool IsFalseOrNull(TokenItems Parameters, out TokenItem Result, out string ErrorMsg)
        {
            ErrorMsg = "";
            Result = null;
            if (Parameters.Count != 1)
            {
                ErrorMsg = "IsFalseOrNull[] Operand Function requires 1 parameter.";
                return false;
            }
            string str = DataTypeCheck.RemoveTextQuotes(Parameters[0].TokenName).Trim();
            bool flag = true;
            if (!string.IsNullOrEmpty(str))
            {
                flag = str.ToLower() == "false";
            }
            Result = new TokenItem(flag.ToString().ToLower(), CMM.Dynamic.Calculator.Parser.TokenType.Token_Operand, TokenDataType.Token_DataType_Boolean, false);
            return true;
        }

        private bool IsNullOrEmpty(TokenItems Parameters, out TokenItem Result, out string ErrorMsg)
        {
            ErrorMsg = "";
            Result = null;
            if (Parameters.Count != 1)
            {
                ErrorMsg = "IsNullOrEmpty[] Operand Function requires 1 parameter.";
                return false;
            }
            if (DataTypeCheck.IsNULL(Parameters[0].TokenName))
            {
                Result = new TokenItem("true", CMM.Dynamic.Calculator.Parser.TokenType.Token_Operand, TokenDataType.Token_DataType_Boolean, false);
            }
            else
            {
                string str = DataTypeCheck.RemoveTextQuotes(Parameters[0].TokenName).Trim().ToLower();
                if (str == "null")
                {
                    Result = new TokenItem("true", CMM.Dynamic.Calculator.Parser.TokenType.Token_Operand, TokenDataType.Token_DataType_Boolean, false);
                }
                else
                {
                    bool flag = string.IsNullOrEmpty(str);
                    Result = new TokenItem(flag.ToString().ToLower(), CMM.Dynamic.Calculator.Parser.TokenType.Token_Operand, TokenDataType.Token_DataType_Boolean, false);
                }
            }
            return true;
        }

        private bool IsTrueOrNull(TokenItems Parameters, out TokenItem Result, out string ErrorMsg)
        {
            ErrorMsg = "";
            Result = null;
            if (Parameters.Count != 1)
            {
                ErrorMsg = "IsTrueOrNull[] Operand Function requires 1 parameter.";
                return false;
            }
            string str = DataTypeCheck.RemoveTextQuotes(Parameters[0].TokenName).Trim();
            bool flag = true;
            if (!string.IsNullOrEmpty(str))
            {
                flag = str.ToLower() == "true";
            }
            Result = new TokenItem(flag.ToString().ToLower(), CMM.Dynamic.Calculator.Parser.TokenType.Token_Operand, TokenDataType.Token_DataType_Boolean, false);
            return true;
        }

        private bool Join(TokenItems Parameters, out TokenItem Result, out string ErrorMsg)
        {
            ErrorMsg = "";
            Result = null;
            if (Parameters.Count <= 1)
            {
                ErrorMsg = "Join[] Operand Function requires at least 2 parameters.";
                return false;
            }
            string str = DataTypeCheck.RemoveTextQuotes(Parameters[0].TokenName);
            string str2 = "";
            for (int i = 1; i < Parameters.Count; i++)
            {
                str2 = str2 + DataTypeCheck.RemoveTextQuotes(Parameters[i].TokenName);
                if (i != (Parameters.Count - 1))
                {
                    str2 = str2 + str;
                }
            }
            Result = new TokenItem("\"" + str2 + "\"", CMM.Dynamic.Calculator.Parser.TokenType.Token_Operand, TokenDataType.Token_DataType_String, false);
            return true;
        }

        private bool LCase(TokenItems Parameters, out TokenItem Result, out string ErrorMsg)
        {
            ErrorMsg = "";
            Result = null;
            if (Parameters.Count != 1)
            {
                ErrorMsg = "Error in operand function LCase[]: Operand Function requires 1 parameter.";
                return false;
            }
            Result = new TokenItem(Parameters[0].TokenName.ToLower(), CMM.Dynamic.Calculator.Parser.TokenType.Token_Operand, TokenDataType.Token_DataType_String, false);
            return true;
        }

        private bool Left(TokenItems Parameters, out TokenItem Result, out string ErrorMsg)
        {
            ErrorMsg = "";
            Result = null;
            if (Parameters.Count != 2)
            {
                ErrorMsg = "Error in operand function Left[]: Operand Function requires 2 parameter.";
                return false;
            }
            bool flag = DataTypeCheck.IsText(Parameters[0].TokenName);
            string str = DataTypeCheck.RemoveTextQuotes(Parameters[0].TokenName);
            if (!DataTypeCheck.IsInteger(Parameters[1].TokenName))
            {
                ErrorMsg = "Error in operand function Left[]: Operand Function requires an integer as the second parameter.";
                return false;
            }
            int length = Parameters[1].TokenName_Int;
            if (length > str.Length)
            {
                ErrorMsg = "Error in operand function Left[]: Operand Function requires an integer as the second parameter.";
                return false;
            }
            string tokenName = str.Substring(0, length);
            if (flag)
            {
                tokenName = "\"" + tokenName + "\"";
            }
            Result = new TokenItem(tokenName, CMM.Dynamic.Calculator.Parser.TokenType.Token_Operand, TokenDataType.Token_DataType_String, false);
            return true;
        }

        private bool Length(TokenItems Parameters, out TokenItem Result, out string ErrorMsg)
        {
            ErrorMsg = "";
            Result = null;
            if (Parameters.Count != 1)
            {
                ErrorMsg = "Error in operand function Length[]: Operand Function requires 1 parameter.";
                return false;
            }
            string str = DataTypeCheck.RemoveTextQuotes(Parameters[0].TokenName);
            Result = new TokenItem(str.Length.ToString(), CMM.Dynamic.Calculator.Parser.TokenType.Token_Operand, TokenDataType.Token_DataType_Int, false);
            return true;
        }

        private bool LPad(TokenItems Parameters, out TokenItem Result, out string ErrorMsg)
        {
            ErrorMsg = "";
            Result = null;
            if (Parameters.Count != 3)
            {
                ErrorMsg = "RPad[] Operand Function requires 3 parameter.";
                return false;
            }
            string str = DataTypeCheck.RemoveTextQuotes(Parameters[0].TokenName);
            string str2 = DataTypeCheck.RemoveTextQuotes(Parameters[1].TokenName);
            if (!DataTypeCheck.IsInteger(Parameters[2].TokenName))
            {
                ErrorMsg = "RPad[] Operand Function requires the 3rd parameter to be an integer.";
                return false;
            }
            int num = Parameters[2].TokenName_Int;
            string str3 = "";
            for (int i = 0; i < num; i++)
            {
                str3 = str3 + str2;
            }
            str3 = str3 + str;
            Result = new TokenItem("\"" + str3 + "\"", CMM.Dynamic.Calculator.Parser.TokenType.Token_Operand, TokenDataType.Token_DataType_String, false);
            return true;
        }

        private bool LTrim(TokenItems Parameters, out TokenItem Result, out string ErrorMsg)
        {
            ErrorMsg = "";
            Result = null;
            if (Parameters.Count != 1)
            {
                ErrorMsg = "LTrim[] Operand Function requires 1 parameter.";
                return false;
            }
            bool flag = DataTypeCheck.IsText(Parameters[0].TokenName);
            string tokenName = DataTypeCheck.RemoveTextQuotes(Parameters[0].TokenName).TrimStart(new char[0]);
            if (flag)
            {
                tokenName = "\"" + tokenName + "\"";
            }
            Result = new TokenItem(tokenName, CMM.Dynamic.Calculator.Parser.TokenType.Token_Operand, TokenDataType.Token_DataType_String, false);
            return true;
        }

        private bool Mid(TokenItems Parameters, out TokenItem Result, out string ErrorMsg)
        {
            ErrorMsg = "";
            Result = null;
            if (Parameters.Count == 0)
            {
                ErrorMsg = "Mid[] Operand Function requires at least 1 parameter.";
                return false;
            }
            double[] array = new double[Parameters.Count];
            int index = 0;
            foreach (TokenItem item in Parameters)
            {
                if (DataTypeCheck.IsDouble(item.TokenName))
                {
                    array[index] = item.TokenName_Double;
                    index++;
                }
                else
                {
                    ErrorMsg = "Mid[] Operand Function can only calculate the middle of parameters that can be converted to double.";
                    return false;
                }
            }
            Array.Sort<double>(array);
            double num2 = 0.0;
            double d = ((array.Length + 1) / 2) - 1;
            int num4 = Convert.ToInt32(Math.Floor(d));
            if ((array.Length % 2) == 0)
            {
                double num5 = array[num4];
                double num6 = array[num4 + 1];
                num2 = (num5 + num6) / 2.0;
            }
            else
            {
                num2 = array[num4];
            }
            Result = new TokenItem(num2.ToString(), CMM.Dynamic.Calculator.Parser.TokenType.Token_Operand, TokenDataType.Token_DataType_Double, false);
            return true;
        }

        private bool Month(TokenItems Parameters, out TokenItem Result, out string ErrorMsg)
        {
            ErrorMsg = "";
            Result = null;
            if (Parameters.Count != 1)
            {
                ErrorMsg = "Error in operand function Month[]: Operand Function requires 1 parameter.";
                return false;
            }
            if (DataTypeCheck.IsDate(Parameters[0].TokenName))
            {
                int month = Parameters[0].TokenName_DateTime.Month;
                Result = new TokenItem(month.ToString(), CMM.Dynamic.Calculator.Parser.TokenType.Token_Operand, TokenDataType.Token_DataType_Int, false);
            }
            else
            {
                ErrorMsg = "Error in operand function Avg[]: Operand Function requires 1 parameter that can be converted to a date time.";
                return false;
            }
            return true;
        }

        private bool NewDate(TokenItems Parameters, out TokenItem Result, out string ErrorMsg)
        {
            ErrorMsg = "";
            Result = null;
            if (Parameters.Count != 3)
            {
                ErrorMsg = "Error in operand function NewDate[]: Operand Function requires 3 parameter.";
                return false;
            }
            for (int i = 0; i < 3; i++)
            {
                if (!DataTypeCheck.IsInteger(Parameters[i].TokenName))
                {
                    ErrorMsg = "Error in operand function NewDate[]: Operand Function requires all 3 paraemters to be integers.";
                    return false;
                }
            }
            DateTime minValue = DateTime.MinValue;
            try
            {
                minValue = new DateTime(Parameters[2].TokenName_Int, Parameters[0].TokenName_Int, Parameters[1].TokenName_Int);
            }
            catch
            {
                ErrorMsg = "Error in operand function NewDate[]: Invalid Date";
                return false;
            }
            Result = new TokenItem(minValue.ToString("M.d.yyyy"), CMM.Dynamic.Calculator.Parser.TokenType.Token_Operand, TokenDataType.Token_DataType_Date, false);
            return true;
        }

        private bool Not(TokenItems Parameters, out TokenItem Result, out string ErrorMsg)
        {
            ErrorMsg = "";
            Result = null;
            if (Parameters.Count != 1)
            {
                ErrorMsg = "Error in operand function Not[]: Operand Function requires 1 parameter.";
                return false;
            }
            if (DataTypeCheck.IsBoolean(Parameters[0].TokenName))
            {
                bool flag = !Parameters[0].TokenName_Boolean;
                Result = new TokenItem(flag.ToString().ToLower(), CMM.Dynamic.Calculator.Parser.TokenType.Token_Operand, TokenDataType.Token_DataType_Boolean, false);
            }
            else
            {
                ErrorMsg = "Error in operand function Not[]: Operand Function can only evaluate parameters that are boolean.";
                return false;
            }
            return true;
        }

        private bool Now(TokenItems Parameters, out TokenItem Result, out string ErrorMsg)
        {
            ErrorMsg = "";
            Result = null;
            Result = new TokenItem(DateTime.Now.ToString("M.d.yyyy"), CMM.Dynamic.Calculator.Parser.TokenType.Token_Operand, TokenDataType.Token_DataType_Date, false);
            return true;
        }

        private bool NumericMax(TokenItems Parameters, out TokenItem Result, out string ErrorMsg)
        {
            ErrorMsg = "";
            Result = null;
            if (Parameters.Count == 0)
            {
                ErrorMsg = "Error in operand function NumericMax[]: Operand Function requires at least 1 parameter.";
                return false;
            }
            double num = 0.0;
            bool flag = true;
            foreach (TokenItem item in Parameters)
            {
                if (DataTypeCheck.IsDouble(item.TokenName))
                {
                    if (flag)
                    {
                        num = item.TokenName_Double;
                        flag = false;
                    }
                    else if (item.TokenName_Double > num)
                    {
                        num = item.TokenName_Double;
                    }
                }
                else
                {
                    ErrorMsg = "Error in operand function NumericMax[]: Operand Function expects that all parameters can be converted to double.";
                    return false;
                }
            }
            Result = new TokenItem(num.ToString(), CMM.Dynamic.Calculator.Parser.TokenType.Token_Operand, TokenDataType.Token_DataType_Double, false);
            return true;
        }

        private bool NumericMin(TokenItems Parameters, out TokenItem Result, out string ErrorMsg)
        {
            ErrorMsg = "";
            Result = null;
            if (Parameters.Count == 0)
            {
                ErrorMsg = "Error in operand function NumericMin[]: Operand Function requires at least 1 parameter.";
                return false;
            }
            double num = 0.0;
            bool flag = true;
            foreach (TokenItem item in Parameters)
            {
                if (DataTypeCheck.IsDouble(item.TokenName))
                {
                    if (flag)
                    {
                        num = item.TokenName_Double;
                        flag = false;
                    }
                    else if (item.TokenName_Double < num)
                    {
                        num = item.TokenName_Double;
                    }
                }
                else
                {
                    ErrorMsg = "Error in operand function NumericMin[]: Operand Function expects that all parameters can be converted to double.";
                    return false;
                }
            }
            Result = new TokenItem(num.ToString(), CMM.Dynamic.Calculator.Parser.TokenType.Token_Operand, TokenDataType.Token_DataType_Double, false);
            return true;
        }

        private bool PCase(TokenItems Parameters, out TokenItem Result, out string ErrorMsg)
        {
            ErrorMsg = "";
            Result = null;
            if (Parameters.Count != 1)
            {
                ErrorMsg = "Error in operand function PCase[]: Operand Function requires 1 parameter.";
                return false;
            }
            string str = DataTypeCheck.RemoveTextQuotes(Parameters[0].TokenName.ToLower().Trim());
            string str2 = str.Substring(0, 1).ToUpper() + str.Substring(1, str.Length - 1);
            Result = new TokenItem("\"" + str2 + "\"", CMM.Dynamic.Calculator.Parser.TokenType.Token_Operand, TokenDataType.Token_DataType_String, false);
            return true;
        }

        private bool Remove(TokenItems Parameters, out TokenItem Result, out string ErrorMsg)
        {
            ErrorMsg = "";
            Result = null;
            if (Parameters.Count != 2)
            {
                ErrorMsg = "InsertOnSubmit[] Operand Function requires 2 text parameter.";
                return false;
            }
            string[] strArray = Parameters[0].TokenName.Split(DataTypeCheck.RemoveTextQuotes(Parameters[1].TokenName.ToString()).ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < strArray.Length; i++)
            {
                builder.Append(strArray[i]);
            }
            Result = new TokenItem(builder.ToString(), CMM.Dynamic.Calculator.Parser.TokenType.Token_Operand, TokenDataType.Token_DataType_String, false);
            return true;
        }

        private bool Replace(TokenItems Parameters, out TokenItem Result, out string ErrorMsg)
        {
            ErrorMsg = "";
            Result = null;
            if (Parameters.Count != 3)
            {
                ErrorMsg = "Replace[] Operand Function requires 3 text parameter.";
                return false;
            }
            string tokenName = Parameters[0].TokenName;
            string oldValue = DataTypeCheck.RemoveTextQuotes(Parameters[1].TokenName.ToString());
            string newValue = DataTypeCheck.RemoveTextQuotes(Parameters[2].TokenName.ToString());
            string str4 = tokenName.Replace(oldValue, newValue);
            Result = new TokenItem(str4, CMM.Dynamic.Calculator.Parser.TokenType.Token_Operand, TokenDataType.Token_DataType_String, false);
            return true;
        }

        private bool Right(TokenItems Parameters, out TokenItem Result, out string ErrorMsg)
        {
            ErrorMsg = "";
            Result = null;
            if (Parameters.Count != 2)
            {
                ErrorMsg = "Right[] Operand Function requires 2 parameter.";
                return false;
            }
            bool flag = DataTypeCheck.IsText(Parameters[0].TokenName);
            string str = DataTypeCheck.RemoveTextQuotes(Parameters[0].TokenName);
            if (!DataTypeCheck.IsInteger(Parameters[1].TokenName))
            {
                ErrorMsg = "Right[] Operand Function requires an integer as the second parameter.";
                return false;
            }
            int length = Parameters[1].TokenName_Int;
            if (length > str.Length)
            {
                ErrorMsg = "Right[] Operand Function requires an integer as the second parameter.";
                return false;
            }
            string tokenName = str.Substring(str.Length - length, length);
            if (flag)
            {
                tokenName = "\"" + tokenName + "\"";
            }
            Result = new TokenItem(tokenName, CMM.Dynamic.Calculator.Parser.TokenType.Token_Operand, TokenDataType.Token_DataType_String, false);
            return true;
        }

        private bool Round(TokenItems Parameters, out TokenItem Result, out string ErrorMsg)
        {
            ErrorMsg = "";
            Result = null;
            if (Parameters.Count != 2)
            {
                ErrorMsg = "Round[] Operand Function requires 2 parameter.";
                return false;
            }
            if (!DataTypeCheck.IsDouble(Parameters[0].TokenName))
            {
                ErrorMsg = "Round[] Operand Function requires the first parameter to be a double.";
                return false;
            }
            if (!DataTypeCheck.IsInteger(Parameters[1].TokenName))
            {
                ErrorMsg = "Round[] Operand Function requires the second parameter to be a integer.";
                return false;
            }
            double num = Parameters[0].TokenName_Double;
            int digits = Parameters[1].TokenName_Int;
            if (digits < 0)
            {
                ErrorMsg = "Round[] Operand Function requires the second parameter to be a positive integer.";
                return false;
            }
            double num3 = Math.Round(num, digits);
            string format = "#";
            if (digits > 0)
            {
                format = format + ".";
                for (int i = 0; i < digits; i++)
                {
                    format = format + "#";
                }
            }
            Result = new TokenItem(num3.ToString(format), CMM.Dynamic.Calculator.Parser.TokenType.Token_Operand, TokenDataType.Token_DataType_Double, false);
            return true;
        }

        private bool RPad(TokenItems Parameters, out TokenItem Result, out string ErrorMsg)
        {
            ErrorMsg = "";
            Result = null;
            if (Parameters.Count != 3)
            {
                ErrorMsg = "RPad[] Operand Function requires 3 parameter.";
                return false;
            }
            string str = DataTypeCheck.RemoveTextQuotes(Parameters[0].TokenName);
            string str2 = DataTypeCheck.RemoveTextQuotes(Parameters[1].TokenName);
            if (!DataTypeCheck.IsInteger(Parameters[2].TokenName))
            {
                ErrorMsg = "RPad[] Operand Function requires the 3rd parameter to be an integer.";
                return false;
            }
            int num = Parameters[2].TokenName_Int;
            string str3 = str;
            for (int i = 0; i < num; i++)
            {
                str3 = str3 + str2;
            }
            Result = new TokenItem("\"" + str3 + "\"", CMM.Dynamic.Calculator.Parser.TokenType.Token_Operand, TokenDataType.Token_DataType_String, false);
            return true;
        }

        private bool RTrim(TokenItems Parameters, out TokenItem Result, out string ErrorMsg)
        {
            ErrorMsg = "";
            Result = null;
            if (Parameters.Count != 1)
            {
                ErrorMsg = "RTrim[] Operand Function requires 1 parameter.";
                return false;
            }
            bool flag = DataTypeCheck.IsText(Parameters[0].TokenName);
            string tokenName = DataTypeCheck.RemoveTextQuotes(Parameters[0].TokenName).TrimEnd(new char[0]);
            if (flag)
            {
                tokenName = "\"" + tokenName + "\"";
            }
            Result = new TokenItem(tokenName, CMM.Dynamic.Calculator.Parser.TokenType.Token_Operand, TokenDataType.Token_DataType_String, false);
            return true;
        }

        private bool SearchString(TokenItems Parameters, out TokenItem Result, out string ErrorMsg)
        {
            ErrorMsg = "";
            Result = null;
            if (Parameters.Count != 3)
            {
                ErrorMsg = "SearchString[] Operand Function requires 3 parameter.";
                return false;
            }
            string str = DataTypeCheck.RemoveTextQuotes(Parameters[0].TokenName);
            if (!DataTypeCheck.IsInteger(Parameters[1].TokenName))
            {
                ErrorMsg = "SearchString[] Operand Function requires an integer as the second parameter.";
                return false;
            }
            int startIndex = Parameters[1].TokenName_Int;
            string str2 = DataTypeCheck.RemoveTextQuotes(Parameters[2].TokenName);
            int index = str.IndexOf(str2, startIndex);
            Result = new TokenItem(index.ToString(), CMM.Dynamic.Calculator.Parser.TokenType.Token_Operand, TokenDataType.Token_DataType_Int, false);
            return true;
        }

        private bool Sin(TokenItems Parameters, out TokenItem Result, out string ErrorMsg)
        {
            ErrorMsg = "";
            Result = null;
            if (Parameters.Count != 1)
            {
                ErrorMsg = "Sin[] Operand Function requires 1 parameter.";
                return false;
            }
            if (DataTypeCheck.IsDouble(Parameters[0].TokenName))
            {
                double num2 = Math.Sin(Parameters[0].TokenName_Double);
                Result = new TokenItem(num2.ToString(), CMM.Dynamic.Calculator.Parser.TokenType.Token_Operand, TokenDataType.Token_DataType_Double, false);
            }
            else
            {
                ErrorMsg = "Sin[] can only evaluate parameters that can be converted to a double.";
                return false;
            }
            return true;
        }

        private bool Sqrt(TokenItems Parameters, out TokenItem Result, out string ErrorMsg)
        {
            ErrorMsg = "";
            Result = null;
            if (Parameters.Count != 1)
            {
                ErrorMsg = "Sqrt[] Operand Function requires 1 parameter.";
                return false;
            }
            if (DataTypeCheck.IsDouble(Parameters[0].TokenName))
            {
                double num2 = Math.Sqrt(Parameters[0].TokenName_Double);
                Result = new TokenItem(num2.ToString(), CMM.Dynamic.Calculator.Parser.TokenType.Token_Operand, TokenDataType.Token_DataType_Double, false);
            }
            else
            {
                ErrorMsg = "Sqrt[] can only evaluate parameters that can be converted to a double.";
                return false;
            }
            return true;
        }

        private bool StringMax(TokenItems Parameters, out TokenItem Result, out string ErrorMsg)
        {
            ErrorMsg = "";
            Result = null;
            if (Parameters.Count == 0)
            {
                ErrorMsg = "StringMax[] Operand Function requires at least 1 parameter.";
                return false;
            }
            string str = "";
            bool flag = true;
            foreach (TokenItem item in Parameters)
            {
                if (flag)
                {
                    str = DataTypeCheck.RemoveTextQuotes(item.TokenName);
                    flag = false;
                }
                else
                {
                    string strB = DataTypeCheck.RemoveTextQuotes(item.TokenName);
                    if (str.CompareTo(strB) < 0)
                    {
                        str = strB;
                    }
                }
            }
            Result = new TokenItem("\"" + str + "\"", CMM.Dynamic.Calculator.Parser.TokenType.Token_Operand, TokenDataType.Token_DataType_String, false);
            return true;
        }

        private bool StringMin(TokenItems Parameters, out TokenItem Result, out string ErrorMsg)
        {
            ErrorMsg = "";
            Result = null;
            if (Parameters.Count == 0)
            {
                ErrorMsg = "StringMax[] Operand Function requires at least 1 parameter.";
                return false;
            }
            string str = "";
            bool flag = true;
            foreach (TokenItem item in Parameters)
            {
                if (flag)
                {
                    str = DataTypeCheck.RemoveTextQuotes(item.TokenName);
                    flag = false;
                }
                else
                {
                    string strB = DataTypeCheck.RemoveTextQuotes(item.TokenName);
                    if (str.CompareTo(strB) > 0)
                    {
                        str = strB;
                    }
                }
            }
            Result = new TokenItem("\"" + str + "\"", CMM.Dynamic.Calculator.Parser.TokenType.Token_Operand, TokenDataType.Token_DataType_String, false);
            return true;
        }

        private bool SubString(TokenItems Parameters, out TokenItem Result, out string ErrorMsg)
        {
            ErrorMsg = "";
            Result = null;
            if (Parameters.Count != 3)
            {
                ErrorMsg = "SubString[] Operand Function requires 3 parameter.";
                return false;
            }
            string str = DataTypeCheck.RemoveTextQuotes(Parameters[0].TokenName);
            if (!DataTypeCheck.IsInteger(Parameters[1].TokenName))
            {
                ErrorMsg = "SubString[] Operand Function requires an integer as the second parameter.";
                return false;
            }
            int startIndex = Parameters[1].TokenName_Int;
            if (!DataTypeCheck.IsInteger(Parameters[2].TokenName))
            {
                ErrorMsg = "SubString[] Operand Function requires an integer as the third parameter.";
                return false;
            }
            int length = Parameters[2].TokenName_Int;
            if ((startIndex + length) > str.Length)
            {
                ErrorMsg = "SubString[] Operand Function: The start position and length cannot be longer than the string.";
                return false;
            }
            string tokenName = str.Substring(startIndex, length);
            tokenName = "\"" + tokenName + "\"";
            Result = new TokenItem(tokenName, CMM.Dynamic.Calculator.Parser.TokenType.Token_Operand, TokenDataType.Token_DataType_String, false);
            return true;
        }

        private bool Trim(TokenItems Parameters, out TokenItem Result, out string ErrorMsg)
        {
            ErrorMsg = "";
            Result = null;
            if (Parameters.Count != 1)
            {
                ErrorMsg = "Trim[] Operand Function requires 1 parameter.";
                return false;
            }
            bool flag = DataTypeCheck.IsText(Parameters[0].TokenName);
            string tokenName = DataTypeCheck.RemoveTextQuotes(Parameters[0].TokenName).Trim();
            if (flag)
            {
                tokenName = "\"" + tokenName + "\"";
            }
            Result = new TokenItem(tokenName, CMM.Dynamic.Calculator.Parser.TokenType.Token_Operand, TokenDataType.Token_DataType_String, false);
            return true;
        }

        private bool UCase(TokenItems Parameters, out TokenItem Result, out string ErrorMsg)
        {
            ErrorMsg = "";
            Result = null;
            if (Parameters.Count != 1)
            {
                ErrorMsg = "UCase[] Operand Function requires 1 parameter.";
                return false;
            }
            Result = new TokenItem(Parameters[0].TokenName.ToUpper(), CMM.Dynamic.Calculator.Parser.TokenType.Token_Operand, TokenDataType.Token_DataType_String, false);
            return true;
        }

        private bool Year(TokenItems Parameters, out TokenItem Result, out string ErrorMsg)
        {
            ErrorMsg = "";
            Result = null;
            if (Parameters.Count != 1)
            {
                ErrorMsg = "Error in operand function Year[]: Operand Function requires 1 parameter.";
                return false;
            }
            if (DataTypeCheck.IsDate(Parameters[0].TokenName))
            {
                int year = Parameters[0].TokenName_DateTime.Year;
                Result = new TokenItem(year.ToString(), CMM.Dynamic.Calculator.Parser.TokenType.Token_Operand, TokenDataType.Token_DataType_Int, false);
            }
            else
            {
                ErrorMsg = "Error in operand function Year[]: Operand Function requires 1 parameter that can be converted to date time.";
                return false;
            }
            return true;
        }

        public double TokenEvalTime
        {
            get
            {
                return this.tokenEvalTime;
            }
        }
    }
}

