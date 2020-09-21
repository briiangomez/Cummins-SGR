namespace CMM.Dynamic.Calculator.Parser
{
    using CMM.Dynamic.Calculator.Support;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Runtime.InteropServices;
    using System.Text;

    public class Token
    {
        private bool anyAssignments;
        private int charIndex;
        private string lastErrorMessage;
        private string lastEvaluationResult;
        private double lastEvaluationTime;
        private ExQueue<TokenItem> rpn_queue;
        private string ruleSyntax;
        private CMM.Dynamic.Calculator.Parser.TokenGroup tokenGroup;
        private CMM.Dynamic.Calculator.Parser.TokenItems tokenItems;
        private double tokenParseTime;
        private CMM.Dynamic.Calculator.Parser.Variables variables;

        public Token(FileInfo Filename)
        {
            this.tokenItems = null;
            this.variables = new CMM.Dynamic.Calculator.Parser.Variables();
            this.rpn_queue = null;
            this.ruleSyntax = "";
            this.lastErrorMessage = "";
            this.tokenParseTime = 0.0;
            this.lastEvaluationTime = 0.0;
            this.charIndex = 0;
            this.lastEvaluationResult = "";
            this.tokenGroup = null;
            this.anyAssignments = false;
            this.tokenItems = new CMM.Dynamic.Calculator.Parser.TokenItems(this);
            string errorMsg = "";
            if (!this.Open(Filename, out errorMsg))
            {
                throw new Exception(errorMsg);
            }
        }

        public Token(string RuleSyntax)
        {
            this.tokenItems = null;
            this.variables = new CMM.Dynamic.Calculator.Parser.Variables();
            this.rpn_queue = null;
            this.ruleSyntax = "";
            this.lastErrorMessage = "";
            this.tokenParseTime = 0.0;
            this.lastEvaluationTime = 0.0;
            this.charIndex = 0;
            this.lastEvaluationResult = "";
            this.tokenGroup = null;
            this.anyAssignments = false;
            this.tokenItems = new CMM.Dynamic.Calculator.Parser.TokenItems(this);
            this.ruleSyntax = RuleSyntax.Trim();
            this.lastErrorMessage = "";
            this.GetTokens();
        }

        private bool CreateTokenItem(string CurrentToken, bool WaitForCreation, bool InOperandFunction, out ParseState NextParseState, out bool IsError, out string ErrorMsg)
        {
            char ch;
            Exception exception;
            string str3;
            NextParseState = ParseState.Parse_State_Comment;
            ErrorMsg = "";
            IsError = false;
            if (string.IsNullOrEmpty(CurrentToken))
            {
                return false;
            }
            CurrentToken = CurrentToken.Trim();
            if (string.IsNullOrEmpty(CurrentToken))
            {
                return false;
            }
            string sOperand = "";
            string sOperator = "";
            if (!DataTypeCheck.IsOperator(CurrentToken))
            {
                if (!DataTypeCheck.ContainsOperator(CurrentToken, out sOperand, out sOperator))
                {
                    if (WaitForCreation)
                    {
                        return false;
                    }
                    if (DataTypeCheck.IsReservedWord(CurrentToken))
                    {
                        IsError = true;
                        ErrorMsg = "The operand \"" + CurrentToken + "\" is a reserved word.";
                        return false;
                    }
                    if (DataTypeCheck.IsInteger(CurrentToken))
                    {
                        this.tokenItems.Add(new TokenItem(CurrentToken, CMM.Dynamic.Calculator.Parser.TokenType.Token_Operand, TokenDataType.Token_DataType_Int, InOperandFunction));
                    }
                    else if (DataTypeCheck.IsDouble(CurrentToken))
                    {
                        this.tokenItems.Add(new TokenItem(CurrentToken, CMM.Dynamic.Calculator.Parser.TokenType.Token_Operand, TokenDataType.Token_DataType_Double, InOperandFunction));
                    }
                    else if (DataTypeCheck.IsDate(CurrentToken))
                    {
                        this.tokenItems.Add(new TokenItem(CurrentToken, CMM.Dynamic.Calculator.Parser.TokenType.Token_Operand, TokenDataType.Token_DataType_Date, InOperandFunction));
                    }
                    else if (DataTypeCheck.IsBoolean(CurrentToken))
                    {
                        this.tokenItems.Add(new TokenItem(CurrentToken, CMM.Dynamic.Calculator.Parser.TokenType.Token_Operand, TokenDataType.Token_DataType_Boolean, InOperandFunction));
                    }
                    else if (DataTypeCheck.IsNULL(CurrentToken))
                    {
                        this.tokenItems.Add(new TokenItem(CurrentToken, CMM.Dynamic.Calculator.Parser.TokenType.Token_Operand, TokenDataType.Token_DataType_NULL, InOperandFunction));
                    }
                    else if (DataTypeCheck.IsText(CurrentToken))
                    {
                        this.tokenItems.Add(new TokenItem(CurrentToken, CMM.Dynamic.Calculator.Parser.TokenType.Token_Operand, TokenDataType.Token_DataType_String, InOperandFunction));
                    }
                    else
                    {
                        this.tokenItems.Add(new TokenItem(CurrentToken, CMM.Dynamic.Calculator.Parser.TokenType.Token_Operand, TokenDataType.Token_DataType_Variable, InOperandFunction));
                        this.variables.Add(CurrentToken);
                    }
                    NextParseState = ParseState.Parse_State_Operator;
                    goto Label_0611;
                }
                if (DataTypeCheck.IsReservedWord(sOperand))
                {
                    IsError = true;
                    ErrorMsg = "The operand \"" + sOperand + "\" is a reserved word.";
                    return false;
                }
                if (DataTypeCheck.IsInteger(sOperand))
                {
                    this.tokenItems.Add(new TokenItem(sOperand, CMM.Dynamic.Calculator.Parser.TokenType.Token_Operand, TokenDataType.Token_DataType_Int, InOperandFunction));
                }
                else if (DataTypeCheck.IsDouble(sOperand))
                {
                    this.tokenItems.Add(new TokenItem(sOperand, CMM.Dynamic.Calculator.Parser.TokenType.Token_Operand, TokenDataType.Token_DataType_Double, InOperandFunction));
                }
                else if (DataTypeCheck.IsDate(sOperand))
                {
                    this.tokenItems.Add(new TokenItem(sOperand, CMM.Dynamic.Calculator.Parser.TokenType.Token_Operand, TokenDataType.Token_DataType_Date, InOperandFunction));
                }
                else if (DataTypeCheck.IsBoolean(sOperand))
                {
                    this.tokenItems.Add(new TokenItem(sOperand, CMM.Dynamic.Calculator.Parser.TokenType.Token_Operand, TokenDataType.Token_DataType_Boolean, InOperandFunction));
                }
                else if (DataTypeCheck.IsNULL(sOperand))
                {
                    this.tokenItems.Add(new TokenItem(sOperand, CMM.Dynamic.Calculator.Parser.TokenType.Token_Operand, TokenDataType.Token_DataType_NULL, InOperandFunction));
                }
                else
                {
                    this.tokenItems.Add(new TokenItem(sOperand, CMM.Dynamic.Calculator.Parser.TokenType.Token_Operand, TokenDataType.Token_DataType_Variable, InOperandFunction));
                    this.variables.Add(sOperand);
                }
                sOperator = sOperator.Trim();
                str3 = sOperator.Trim().ToLower();
                if (str3 != null)
                {
                    if (!(str3 == "<"))
                    {
                        if (str3 == ">")
                        {
                            try
                            {
                                if (this.charIndex < (this.ruleSyntax.Length - 1))
                                {
                                    ch = this.ruleSyntax[this.charIndex];
                                    if (ch == '=')
                                    {
                                        sOperator = sOperator + '=';
                                        this.charIndex++;
                                    }
                                }
                            }
                            catch (Exception exception4)
                            {
                                exception = exception4;
                                IsError = true;
                                ErrorMsg = "Error while determining if the next character is >, location = 4, Error message = " + exception.Message;
                                return false;
                            }
                        }
                    }
                    else
                    {
                        try
                        {
                            if (this.charIndex < (this.ruleSyntax.Length - 1))
                            {
                                ch = this.ruleSyntax[this.charIndex];
                                switch (ch)
                                {
                                    case '=':
                                        sOperator = sOperator + '=';
                                        this.charIndex++;
                                        goto Label_049B;

                                    case '>':
                                        sOperator = sOperator + '>';
                                        this.charIndex++;
                                        break;
                                }
                            }
                        }
                        catch (Exception exception3)
                        {
                            exception = exception3;
                            IsError = true;
                            ErrorMsg = "Error while determining if the next character is <, location = 3, Error message = " + exception.Message;
                            return false;
                        }
                    }
                }
                goto Label_049B;
            }
            sOperator = CurrentToken;
            str3 = CurrentToken;
            if (str3 != null)
            {
                if (!(str3 == "<"))
                {
                    if (str3 == ">")
                    {
                        try
                        {
                            if (this.charIndex < (this.ruleSyntax.Length - 1))
                            {
                                ch = this.ruleSyntax[this.charIndex];
                                if (ch == '=')
                                {
                                    sOperator = sOperator + '=';
                                    this.charIndex++;
                                }
                            }
                        }
                        catch (Exception exception2)
                        {
                            exception = exception2;
                            IsError = true;
                            ErrorMsg = "Error while determining if the next character is <, location = 2, Error message = " + exception.Message;
                            return false;
                        }
                    }
                }
                else
                {
                    try
                    {
                        if (this.charIndex < (this.ruleSyntax.Length - 1))
                        {
                            switch (this.ruleSyntax[this.charIndex])
                            {
                                case '=':
                                    sOperator = sOperator + '=';
                                    this.charIndex++;
                                    goto Label_01D3;

                                case '>':
                                    sOperator = sOperator + '>';
                                    this.charIndex++;
                                    break;
                            }
                        }
                    }
                    catch (Exception exception1)
                    {
                        exception = exception1;
                        IsError = true;
                        ErrorMsg = "Error while determining if the next character is <, location = 1, Error message = " + exception.Message;
                        return false;
                    }
                }
            }
        Label_01D3:
            this.tokenItems.Add(new TokenItem(sOperator, CMM.Dynamic.Calculator.Parser.TokenType.Token_Operator, InOperandFunction));
            NextParseState = ParseState.Parse_State_Operand;
            goto Label_0611;
        Label_049B:
            this.tokenItems.Add(new TokenItem(sOperator, CMM.Dynamic.Calculator.Parser.TokenType.Token_Operator, InOperandFunction));
            NextParseState = ParseState.Parse_State_Operand;
        Label_0611:
            return true;
        }

        private bool FoundAssignment()
        {
            try
            {
                if (this.charIndex < (this.ruleSyntax.Length - 1))
                {
                    char ch = this.ruleSyntax[this.charIndex];
                    return (ch == '=');
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

        private void GetTokens()
        {
            ParseState state = ParseState.Parse_State_Operand;
            ParseState state2 = ParseState.Parse_State_Comment;
            ParseState state3 = ParseState.Parse_State_Operand;
            ParseState nextParseState = ParseState.Parse_State_Operand;
            string currentToken = "";
            int num = 0;
            int num2 = 0;
            int num3 = 0;
            int num4 = 0;
            int num5 = 0;
            int num6 = 0;
            bool isError = false;
            bool flag2 = false;
            int num7 = 0;
            if (string.IsNullOrEmpty(this.ruleSyntax))
            {
                return;
            }
            Stopwatch stopwatch = Stopwatch.StartNew();
            this.charIndex = 0;
        Label_0052:
            if (this.charIndex >= this.ruleSyntax.Length)
            {
                goto Label_0FD2;
            }
            char ch = this.ruleSyntax[this.charIndex];
            this.charIndex++;
            Debug.WriteLine("c = " + ch.ToString() + "\tcurrentToken = " + currentToken + "\tparse state = " + state.ToString() + "\tOp Parse State = " + state3.ToString());
            switch (ch)
            {
                case '\n':
                case '\t':
                    ch = ' ';
                    break;
            }
            if (state == ParseState.Parse_State_Comment)
            {
                if (ch == '~')
                {
                    state = state2;
                    num3--;
                }
            }
            else if (ch == '~')
            {
                num3++;
                state2 = state;
                state = ParseState.Parse_State_Comment;
            }
            else
            {
                switch (state)
                {
                    case ParseState.Parse_State_Operand:
                        Exception exception;
                        if (ch != '"')
                        {
                            switch (ch)
                            {
                                case ' ':
                                    try
                                    {
                                        flag2 = this.CreateTokenItem(currentToken, false, false, out nextParseState, out isError, out this.lastErrorMessage);
                                        if (isError)
                                        {
                                            return;
                                        }
                                        if (flag2)
                                        {
                                            state = nextParseState;
                                            currentToken = "";
                                        }
                                    }
                                    catch (Exception exception2)
                                    {
                                        exception = exception2;
                                        this.lastErrorMessage = "Error in GetTokens() in Operand Space Handling: " + exception.Message;
                                        return;
                                    }
                                    goto Label_0FB6;

                                case '(':
                                    num++;
                                    flag2 = this.CreateTokenItem(currentToken, false, false, out nextParseState, out isError, out this.lastErrorMessage);
                                    if (isError)
                                    {
                                        return;
                                    }
                                    this.tokenItems.Add(new TokenItem("(", CMM.Dynamic.Calculator.Parser.TokenType.Token_Open_Parenthesis, false));
                                    currentToken = "";
                                    state = ParseState.Parse_State_Operand;
                                    goto Label_0FB6;

                                case ')':
                                    num--;
                                    flag2 = this.CreateTokenItem(currentToken, false, false, out nextParseState, out isError, out this.lastErrorMessage);
                                    if (isError)
                                    {
                                        return;
                                    }
                                    this.tokenItems.Add(new TokenItem(")", CMM.Dynamic.Calculator.Parser.TokenType.Token_Close_Parenthesis, false));
                                    currentToken = "";
                                    state = ParseState.Parse_State_Operator;
                                    goto Label_0FB6;

                                case '[':
                                    num2++;
                                    currentToken = currentToken.Trim();
                                    if (DataTypeCheck.IsOperandFunction(currentToken))
                                    {
                                        num7++;
                                        currentToken = currentToken + ch;
                                        this.tokenItems.Add(new TokenItem(currentToken, CMM.Dynamic.Calculator.Parser.TokenType.Token_Operand_Function_Start, false));
                                        state = ParseState.Parse_State_OperandFunction;
                                        state3 = ParseState.Parse_State_Operand;
                                        currentToken = "";
                                        goto Label_0FB6;
                                    }
                                    this.lastErrorMessage = "Error in Rule Syntax: Found an open square parenthesis without an operand function";
                                    return;

                                case ']':
                                    num2--;
                                    this.lastErrorMessage = "Error in Rule Syntax: Found an ] while looking for an operand.";
                                    return;

                                case ',':
                                    this.lastErrorMessage = "Error in Rule Syntax: Found a , (comma) while looking for an operand.";
                                    return;

                                case '-':
                                    if (string.IsNullOrEmpty(currentToken.Trim()))
                                    {
                                        currentToken = currentToken + ch;
                                    }
                                    else
                                    {
                                        currentToken = currentToken + ch;
                                        flag2 = this.CreateTokenItem(currentToken, true, false, out nextParseState, out isError, out this.lastErrorMessage);
                                        if (isError)
                                        {
                                            return;
                                        }
                                        if (flag2)
                                        {
                                            currentToken = "";
                                            state = nextParseState;
                                        }
                                    }
                                    goto Label_0FB6;

                                case ':':
                                    if (!this.FoundAssignment())
                                    {
                                        this.lastErrorMessage = "Error in Rule Syntax: Found a : (colon) but did not find an assignment token.";
                                    }
                                    else
                                    {
                                        num5++;
                                        this.CreateTokenItem(currentToken, false, false, out nextParseState, out isError, out this.lastErrorMessage);
                                        if (!isError)
                                        {
                                            if (this.tokenItems[this.tokenItems.Count - 1].TokenType == CMM.Dynamic.Calculator.Parser.TokenType.Token_Operand)
                                            {
                                                if (this.tokenItems[this.tokenItems.Count - 1].TokenDataType != TokenDataType.Token_DataType_Variable)
                                                {
                                                    this.lastErrorMessage = "Error in Rule Syntax: An assignment can only be made to a variable.";
                                                    return;
                                                }
                                                this.tokenItems[this.tokenItems.Count - 1].WillBeAssigned = true;
                                                this.anyAssignments = true;
                                                this.tokenItems.Add(new TokenItem(":=", CMM.Dynamic.Calculator.Parser.TokenType.Token_Assignemt_Start, false));
                                                this.charIndex++;
                                                currentToken = "";
                                                state = ParseState.Parse_State_Operand;
                                                goto Label_0FB6;
                                            }
                                            this.lastErrorMessage = "Error in Rule Syntax: An assignment can only be made to a variable.";
                                        }
                                    }
                                    return;
                            }
                            if (ch == ';')
                            {
                                num6++;
                                flag2 = this.CreateTokenItem(currentToken, false, false, out nextParseState, out isError, out this.lastErrorMessage);
                                if (isError)
                                {
                                    return;
                                }
                                this.tokenItems.Add(new TokenItem(";", CMM.Dynamic.Calculator.Parser.TokenType.Token_Assignment_Stop, false));
                                currentToken = "";
                                state = ParseState.Parse_State_Operand;
                            }
                            else
                            {
                                currentToken = currentToken + ch;
                                flag2 = this.CreateTokenItem(currentToken, true, false, out nextParseState, out isError, out this.lastErrorMessage);
                                if (isError)
                                {
                                    return;
                                }
                                if (flag2)
                                {
                                    currentToken = "";
                                    state = nextParseState;
                                }
                            }
                        }
                        else
                        {
                            try
                            {
                                num4++;
                                currentToken = currentToken + ch;
                                state = ParseState.Parse_State_Quote;
                            }
                            catch (Exception exception1)
                            {
                                exception = exception1;
                                this.lastErrorMessage = "Error in GetTokens() in Operand Quote Handling: " + exception.Message;
                                return;
                            }
                        }
                        goto Label_0FB6;

                    case ParseState.Parse_State_Operator:
                        if (ch != '"')
                        {
                            switch (ch)
                            {
                                case ' ':
                                    if (string.IsNullOrEmpty(currentToken.Trim()))
                                    {
                                        currentToken = "";
                                        goto Label_0FB6;
                                    }
                                    this.lastErrorMessage = "Error with expression syntax: Found a space while looking for an operator";
                                    return;

                                case '(':
                                    this.lastErrorMessage = "Error in rule syntax: Found an open parenthesis while searching for an operator";
                                    return;

                                case ')':
                                    num--;
                                    this.tokenItems.Add(new TokenItem(")", CMM.Dynamic.Calculator.Parser.TokenType.Token_Close_Parenthesis, false));
                                    goto Label_0FB6;

                                case '[':
                                    this.lastErrorMessage = "Error in rule syntax: Found an open square parenthesis while searching for an operator";
                                    return;

                                case ']':
                                    this.lastErrorMessage = "Error in rule syntax: Found a closed square parenthesis while searching for an operator";
                                    return;

                                case ',':
                                    this.lastErrorMessage = "Error in rule syntax: Found a comma while searching for an operator";
                                    return;

                                case ':':
                                    if (!this.FoundAssignment())
                                    {
                                        this.lastErrorMessage = "Error in Rule Syntax: Found a : (colon) but did not find an assignment token.";
                                        return;
                                    }
                                    num5++;
                                    if (this.tokenItems[this.tokenItems.Count - 1].TokenType == CMM.Dynamic.Calculator.Parser.TokenType.Token_Operand)
                                    {
                                        if (this.tokenItems[this.tokenItems.Count - 1].TokenDataType != TokenDataType.Token_DataType_Variable)
                                        {
                                            this.lastErrorMessage = "Error in Rule Syntax: An assignment can only be made to a variable.";
                                            return;
                                        }
                                    }
                                    else
                                    {
                                        this.lastErrorMessage = "Error in Rule Syntax: An assignment can only be made to a variable.";
                                        return;
                                    }
                                    this.tokenItems[this.tokenItems.Count - 1].WillBeAssigned = true;
                                    this.anyAssignments = true;
                                    this.tokenItems.Add(new TokenItem(":=", CMM.Dynamic.Calculator.Parser.TokenType.Token_Assignemt_Start, false));
                                    this.charIndex++;
                                    currentToken = "";
                                    state = ParseState.Parse_State_Operand;
                                    goto Label_0FB6;
                            }
                            if (ch == ';')
                            {
                                this.tokenItems.Add(new TokenItem(";", CMM.Dynamic.Calculator.Parser.TokenType.Token_Assignment_Stop, false));
                                num6++;
                                currentToken = "";
                                state = ParseState.Parse_State_Operand;
                            }
                            else
                            {
                                currentToken = currentToken + ch;
                                flag2 = this.CreateTokenItem(currentToken, true, false, out nextParseState, out isError, out this.lastErrorMessage);
                                if (isError)
                                {
                                    return;
                                }
                                if (flag2)
                                {
                                    currentToken = "";
                                    state = nextParseState;
                                }
                            }
                            goto Label_0FB6;
                        }
                        this.lastErrorMessage = "Error in Rule Syntax: Found a double quote (\") while looking for an operator.";
                        return;

                    case ParseState.Parse_State_Quote:
                        if (ch != '"')
                        {
                            currentToken = currentToken + ch;
                            goto Label_0FB6;
                        }
                        num4--;
                        currentToken = currentToken + ch;
                        flag2 = this.CreateTokenItem(currentToken, false, false, out nextParseState, out isError, out this.lastErrorMessage);
                        if (!isError)
                        {
                            if (flag2)
                            {
                                state = nextParseState;
                                currentToken = "";
                            }
                            goto Label_0FB6;
                        }
                        return;

                    case ParseState.Parse_State_OperandFunction:
                        switch (state3)
                        {
                            case ParseState.Parse_State_Operand:
                                switch (ch)
                                {
                                    case '"':
                                        num4++;
                                        currentToken = currentToken + ch;
                                        state3 = ParseState.Parse_State_Quote;
                                        goto Label_0FB6;

                                    case ' ':
                                        if (this.CreateTokenItem(currentToken, false, true, out nextParseState, out isError, out this.lastErrorMessage))
                                        {
                                            state3 = nextParseState;
                                            currentToken = "";
                                        }
                                        goto Label_0FB6;

                                    case '(':
                                        num++;
                                        this.CreateTokenItem(currentToken, false, true, out nextParseState, out isError, out this.lastErrorMessage);
                                        if (isError)
                                        {
                                            return;
                                        }
                                        this.tokenItems.Add(new TokenItem("(", CMM.Dynamic.Calculator.Parser.TokenType.Token_Open_Parenthesis, true));
                                        currentToken = "";
                                        state3 = ParseState.Parse_State_Operand;
                                        goto Label_0FB6;

                                    case ')':
                                        num--;
                                        this.CreateTokenItem(currentToken, false, true, out nextParseState, out isError, out this.lastErrorMessage);
                                        if (isError)
                                        {
                                            return;
                                        }
                                        this.tokenItems.Add(new TokenItem(")", CMM.Dynamic.Calculator.Parser.TokenType.Token_Close_Parenthesis, true));
                                        currentToken = "";
                                        state3 = ParseState.Parse_State_Operator;
                                        goto Label_0FB6;

                                    case '[':
                                        num2++;
                                        currentToken = currentToken.Trim();
                                        if (DataTypeCheck.IsOperandFunction(currentToken))
                                        {
                                            num7++;
                                            currentToken = currentToken + ch;
                                            this.tokenItems.Add(new TokenItem(currentToken, CMM.Dynamic.Calculator.Parser.TokenType.Token_Operand_Function_Start, true));
                                            state3 = ParseState.Parse_State_Operand;
                                            currentToken = "";
                                            goto Label_0FB6;
                                        }
                                        this.lastErrorMessage = "Error in Rule Syntax: Found an open square parenthesis without an operand function";
                                        return;

                                    case ']':
                                        num2--;
                                        this.CreateTokenItem(currentToken, false, true, out nextParseState, out isError, out this.lastErrorMessage);
                                        if (isError)
                                        {
                                            return;
                                        }
                                        this.tokenItems.Add(new TokenItem("]", CMM.Dynamic.Calculator.Parser.TokenType.Token_Operand_Function_Stop, true));
                                        num7--;
                                        state3 = ParseState.Parse_State_Operator;
                                        if (num7 <= 0)
                                        {
                                            num7 = 0;
                                            state = ParseState.Parse_State_Operator;
                                        }
                                        currentToken = "";
                                        goto Label_0FB6;

                                    case ',':
                                        this.CreateTokenItem(currentToken, false, true, out nextParseState, out isError, out this.lastErrorMessage);
                                        if (isError)
                                        {
                                            return;
                                        }
                                        this.tokenItems.Add(new TokenItem(",", CMM.Dynamic.Calculator.Parser.TokenType.Token_Operand_Function_Delimiter, true));
                                        currentToken = "";
                                        state3 = ParseState.Parse_State_Operand;
                                        goto Label_0FB6;

                                    case '-':
                                        if (string.IsNullOrEmpty(currentToken.Trim()))
                                        {
                                            currentToken = currentToken + ch;
                                        }
                                        else
                                        {
                                            currentToken = currentToken + ch;
                                            flag2 = this.CreateTokenItem(currentToken, true, true, out nextParseState, out isError, out this.lastErrorMessage);
                                            if (isError)
                                            {
                                                return;
                                            }
                                            if (flag2)
                                            {
                                                currentToken = "";
                                                state3 = nextParseState;
                                            }
                                        }
                                        goto Label_0FB6;
                                }
                                if ((ch == ':') || (ch == ';'))
                                {
                                    this.lastErrorMessage = "Error in Rule Syntax: Assignments are not allowed within Operand Functions";
                                    return;
                                }
                                currentToken = currentToken + ch;
                                flag2 = this.CreateTokenItem(currentToken, true, true, out nextParseState, out isError, out this.lastErrorMessage);
                                if (isError)
                                {
                                    return;
                                }
                                if (flag2)
                                {
                                    currentToken = "";
                                    state3 = nextParseState;
                                }
                                goto Label_0FB6;

                            case ParseState.Parse_State_Operator:
                                switch (ch)
                                {
                                    case '"':
                                        this.lastErrorMessage = "Error in Rule Syntax: Found a double quote (\") while looking for an operator.";
                                        return;

                                    case ' ':
                                        if (string.IsNullOrEmpty(currentToken.Trim()))
                                        {
                                            currentToken = "";
                                            goto Label_0FB6;
                                        }
                                        this.lastErrorMessage = "Error with expression syntax: Found a space while looking for an operator";
                                        return;

                                    case '(':
                                        this.lastErrorMessage = "Error in rule syntax: Found an open parenthesis while searching for an operator";
                                        return;

                                    case ')':
                                        num--;
                                        this.tokenItems.Add(new TokenItem(")", CMM.Dynamic.Calculator.Parser.TokenType.Token_Close_Parenthesis, true));
                                        goto Label_0FB6;

                                    case '[':
                                        this.lastErrorMessage = "Error in rule syntax: Found an open square parenthesis while searching for an operator";
                                        return;

                                    case ']':
                                        num2--;
                                        this.CreateTokenItem(currentToken, false, true, out nextParseState, out isError, out this.lastErrorMessage);
                                        if (isError)
                                        {
                                            return;
                                        }
                                        this.tokenItems.Add(new TokenItem("]", CMM.Dynamic.Calculator.Parser.TokenType.Token_Operand_Function_Stop, true));
                                        num7--;
                                        state3 = ParseState.Parse_State_Operand;
                                        if (num7 <= 0)
                                        {
                                            num7 = 0;
                                            state = ParseState.Parse_State_Operator;
                                        }
                                        currentToken = "";
                                        goto Label_0FB6;

                                    case ',':
                                        this.CreateTokenItem(currentToken, false, true, out nextParseState, out isError, out this.lastErrorMessage);
                                        if (isError)
                                        {
                                            return;
                                        }
                                        this.tokenItems.Add(new TokenItem(",", CMM.Dynamic.Calculator.Parser.TokenType.Token_Operand_Function_Delimiter, true));
                                        currentToken = "";
                                        state3 = ParseState.Parse_State_Operand;
                                        goto Label_0FB6;
                                }
                                if ((ch == ':') || (ch == ';'))
                                {
                                    this.lastErrorMessage = "Error in Rule Syntax: Assignments are not allowed within Operand Functions";
                                    return;
                                }
                                currentToken = currentToken + ch;
                                flag2 = this.CreateTokenItem(currentToken, true, true, out nextParseState, out isError, out this.lastErrorMessage);
                                if (isError)
                                {
                                    return;
                                }
                                if (flag2)
                                {
                                    currentToken = "";
                                    state3 = ParseState.Parse_State_Operand;
                                }
                                goto Label_0FB6;

                            case ParseState.Parse_State_Quote:
                                if (ch == '"')
                                {
                                    num4--;
                                    currentToken = currentToken + ch;
                                    flag2 = this.CreateTokenItem(currentToken, false, true, out nextParseState, out isError, out this.lastErrorMessage);
                                    if (isError)
                                    {
                                        return;
                                    }
                                    if (flag2)
                                    {
                                        state3 = nextParseState;
                                        currentToken = "";
                                    }
                                }
                                else
                                {
                                    currentToken = currentToken + ch;
                                }
                                goto Label_0FB6;
                        }
                        goto Label_0FB6;
                }
            }
        Label_0FB6:
            if (this.charIndex < this.ruleSyntax.Length)
            {
                goto Label_0052;
            }
        Label_0FD2:
            currentToken = currentToken.Trim();
            if (!string.IsNullOrEmpty(currentToken))
            {
                if (currentToken == "(")
                {
                    num++;
                }
                else if (currentToken == ")")
                {
                    num--;
                }
                else if (currentToken == "[")
                {
                    num2++;
                }
                else if (currentToken == "]")
                {
                    num2--;
                }
                else if (currentToken == "~")
                {
                    num3--;
                }
                else if (currentToken == "\"")
                {
                    num4--;
                }
                switch (state)
                {
                    case ParseState.Parse_State_Operand:
                        this.CreateTokenItem(currentToken, false, false, out nextParseState, out isError, out this.lastErrorMessage);
                        if (!isError)
                        {
                            break;
                        }
                        return;

                    case ParseState.Parse_State_Operator:
                        this.lastErrorMessage = "Error in Rule Syntax: A rule cannot end with an operator.";
                        break;

                    case ParseState.Parse_State_Quote:
                        if (!(currentToken != "\""))
                        {
                            this.tokenItems.Add(new TokenItem(currentToken, CMM.Dynamic.Calculator.Parser.TokenType.Token_Operand, TokenDataType.Token_DataType_String, false));
                            break;
                        }
                        this.lastErrorMessage = "Error in RuleSyntax: Double quote mismatch.";
                        return;
                }
            }
            if (num != 0)
            {
                this.lastErrorMessage = "Error in RuleSyntax: There is a parenthesis mismatch.";
            }
            else if (num2 != 0)
            {
                this.lastErrorMessage = "Error in RuleSyntax: There is an operand function mismatch.";
            }
            else if (num7 > 0)
            {
                this.lastErrorMessage = "Error in RuleSyntax: There is an operand function mismatch error...Operand function depth is not zero.";
            }
            else if (num3 != 0)
            {
                this.lastErrorMessage = "Error in RuleSyntax: There is a comment mismatch.";
            }
            else if (num4 != 0)
            {
                this.lastErrorMessage = "Error in RuleSyntax: There is a quote mismatch.";
            }
            else if (this.charIndex < this.ruleSyntax.Length)
            {
                this.lastErrorMessage = "Error in RuleSyntax: There was a problem parsing the rule...some of the tokens were not found.";
            }
            else if (num5 != num6)
            {
                this.lastErrorMessage = "Error in RuleSyntax: There was a problem parsing the rule...there was an assignment mismatch.";
            }
            else
            {
                bool flag3 = false;
                ExStack<TokenItem> stack = new ExStack<TokenItem>();
                foreach (TokenItem item in this.tokenItems)
                {
                    Debug.WriteLine(item.TokenName.Trim().ToLower());
                    if (item.TokenType == CMM.Dynamic.Calculator.Parser.TokenType.Token_Operand_Function_Start)
                    {
                        flag3 = flag3 || (item.TokenName.Trim().ToLower() == "iif[");
                        stack.Push(item);
                    }
                    else if (item.TokenType == CMM.Dynamic.Calculator.Parser.TokenType.Token_Operand_Function_Stop)
                    {
                        string str2 = stack.Peek().TokenName.Trim().ToLower();
                        if ((str2 == "iif[") || (str2 == "]"))
                        {
                            stack.Push(item);
                        }
                        else
                        {
                            stack.Pop();
                        }
                    }
                }
                if (flag3)
                {
                    do
                    {
                        TokenItem item2 = null;
                        TokenItem item3 = null;
                        if (stack.Count > 0)
                        {
                            item2 = stack.Pop();
                        }
                        if (stack.Count > 0)
                        {
                            item3 = stack.Peek();
                        }
                        if (((item2 != null) && (item2.TokenType == CMM.Dynamic.Calculator.Parser.TokenType.Token_Operand_Function_Stop)) && ((item3 != null) && (item3.TokenType == CMM.Dynamic.Calculator.Parser.TokenType.Token_Operand_Function_Start)))
                        {
                            item3.CanShortCircuit = true;
                        }
                    }
                    while (stack.Count > 0);
                }
                if (!this.AnyErrors)
                {
                    this.MakeRPNQueue();
                }
                if (this.rpn_queue == null)
                {
                    this.lastErrorMessage = "Error in RuleSyntax: There was a problem creating the RPN queue.";
                }
                else if (this.rpn_queue.Count == 0)
                {
                    this.lastErrorMessage = "Error in RuleSyntax: There was a problem creating the RPN queue.";
                }
                else
                {
                    stopwatch.Stop();
                    this.tokenParseTime = stopwatch.Elapsed.TotalMilliseconds;
                }
            }
        }

        private void GetTokens_Old_1()
        {
            ParseState state = ParseState.Parse_State_Operand;
            ParseState state2 = ParseState.Parse_State_Comment;
            ParseState state3 = ParseState.Parse_State_Operand;
            ParseState nextParseState = ParseState.Parse_State_Operand;
            string currentToken = "";
            int num = 0;
            int num2 = 0;
            int num3 = 0;
            int num4 = 0;
            bool isError = false;
            bool flag2 = false;
            int num5 = 0;
            if (string.IsNullOrEmpty(this.ruleSyntax))
            {
                return;
            }
            Stopwatch stopwatch = Stopwatch.StartNew();
            this.charIndex = 0;
        Label_004C:
            if (this.charIndex >= this.ruleSyntax.Length)
            {
                goto Label_0CAC;
            }
            char ch = this.ruleSyntax[this.charIndex];
            this.charIndex++;
            Debug.WriteLine("c = " + ch.ToString() + "\tcurrentToken = " + currentToken + "\tparse state = " + state.ToString() + "\tOp Parse State = " + state3.ToString());
            switch (ch)
            {
                case '\n':
                case '\t':
                    ch = ' ';
                    break;
            }
            if (state == ParseState.Parse_State_Comment)
            {
                if (ch == '~')
                {
                    state = state2;
                    num3--;
                }
            }
            else if (ch == '~')
            {
                num3++;
                state2 = state;
                state = ParseState.Parse_State_Comment;
            }
            else
            {
                switch (state)
                {
                    case ParseState.Parse_State_Operand:
                        Exception exception;
                        if (ch != '"')
                        {
                            switch (ch)
                            {
                                case ' ':
                                    try
                                    {
                                        flag2 = this.CreateTokenItem(currentToken, false, false, out nextParseState, out isError, out this.lastErrorMessage);
                                        if (isError)
                                        {
                                            return;
                                        }
                                        if (flag2)
                                        {
                                            state = nextParseState;
                                            currentToken = "";
                                        }
                                    }
                                    catch (Exception exception2)
                                    {
                                        exception = exception2;
                                        this.lastErrorMessage = "Error in GetTokens() in Operand Space Handling: " + exception.Message;
                                        return;
                                    }
                                    goto Label_0C90;

                                case '(':
                                    num++;
                                    flag2 = this.CreateTokenItem(currentToken, false, false, out nextParseState, out isError, out this.lastErrorMessage);
                                    if (isError)
                                    {
                                        return;
                                    }
                                    this.tokenItems.Add(new TokenItem("(", CMM.Dynamic.Calculator.Parser.TokenType.Token_Open_Parenthesis, false));
                                    currentToken = "";
                                    state = ParseState.Parse_State_Operand;
                                    goto Label_0C90;

                                case ')':
                                    num--;
                                    flag2 = this.CreateTokenItem(currentToken, false, false, out nextParseState, out isError, out this.lastErrorMessage);
                                    if (isError)
                                    {
                                        return;
                                    }
                                    this.tokenItems.Add(new TokenItem(")", CMM.Dynamic.Calculator.Parser.TokenType.Token_Close_Parenthesis, false));
                                    currentToken = "";
                                    state = ParseState.Parse_State_Operator;
                                    goto Label_0C90;

                                case '[':
                                    num2++;
                                    currentToken = currentToken.Trim();
                                    if (DataTypeCheck.IsOperandFunction(currentToken))
                                    {
                                        num5++;
                                        currentToken = currentToken + ch;
                                        this.tokenItems.Add(new TokenItem(currentToken, CMM.Dynamic.Calculator.Parser.TokenType.Token_Operand_Function_Start, false));
                                        state = ParseState.Parse_State_OperandFunction;
                                        state3 = ParseState.Parse_State_Operand;
                                        currentToken = "";
                                        goto Label_0C90;
                                    }
                                    this.lastErrorMessage = "Error in Rule Syntax: Found an open square parenthesis without an operand function";
                                    return;

                                case ']':
                                    num2--;
                                    this.lastErrorMessage = "Error in Rule Syntax: Found an ] while looking for an operand.";
                                    return;

                                case ',':
                                    this.lastErrorMessage = "Error in Rule Syntax: Found a , (comma) while looking for an operand.";
                                    return;

                                case '-':
                                    if (string.IsNullOrEmpty(currentToken.Trim()))
                                    {
                                        currentToken = currentToken + ch;
                                    }
                                    else
                                    {
                                        currentToken = currentToken + ch;
                                        flag2 = this.CreateTokenItem(currentToken, true, false, out nextParseState, out isError, out this.lastErrorMessage);
                                        if (isError)
                                        {
                                            return;
                                        }
                                        if (flag2)
                                        {
                                            currentToken = "";
                                            state = nextParseState;
                                        }
                                    }
                                    goto Label_0C90;
                            }
                            currentToken = currentToken + ch;
                            flag2 = this.CreateTokenItem(currentToken, true, false, out nextParseState, out isError, out this.lastErrorMessage);
                            if (isError)
                            {
                                return;
                            }
                            if (flag2)
                            {
                                currentToken = "";
                                state = nextParseState;
                            }
                        }
                        else
                        {
                            try
                            {
                                num4++;
                                currentToken = currentToken + ch;
                                state = ParseState.Parse_State_Quote;
                            }
                            catch (Exception exception1)
                            {
                                exception = exception1;
                                this.lastErrorMessage = "Error in GetTokens() in Operand Quote Handling: " + exception.Message;
                                return;
                            }
                        }
                        goto Label_0C90;

                    case ParseState.Parse_State_Operator:
                        if (ch != '"')
                        {
                            switch (ch)
                            {
                                case ' ':
                                    if (string.IsNullOrEmpty(currentToken.Trim()))
                                    {
                                        currentToken = "";
                                        goto Label_0C90;
                                    }
                                    this.lastErrorMessage = "Error with expression syntax: Found a space while looking for an operator";
                                    return;

                                case '(':
                                    this.lastErrorMessage = "Error in rule syntax: Found an open parenthesis while searching for an operator";
                                    return;
                            }
                            if (ch == ')')
                            {
                                num--;
                                this.tokenItems.Add(new TokenItem(")", CMM.Dynamic.Calculator.Parser.TokenType.Token_Close_Parenthesis, false));
                            }
                            else
                            {
                                if (ch == '[')
                                {
                                    this.lastErrorMessage = "Error in rule syntax: Found an open square parenthesis while searching for an operator";
                                    return;
                                }
                                if (ch == ']')
                                {
                                    this.lastErrorMessage = "Error in rule syntax: Found a closed square parenthesis while searching for an operator";
                                    return;
                                }
                                if (ch == ',')
                                {
                                    this.lastErrorMessage = "Error in rule syntax: Found a comma while searching for an operator";
                                    return;
                                }
                                currentToken = currentToken + ch;
                                flag2 = this.CreateTokenItem(currentToken, true, false, out nextParseState, out isError, out this.lastErrorMessage);
                                if (isError)
                                {
                                    return;
                                }
                                if (flag2)
                                {
                                    currentToken = "";
                                    state = nextParseState;
                                }
                            }
                            goto Label_0C90;
                        }
                        this.lastErrorMessage = "Error in Rule Syntax: Found a double quote (\") while looking for an operator.";
                        return;

                    case ParseState.Parse_State_Quote:
                        if (ch != '"')
                        {
                            currentToken = currentToken + ch;
                            goto Label_0C90;
                        }
                        num4--;
                        currentToken = currentToken + ch;
                        flag2 = this.CreateTokenItem(currentToken, false, false, out nextParseState, out isError, out this.lastErrorMessage);
                        if (!isError)
                        {
                            if (flag2)
                            {
                                state = nextParseState;
                                currentToken = "";
                            }
                            goto Label_0C90;
                        }
                        return;

                    case ParseState.Parse_State_OperandFunction:
                        switch (state3)
                        {
                            case ParseState.Parse_State_Operand:
                                switch (ch)
                                {
                                    case '"':
                                        num4++;
                                        currentToken = currentToken + ch;
                                        state3 = ParseState.Parse_State_Quote;
                                        goto Label_0C90;

                                    case ' ':
                                        if (this.CreateTokenItem(currentToken, false, true, out nextParseState, out isError, out this.lastErrorMessage))
                                        {
                                            state3 = nextParseState;
                                            currentToken = "";
                                        }
                                        goto Label_0C90;

                                    case '(':
                                        num++;
                                        this.CreateTokenItem(currentToken, false, true, out nextParseState, out isError, out this.lastErrorMessage);
                                        if (isError)
                                        {
                                            return;
                                        }
                                        this.tokenItems.Add(new TokenItem("(", CMM.Dynamic.Calculator.Parser.TokenType.Token_Open_Parenthesis, true));
                                        currentToken = "";
                                        state3 = ParseState.Parse_State_Operand;
                                        goto Label_0C90;

                                    case ')':
                                        num--;
                                        this.CreateTokenItem(currentToken, false, true, out nextParseState, out isError, out this.lastErrorMessage);
                                        if (isError)
                                        {
                                            return;
                                        }
                                        this.tokenItems.Add(new TokenItem(")", CMM.Dynamic.Calculator.Parser.TokenType.Token_Close_Parenthesis, true));
                                        currentToken = "";
                                        state3 = ParseState.Parse_State_Operator;
                                        goto Label_0C90;

                                    case '[':
                                        num2++;
                                        currentToken = currentToken.Trim();
                                        if (DataTypeCheck.IsOperandFunction(currentToken))
                                        {
                                            num5++;
                                            currentToken = currentToken + ch;
                                            this.tokenItems.Add(new TokenItem(currentToken, CMM.Dynamic.Calculator.Parser.TokenType.Token_Operand_Function_Start, true));
                                            state3 = ParseState.Parse_State_Operand;
                                            currentToken = "";
                                            goto Label_0C90;
                                        }
                                        this.lastErrorMessage = "Error in Rule Syntax: Found an open square parenthesis without an operand function";
                                        return;

                                    case ']':
                                        num2--;
                                        this.CreateTokenItem(currentToken, false, true, out nextParseState, out isError, out this.lastErrorMessage);
                                        if (isError)
                                        {
                                            return;
                                        }
                                        this.tokenItems.Add(new TokenItem("]", CMM.Dynamic.Calculator.Parser.TokenType.Token_Operand_Function_Stop, true));
                                        num5--;
                                        state3 = ParseState.Parse_State_Operator;
                                        if (num5 <= 0)
                                        {
                                            num5 = 0;
                                            state = ParseState.Parse_State_Operator;
                                        }
                                        currentToken = "";
                                        goto Label_0C90;

                                    case ',':
                                        this.CreateTokenItem(currentToken, false, true, out nextParseState, out isError, out this.lastErrorMessage);
                                        if (isError)
                                        {
                                            return;
                                        }
                                        this.tokenItems.Add(new TokenItem(",", CMM.Dynamic.Calculator.Parser.TokenType.Token_Operand_Function_Delimiter, true));
                                        currentToken = "";
                                        state3 = ParseState.Parse_State_Operand;
                                        goto Label_0C90;

                                    case '-':
                                        if (string.IsNullOrEmpty(currentToken.Trim()))
                                        {
                                            currentToken = currentToken + ch;
                                        }
                                        else
                                        {
                                            currentToken = currentToken + ch;
                                            flag2 = this.CreateTokenItem(currentToken, true, true, out nextParseState, out isError, out this.lastErrorMessage);
                                            if (isError)
                                            {
                                                return;
                                            }
                                            if (flag2)
                                            {
                                                currentToken = "";
                                                state3 = nextParseState;
                                            }
                                        }
                                        goto Label_0C90;
                                }
                                currentToken = currentToken + ch;
                                flag2 = this.CreateTokenItem(currentToken, true, true, out nextParseState, out isError, out this.lastErrorMessage);
                                if (isError)
                                {
                                    return;
                                }
                                if (flag2)
                                {
                                    currentToken = "";
                                    state3 = nextParseState;
                                }
                                goto Label_0C90;

                            case ParseState.Parse_State_Operator:
                                switch (ch)
                                {
                                    case '"':
                                        this.lastErrorMessage = "Error in Rule Syntax: Found a double quote (\") while looking for an operator.";
                                        return;

                                    case ' ':
                                        if (string.IsNullOrEmpty(currentToken.Trim()))
                                        {
                                            currentToken = "";
                                            goto Label_0C90;
                                        }
                                        this.lastErrorMessage = "Error with expression syntax: Found a space while looking for an operator";
                                        return;

                                    case '(':
                                        this.lastErrorMessage = "Error in rule syntax: Found an open parenthesis while searching for an operator";
                                        return;

                                    case ')':
                                        num--;
                                        this.tokenItems.Add(new TokenItem(")", CMM.Dynamic.Calculator.Parser.TokenType.Token_Close_Parenthesis, true));
                                        goto Label_0C90;

                                    case '[':
                                        this.lastErrorMessage = "Error in rule syntax: Found an open square parenthesis while searching for an operator";
                                        return;

                                    case ']':
                                        num2--;
                                        this.CreateTokenItem(currentToken, false, true, out nextParseState, out isError, out this.lastErrorMessage);
                                        if (isError)
                                        {
                                            return;
                                        }
                                        this.tokenItems.Add(new TokenItem("]", CMM.Dynamic.Calculator.Parser.TokenType.Token_Operand_Function_Stop, true));
                                        num5--;
                                        state3 = ParseState.Parse_State_Operand;
                                        if (num5 <= 0)
                                        {
                                            num5 = 0;
                                            state = ParseState.Parse_State_Operator;
                                        }
                                        currentToken = "";
                                        goto Label_0C90;

                                    case ',':
                                        this.CreateTokenItem(currentToken, false, true, out nextParseState, out isError, out this.lastErrorMessage);
                                        if (isError)
                                        {
                                            return;
                                        }
                                        this.tokenItems.Add(new TokenItem(",", CMM.Dynamic.Calculator.Parser.TokenType.Token_Operand_Function_Delimiter, true));
                                        currentToken = "";
                                        state3 = ParseState.Parse_State_Operand;
                                        goto Label_0C90;
                                }
                                currentToken = currentToken + ch;
                                flag2 = this.CreateTokenItem(currentToken, true, true, out nextParseState, out isError, out this.lastErrorMessage);
                                if (isError)
                                {
                                    return;
                                }
                                if (flag2)
                                {
                                    currentToken = "";
                                    state3 = ParseState.Parse_State_Operand;
                                }
                                goto Label_0C90;

                            case ParseState.Parse_State_Quote:
                                if (ch == '"')
                                {
                                    num4--;
                                    currentToken = currentToken + ch;
                                    flag2 = this.CreateTokenItem(currentToken, false, true, out nextParseState, out isError, out this.lastErrorMessage);
                                    if (isError)
                                    {
                                        return;
                                    }
                                    if (flag2)
                                    {
                                        state3 = nextParseState;
                                        currentToken = "";
                                    }
                                }
                                else
                                {
                                    currentToken = currentToken + ch;
                                }
                                goto Label_0C90;
                        }
                        goto Label_0C90;
                }
            }
        Label_0C90:
            if (this.charIndex < this.ruleSyntax.Length)
            {
                goto Label_004C;
            }
        Label_0CAC:
            currentToken = currentToken.Trim();
            if (!string.IsNullOrEmpty(currentToken))
            {
                if (currentToken == "(")
                {
                    num++;
                }
                else if (currentToken == ")")
                {
                    num--;
                }
                else if (currentToken == "[")
                {
                    num2++;
                }
                else if (currentToken == "]")
                {
                    num2--;
                }
                else if (currentToken == "~")
                {
                    num3--;
                }
                else if (currentToken == "\"")
                {
                    num4--;
                }
                switch (state)
                {
                    case ParseState.Parse_State_Operand:
                        this.CreateTokenItem(currentToken, false, false, out nextParseState, out isError, out this.lastErrorMessage);
                        if (!isError)
                        {
                            break;
                        }
                        return;

                    case ParseState.Parse_State_Operator:
                        this.lastErrorMessage = "Error in Rule Syntax: A rule cannot end with an operator.";
                        break;

                    case ParseState.Parse_State_Quote:
                        if (!(currentToken != "\""))
                        {
                            this.tokenItems.Add(new TokenItem(currentToken, CMM.Dynamic.Calculator.Parser.TokenType.Token_Operand, TokenDataType.Token_DataType_String, false));
                            break;
                        }
                        this.lastErrorMessage = "Error in RuleSyntax: Double quote mismatch.";
                        return;
                }
            }
            if (num != 0)
            {
                this.lastErrorMessage = "Error in RuleSyntax: There is a parenthesis mismatch.";
            }
            else if (num2 != 0)
            {
                this.lastErrorMessage = "Error in RuleSyntax: There is an operand function mismatch.";
            }
            else if (num5 > 0)
            {
                this.lastErrorMessage = "Error in RuleSyntax: There is an operand function mismatch error...Operand function depth is not zero.";
            }
            else if (num3 != 0)
            {
                this.lastErrorMessage = "Error in RuleSyntax: There is a comment mismatch.";
            }
            else if (num4 != 0)
            {
                this.lastErrorMessage = "Error in RuleSyntax: There is a quote mismatch.";
            }
            else if (this.charIndex < this.ruleSyntax.Length)
            {
                this.lastErrorMessage = "Error in RuleSyntax: There was a problem parsing the rule...some of the tokens were not found.";
            }
            else
            {
                if (!this.AnyErrors)
                {
                    this.MakeRPNQueue();
                }
                if (this.rpn_queue == null)
                {
                    this.lastErrorMessage = "Error in RuleSyntax: There was a problem creating the RPN queue.";
                }
                else if (this.rpn_queue.Count == 0)
                {
                    this.lastErrorMessage = "Error in RuleSyntax: There was a problem creating the RPN queue.";
                }
                else
                {
                    stopwatch.Stop();
                    this.tokenParseTime = stopwatch.Elapsed.TotalMilliseconds;
                }
            }
        }

        private void GetTokens_old_2()
        {
            ParseState state = ParseState.Parse_State_Operand;
            ParseState state2 = ParseState.Parse_State_Comment;
            ParseState state3 = ParseState.Parse_State_Operand;
            ParseState nextParseState = ParseState.Parse_State_Operand;
            string currentToken = "";
            int num = 0;
            int num2 = 0;
            int num3 = 0;
            int num4 = 0;
            int num5 = 0;
            int num6 = 0;
            bool isError = false;
            bool flag2 = false;
            int num7 = 0;
            if (string.IsNullOrEmpty(this.ruleSyntax))
            {
                return;
            }
            Stopwatch stopwatch = Stopwatch.StartNew();
            this.charIndex = 0;
        Label_0052:
            if (this.charIndex >= this.ruleSyntax.Length)
            {
                goto Label_0FD2;
            }
            char ch = this.ruleSyntax[this.charIndex];
            this.charIndex++;
            Debug.WriteLine("c = " + ch.ToString() + "\tcurrentToken = " + currentToken + "\tparse state = " + state.ToString() + "\tOp Parse State = " + state3.ToString());
            switch (ch)
            {
                case '\n':
                case '\t':
                    ch = ' ';
                    break;
            }
            if (state == ParseState.Parse_State_Comment)
            {
                if (ch == '~')
                {
                    state = state2;
                    num3--;
                }
            }
            else if (ch == '~')
            {
                num3++;
                state2 = state;
                state = ParseState.Parse_State_Comment;
            }
            else
            {
                switch (state)
                {
                    case ParseState.Parse_State_Operand:
                        Exception exception;
                        if (ch != '"')
                        {
                            switch (ch)
                            {
                                case ' ':
                                    try
                                    {
                                        flag2 = this.CreateTokenItem(currentToken, false, false, out nextParseState, out isError, out this.lastErrorMessage);
                                        if (isError)
                                        {
                                            return;
                                        }
                                        if (flag2)
                                        {
                                            state = nextParseState;
                                            currentToken = "";
                                        }
                                    }
                                    catch (Exception exception2)
                                    {
                                        exception = exception2;
                                        this.lastErrorMessage = "Error in GetTokens() in Operand Space Handling: " + exception.Message;
                                        return;
                                    }
                                    goto Label_0FB6;

                                case '(':
                                    num++;
                                    flag2 = this.CreateTokenItem(currentToken, false, false, out nextParseState, out isError, out this.lastErrorMessage);
                                    if (isError)
                                    {
                                        return;
                                    }
                                    this.tokenItems.Add(new TokenItem("(", CMM.Dynamic.Calculator.Parser.TokenType.Token_Open_Parenthesis, false));
                                    currentToken = "";
                                    state = ParseState.Parse_State_Operand;
                                    goto Label_0FB6;

                                case ')':
                                    num--;
                                    flag2 = this.CreateTokenItem(currentToken, false, false, out nextParseState, out isError, out this.lastErrorMessage);
                                    if (isError)
                                    {
                                        return;
                                    }
                                    this.tokenItems.Add(new TokenItem(")", CMM.Dynamic.Calculator.Parser.TokenType.Token_Close_Parenthesis, false));
                                    currentToken = "";
                                    state = ParseState.Parse_State_Operator;
                                    goto Label_0FB6;

                                case '[':
                                    num2++;
                                    currentToken = currentToken.Trim();
                                    if (DataTypeCheck.IsOperandFunction(currentToken))
                                    {
                                        num7++;
                                        currentToken = currentToken + ch;
                                        this.tokenItems.Add(new TokenItem(currentToken, CMM.Dynamic.Calculator.Parser.TokenType.Token_Operand_Function_Start, false));
                                        state = ParseState.Parse_State_OperandFunction;
                                        state3 = ParseState.Parse_State_Operand;
                                        currentToken = "";
                                        goto Label_0FB6;
                                    }
                                    this.lastErrorMessage = "Error in Rule Syntax: Found an open square parenthesis without an operand function";
                                    return;

                                case ']':
                                    num2--;
                                    this.lastErrorMessage = "Error in Rule Syntax: Found an ] while looking for an operand.";
                                    return;

                                case ',':
                                    this.lastErrorMessage = "Error in Rule Syntax: Found a , (comma) while looking for an operand.";
                                    return;

                                case '-':
                                    if (string.IsNullOrEmpty(currentToken.Trim()))
                                    {
                                        currentToken = currentToken + ch;
                                    }
                                    else
                                    {
                                        currentToken = currentToken + ch;
                                        flag2 = this.CreateTokenItem(currentToken, true, false, out nextParseState, out isError, out this.lastErrorMessage);
                                        if (isError)
                                        {
                                            return;
                                        }
                                        if (flag2)
                                        {
                                            currentToken = "";
                                            state = nextParseState;
                                        }
                                    }
                                    goto Label_0FB6;

                                case ':':
                                    if (!this.FoundAssignment())
                                    {
                                        this.lastErrorMessage = "Error in Rule Syntax: Found a : (colon) but did not find an assignment token.";
                                    }
                                    else
                                    {
                                        num5++;
                                        this.CreateTokenItem(currentToken, false, false, out nextParseState, out isError, out this.lastErrorMessage);
                                        if (!isError)
                                        {
                                            if (this.tokenItems[this.tokenItems.Count - 1].TokenType == CMM.Dynamic.Calculator.Parser.TokenType.Token_Operand)
                                            {
                                                if (this.tokenItems[this.tokenItems.Count - 1].TokenDataType != TokenDataType.Token_DataType_Variable)
                                                {
                                                    this.lastErrorMessage = "Error in Rule Syntax: An assignment can only be made to a variable.";
                                                    return;
                                                }
                                                this.tokenItems[this.tokenItems.Count - 1].WillBeAssigned = true;
                                                this.anyAssignments = true;
                                                this.tokenItems.Add(new TokenItem(":=", CMM.Dynamic.Calculator.Parser.TokenType.Token_Assignemt_Start, false));
                                                this.charIndex++;
                                                currentToken = "";
                                                state = ParseState.Parse_State_Operand;
                                                goto Label_0FB6;
                                            }
                                            this.lastErrorMessage = "Error in Rule Syntax: An assignment can only be made to a variable.";
                                        }
                                    }
                                    return;
                            }
                            if (ch == ';')
                            {
                                num6++;
                                flag2 = this.CreateTokenItem(currentToken, false, false, out nextParseState, out isError, out this.lastErrorMessage);
                                if (isError)
                                {
                                    return;
                                }
                                this.tokenItems.Add(new TokenItem(";", CMM.Dynamic.Calculator.Parser.TokenType.Token_Assignment_Stop, false));
                                currentToken = "";
                                state = ParseState.Parse_State_Operand;
                            }
                            else
                            {
                                currentToken = currentToken + ch;
                                flag2 = this.CreateTokenItem(currentToken, true, false, out nextParseState, out isError, out this.lastErrorMessage);
                                if (isError)
                                {
                                    return;
                                }
                                if (flag2)
                                {
                                    currentToken = "";
                                    state = nextParseState;
                                }
                            }
                        }
                        else
                        {
                            try
                            {
                                num4++;
                                currentToken = currentToken + ch;
                                state = ParseState.Parse_State_Quote;
                            }
                            catch (Exception exception1)
                            {
                                exception = exception1;
                                this.lastErrorMessage = "Error in GetTokens() in Operand Quote Handling: " + exception.Message;
                                return;
                            }
                        }
                        goto Label_0FB6;

                    case ParseState.Parse_State_Operator:
                        if (ch != '"')
                        {
                            switch (ch)
                            {
                                case ' ':
                                    if (string.IsNullOrEmpty(currentToken.Trim()))
                                    {
                                        currentToken = "";
                                        goto Label_0FB6;
                                    }
                                    this.lastErrorMessage = "Error with expression syntax: Found a space while looking for an operator";
                                    return;

                                case '(':
                                    this.lastErrorMessage = "Error in rule syntax: Found an open parenthesis while searching for an operator";
                                    return;

                                case ')':
                                    num--;
                                    this.tokenItems.Add(new TokenItem(")", CMM.Dynamic.Calculator.Parser.TokenType.Token_Close_Parenthesis, false));
                                    goto Label_0FB6;

                                case '[':
                                    this.lastErrorMessage = "Error in rule syntax: Found an open square parenthesis while searching for an operator";
                                    return;

                                case ']':
                                    this.lastErrorMessage = "Error in rule syntax: Found a closed square parenthesis while searching for an operator";
                                    return;

                                case ',':
                                    this.lastErrorMessage = "Error in rule syntax: Found a comma while searching for an operator";
                                    return;

                                case ':':
                                    if (!this.FoundAssignment())
                                    {
                                        this.lastErrorMessage = "Error in Rule Syntax: Found a : (colon) but did not find an assignment token.";
                                        return;
                                    }
                                    num5++;
                                    if (this.tokenItems[this.tokenItems.Count - 1].TokenType == CMM.Dynamic.Calculator.Parser.TokenType.Token_Operand)
                                    {
                                        if (this.tokenItems[this.tokenItems.Count - 1].TokenDataType != TokenDataType.Token_DataType_Variable)
                                        {
                                            this.lastErrorMessage = "Error in Rule Syntax: An assignment can only be made to a variable.";
                                            return;
                                        }
                                    }
                                    else
                                    {
                                        this.lastErrorMessage = "Error in Rule Syntax: An assignment can only be made to a variable.";
                                        return;
                                    }
                                    this.tokenItems[this.tokenItems.Count - 1].WillBeAssigned = true;
                                    this.anyAssignments = true;
                                    this.tokenItems.Add(new TokenItem(":=", CMM.Dynamic.Calculator.Parser.TokenType.Token_Assignemt_Start, false));
                                    this.charIndex++;
                                    currentToken = "";
                                    state = ParseState.Parse_State_Operand;
                                    goto Label_0FB6;
                            }
                            if (ch == ';')
                            {
                                this.tokenItems.Add(new TokenItem(";", CMM.Dynamic.Calculator.Parser.TokenType.Token_Assignment_Stop, false));
                                num6++;
                                currentToken = "";
                                state = ParseState.Parse_State_Operand;
                            }
                            else
                            {
                                currentToken = currentToken + ch;
                                flag2 = this.CreateTokenItem(currentToken, true, false, out nextParseState, out isError, out this.lastErrorMessage);
                                if (isError)
                                {
                                    return;
                                }
                                if (flag2)
                                {
                                    currentToken = "";
                                    state = nextParseState;
                                }
                            }
                            goto Label_0FB6;
                        }
                        this.lastErrorMessage = "Error in Rule Syntax: Found a double quote (\") while looking for an operator.";
                        return;

                    case ParseState.Parse_State_Quote:
                        if (ch != '"')
                        {
                            currentToken = currentToken + ch;
                            goto Label_0FB6;
                        }
                        num4--;
                        currentToken = currentToken + ch;
                        flag2 = this.CreateTokenItem(currentToken, false, false, out nextParseState, out isError, out this.lastErrorMessage);
                        if (!isError)
                        {
                            if (flag2)
                            {
                                state = nextParseState;
                                currentToken = "";
                            }
                            goto Label_0FB6;
                        }
                        return;

                    case ParseState.Parse_State_OperandFunction:
                        switch (state3)
                        {
                            case ParseState.Parse_State_Operand:
                                switch (ch)
                                {
                                    case '"':
                                        num4++;
                                        currentToken = currentToken + ch;
                                        state3 = ParseState.Parse_State_Quote;
                                        goto Label_0FB6;

                                    case ' ':
                                        if (this.CreateTokenItem(currentToken, false, true, out nextParseState, out isError, out this.lastErrorMessage))
                                        {
                                            state3 = nextParseState;
                                            currentToken = "";
                                        }
                                        goto Label_0FB6;

                                    case '(':
                                        num++;
                                        this.CreateTokenItem(currentToken, false, true, out nextParseState, out isError, out this.lastErrorMessage);
                                        if (isError)
                                        {
                                            return;
                                        }
                                        this.tokenItems.Add(new TokenItem("(", CMM.Dynamic.Calculator.Parser.TokenType.Token_Open_Parenthesis, true));
                                        currentToken = "";
                                        state3 = ParseState.Parse_State_Operand;
                                        goto Label_0FB6;

                                    case ')':
                                        num--;
                                        this.CreateTokenItem(currentToken, false, true, out nextParseState, out isError, out this.lastErrorMessage);
                                        if (isError)
                                        {
                                            return;
                                        }
                                        this.tokenItems.Add(new TokenItem(")", CMM.Dynamic.Calculator.Parser.TokenType.Token_Close_Parenthesis, true));
                                        currentToken = "";
                                        state3 = ParseState.Parse_State_Operator;
                                        goto Label_0FB6;

                                    case '[':
                                        num2++;
                                        currentToken = currentToken.Trim();
                                        if (DataTypeCheck.IsOperandFunction(currentToken))
                                        {
                                            num7++;
                                            currentToken = currentToken + ch;
                                            this.tokenItems.Add(new TokenItem(currentToken, CMM.Dynamic.Calculator.Parser.TokenType.Token_Operand_Function_Start, true));
                                            state3 = ParseState.Parse_State_Operand;
                                            currentToken = "";
                                            goto Label_0FB6;
                                        }
                                        this.lastErrorMessage = "Error in Rule Syntax: Found an open square parenthesis without an operand function";
                                        return;

                                    case ']':
                                        num2--;
                                        this.CreateTokenItem(currentToken, false, true, out nextParseState, out isError, out this.lastErrorMessage);
                                        if (isError)
                                        {
                                            return;
                                        }
                                        this.tokenItems.Add(new TokenItem("]", CMM.Dynamic.Calculator.Parser.TokenType.Token_Operand_Function_Stop, true));
                                        num7--;
                                        state3 = ParseState.Parse_State_Operator;
                                        if (num7 <= 0)
                                        {
                                            num7 = 0;
                                            state = ParseState.Parse_State_Operator;
                                        }
                                        currentToken = "";
                                        goto Label_0FB6;

                                    case ',':
                                        this.CreateTokenItem(currentToken, false, true, out nextParseState, out isError, out this.lastErrorMessage);
                                        if (isError)
                                        {
                                            return;
                                        }
                                        this.tokenItems.Add(new TokenItem(",", CMM.Dynamic.Calculator.Parser.TokenType.Token_Operand_Function_Delimiter, true));
                                        currentToken = "";
                                        state3 = ParseState.Parse_State_Operand;
                                        goto Label_0FB6;

                                    case '-':
                                        if (string.IsNullOrEmpty(currentToken.Trim()))
                                        {
                                            currentToken = currentToken + ch;
                                        }
                                        else
                                        {
                                            currentToken = currentToken + ch;
                                            flag2 = this.CreateTokenItem(currentToken, true, true, out nextParseState, out isError, out this.lastErrorMessage);
                                            if (isError)
                                            {
                                                return;
                                            }
                                            if (flag2)
                                            {
                                                currentToken = "";
                                                state3 = nextParseState;
                                            }
                                        }
                                        goto Label_0FB6;
                                }
                                if ((ch == ':') || (ch == ';'))
                                {
                                    this.lastErrorMessage = "Error in Rule Syntax: Assignments are not allowed within Operand Functions";
                                    return;
                                }
                                currentToken = currentToken + ch;
                                flag2 = this.CreateTokenItem(currentToken, true, true, out nextParseState, out isError, out this.lastErrorMessage);
                                if (isError)
                                {
                                    return;
                                }
                                if (flag2)
                                {
                                    currentToken = "";
                                    state3 = nextParseState;
                                }
                                goto Label_0FB6;

                            case ParseState.Parse_State_Operator:
                                switch (ch)
                                {
                                    case '"':
                                        this.lastErrorMessage = "Error in Rule Syntax: Found a double quote (\") while looking for an operator.";
                                        return;

                                    case ' ':
                                        if (string.IsNullOrEmpty(currentToken.Trim()))
                                        {
                                            currentToken = "";
                                            goto Label_0FB6;
                                        }
                                        this.lastErrorMessage = "Error with expression syntax: Found a space while looking for an operator";
                                        return;

                                    case '(':
                                        this.lastErrorMessage = "Error in rule syntax: Found an open parenthesis while searching for an operator";
                                        return;

                                    case ')':
                                        num--;
                                        this.tokenItems.Add(new TokenItem(")", CMM.Dynamic.Calculator.Parser.TokenType.Token_Close_Parenthesis, true));
                                        goto Label_0FB6;

                                    case '[':
                                        this.lastErrorMessage = "Error in rule syntax: Found an open square parenthesis while searching for an operator";
                                        return;

                                    case ']':
                                        num2--;
                                        this.CreateTokenItem(currentToken, false, true, out nextParseState, out isError, out this.lastErrorMessage);
                                        if (isError)
                                        {
                                            return;
                                        }
                                        this.tokenItems.Add(new TokenItem("]", CMM.Dynamic.Calculator.Parser.TokenType.Token_Operand_Function_Stop, true));
                                        num7--;
                                        state3 = ParseState.Parse_State_Operand;
                                        if (num7 <= 0)
                                        {
                                            num7 = 0;
                                            state = ParseState.Parse_State_Operator;
                                        }
                                        currentToken = "";
                                        goto Label_0FB6;

                                    case ',':
                                        this.CreateTokenItem(currentToken, false, true, out nextParseState, out isError, out this.lastErrorMessage);
                                        if (isError)
                                        {
                                            return;
                                        }
                                        this.tokenItems.Add(new TokenItem(",", CMM.Dynamic.Calculator.Parser.TokenType.Token_Operand_Function_Delimiter, true));
                                        currentToken = "";
                                        state3 = ParseState.Parse_State_Operand;
                                        goto Label_0FB6;
                                }
                                if ((ch == ':') || (ch == ';'))
                                {
                                    this.lastErrorMessage = "Error in Rule Syntax: Assignments are not allowed within Operand Functions";
                                    return;
                                }
                                currentToken = currentToken + ch;
                                flag2 = this.CreateTokenItem(currentToken, true, true, out nextParseState, out isError, out this.lastErrorMessage);
                                if (isError)
                                {
                                    return;
                                }
                                if (flag2)
                                {
                                    currentToken = "";
                                    state3 = ParseState.Parse_State_Operand;
                                }
                                goto Label_0FB6;

                            case ParseState.Parse_State_Quote:
                                if (ch == '"')
                                {
                                    num4--;
                                    currentToken = currentToken + ch;
                                    flag2 = this.CreateTokenItem(currentToken, false, true, out nextParseState, out isError, out this.lastErrorMessage);
                                    if (isError)
                                    {
                                        return;
                                    }
                                    if (flag2)
                                    {
                                        state3 = nextParseState;
                                        currentToken = "";
                                    }
                                }
                                else
                                {
                                    currentToken = currentToken + ch;
                                }
                                goto Label_0FB6;
                        }
                        goto Label_0FB6;
                }
            }
        Label_0FB6:
            if (this.charIndex < this.ruleSyntax.Length)
            {
                goto Label_0052;
            }
        Label_0FD2:
            currentToken = currentToken.Trim();
            if (!string.IsNullOrEmpty(currentToken))
            {
                if (currentToken == "(")
                {
                    num++;
                }
                else if (currentToken == ")")
                {
                    num--;
                }
                else if (currentToken == "[")
                {
                    num2++;
                }
                else if (currentToken == "]")
                {
                    num2--;
                }
                else if (currentToken == "~")
                {
                    num3--;
                }
                else if (currentToken == "\"")
                {
                    num4--;
                }
                switch (state)
                {
                    case ParseState.Parse_State_Operand:
                        this.CreateTokenItem(currentToken, false, false, out nextParseState, out isError, out this.lastErrorMessage);
                        if (!isError)
                        {
                            break;
                        }
                        return;

                    case ParseState.Parse_State_Operator:
                        this.lastErrorMessage = "Error in Rule Syntax: A rule cannot end with an operator.";
                        break;

                    case ParseState.Parse_State_Quote:
                        if (!(currentToken != "\""))
                        {
                            this.tokenItems.Add(new TokenItem(currentToken, CMM.Dynamic.Calculator.Parser.TokenType.Token_Operand, TokenDataType.Token_DataType_String, false));
                            break;
                        }
                        this.lastErrorMessage = "Error in RuleSyntax: Double quote mismatch.";
                        return;
                }
            }
            if (num != 0)
            {
                this.lastErrorMessage = "Error in RuleSyntax: There is a parenthesis mismatch.";
            }
            else if (num2 != 0)
            {
                this.lastErrorMessage = "Error in RuleSyntax: There is an operand function mismatch.";
            }
            else if (num7 > 0)
            {
                this.lastErrorMessage = "Error in RuleSyntax: There is an operand function mismatch error...Operand function depth is not zero.";
            }
            else if (num3 != 0)
            {
                this.lastErrorMessage = "Error in RuleSyntax: There is a comment mismatch.";
            }
            else if (num4 != 0)
            {
                this.lastErrorMessage = "Error in RuleSyntax: There is a quote mismatch.";
            }
            else if (this.charIndex < this.ruleSyntax.Length)
            {
                this.lastErrorMessage = "Error in RuleSyntax: There was a problem parsing the rule...some of the tokens were not found.";
            }
            else if (num5 != num6)
            {
                this.lastErrorMessage = "Error in RuleSyntax: There was a problem parsing the rule...there was an assignment mismatch.";
            }
            else
            {
                if (!this.AnyErrors)
                {
                    this.MakeRPNQueue();
                }
                if (this.rpn_queue == null)
                {
                    this.lastErrorMessage = "Error in RuleSyntax: There was a problem creating the RPN queue.";
                }
                else if (this.rpn_queue.Count == 0)
                {
                    this.lastErrorMessage = "Error in RuleSyntax: There was a problem creating the RPN queue.";
                }
                else
                {
                    stopwatch.Stop();
                    this.tokenParseTime = stopwatch.Elapsed.TotalMilliseconds;
                }
            }
        }

        private void MakeRPNQueue()
        {
            this.lastErrorMessage = "";
            if (this.tokenItems.Count == 0)
            {
                this.lastErrorMessage = "No tokens to add to the RPN stack";
            }
            else
            {
                this.rpn_queue = new ExQueue<TokenItem>(this.tokenItems.Count);
                ExStack<TokenItem> stack = new ExStack<TokenItem>();
                ExStack<TokenItem> stack2 = new ExStack<TokenItem>();
                ExQueue<TokenItem> queue = new ExQueue<TokenItem>();
                bool flag = false;
                TokenItem item = null;
                IIFShortCircuitState state = IIFShortCircuitState.ShortCircuit_Condition;
                int num = 0;
                TokenItem item2 = new TokenItem(",", CMM.Dynamic.Calculator.Parser.TokenType.Token_Operand_Function_Delimiter, false);
                foreach (TokenItem item3 in this.tokenItems)
                {
                    TokenItem item4;
                    TokenItem item6;
                    bool flag2;
                    Debug.WriteLine(item3.TokenName);
                    switch (item3.TokenType)
                    {
                        case CMM.Dynamic.Calculator.Parser.TokenType.Token_Operand:
                            if (!item3.InOperandFunction)
                            {
                                goto Label_021E;
                            }
                            queue.Add(item3);
                            goto Label_0807;

                        case CMM.Dynamic.Calculator.Parser.TokenType.Token_Operand_Function_Start:
                            if (!item3.InOperandFunction)
                            {
                                goto Label_0404;
                            }
                            if (!item3.CanShortCircuit)
                            {
                                goto Label_03ED;
                            }
                            this.rpn_queue.Add(item3);
                            goto Label_0414;

                        case CMM.Dynamic.Calculator.Parser.TokenType.Token_Open_Parenthesis:
                            if (!item3.InOperandFunction)
                            {
                                goto Label_01F1;
                            }
                            stack2.Push(item3);
                            goto Label_0807;

                        case CMM.Dynamic.Calculator.Parser.TokenType.Token_Close_Parenthesis:
                            break;

                        case CMM.Dynamic.Calculator.Parser.TokenType.Token_Operand_Function_Stop:
                            goto Label_043E;

                        case CMM.Dynamic.Calculator.Parser.TokenType.Token_Operand_Function_Delimiter:
                            goto Label_0233;

                        case CMM.Dynamic.Calculator.Parser.TokenType.Token_Operator:
                            goto Label_0654;

                        case CMM.Dynamic.Calculator.Parser.TokenType.Token_Assignemt_Start:
                            goto Label_073E;

                        case CMM.Dynamic.Calculator.Parser.TokenType.Token_Assignment_Stop:
                            goto Label_07AA;

                        default:
                            goto Label_0807;
                    }
                    if (!item3.InOperandFunction)
                    {
                        goto Label_014E;
                    }
                    if (stack2.Count <= 0)
                    {
                        goto Label_0807;
                    }
                Label_00FC:
                    if (stack2.Peek().TokenType == CMM.Dynamic.Calculator.Parser.TokenType.Token_Open_Parenthesis)
                    {
                        stack2.Pop();
                    }
                    else
                    {
                        queue.Add(stack2.Pop());
                        if (stack2.Count != 0)
                        {
                            flag2 = true;
                            goto Label_00FC;
                        }
                    }
                    goto Label_0807;
                Label_014E:
                    if (stack.Count <= 0)
                    {
                        goto Label_0807;
                    }
                Label_0162:
                    Debug.WriteLine("    Peek = " + stack.Peek().TokenName);
                    if (stack.Peek().TokenType == CMM.Dynamic.Calculator.Parser.TokenType.Token_Open_Parenthesis)
                    {
                        stack.Pop();
                    }
                    else
                    {
                        this.rpn_queue.Add(stack.Pop());
                        if (stack.Count != 0)
                        {
                            flag2 = true;
                            goto Label_0162;
                        }
                    }
                    goto Label_0807;
                Label_01F1:
                    stack.Push(item3);
                    goto Label_0807;
                Label_021E:
                    this.rpn_queue.Add(item3);
                    goto Label_0807;
                Label_0233:
                    if (stack2.Count != 0)
                    {
                        item4 = stack2.Pop();
                        if (item4.TokenType != CMM.Dynamic.Calculator.Parser.TokenType.Token_Operand_Function_Delimiter)
                        {
                            queue.Add(item4);
                            flag2 = true;
                            goto Label_0233;
                        }
                    }
                Label_0274:
                    if (queue.Count == 0)
                    {
                        goto Label_035B;
                    }
                    TokenItem item5 = queue.Dequeue();
                    if (flag)
                    {
                        if (item5.TokenName.Trim().ToLower() != "iif[")
                        {
                            switch (state)
                            {
                                case IIFShortCircuitState.ShortCircuit_Condition:
                                    item.ShortCircuit.RPNCondition.Add(item5);
                                    goto Label_0334;

                                case IIFShortCircuitState.ShortCircuit_True:
                                    item.ShortCircuit.RPNTrue.Add(item5);
                                    goto Label_0334;

                                case IIFShortCircuitState.ShortCircuit_False:
                                    item.ShortCircuit.RPNFalse.Add(item5);
                                    goto Label_0334;
                            }
                        }
                    }
                    else
                    {
                        this.rpn_queue.Add(item5);
                    }
                Label_0334:
                    if (flag || (item5.TokenType != CMM.Dynamic.Calculator.Parser.TokenType.Token_Operand_Function_Start))
                    {
                        flag2 = true;
                        goto Label_0274;
                    }
                Label_035B:
                    stack2.Push(item3);
                    if (flag)
                    {
                        if (num == 0)
                        {
                            switch (state)
                            {
                                case IIFShortCircuitState.ShortCircuit_Condition:
                                    state = IIFShortCircuitState.ShortCircuit_True;
                                    goto Label_0807;

                                case IIFShortCircuitState.ShortCircuit_True:
                                    state = IIFShortCircuitState.ShortCircuit_False;
                                    goto Label_0807;

                                case IIFShortCircuitState.ShortCircuit_False:
                                    goto Label_0807;
                            }
                        }
                    }
                    else
                    {
                        this.rpn_queue.Add(item3);
                    }
                    goto Label_0807;
                Label_03ED:
                    queue.Add(item3);
                    stack2.Push(item2);
                    goto Label_0414;
                Label_0404:
                    this.rpn_queue.Add(item3);
                Label_0414:
                    if (item3.CanShortCircuit)
                    {
                        flag = true;
                        item = item3;
                        state = IIFShortCircuitState.ShortCircuit_Condition;
                    }
                    else
                    {
                        num++;
                    }
                    goto Label_0807;
                Label_043E:
                    if (!item3.InOperandFunction)
                    {
                        goto Label_063F;
                    }
                    while (true)
                    {
                        if (stack2.Count == 0)
                        {
                            goto Label_0493;
                        }
                        item4 = stack2.Pop();
                        if (item4.TokenType == CMM.Dynamic.Calculator.Parser.TokenType.Token_Operand_Function_Delimiter)
                        {
                            goto Label_0493;
                        }
                        queue.Add(item4);
                        flag2 = true;
                    }
                Label_0493:
                    queue.Add(item3);
                Label_049C:
                    if (queue.Count == 0)
                    {
                        goto Label_0610;
                    }
                    item5 = queue.Dequeue();
                    if (flag)
                    {
                        if ((num == 0) && (state != IIFShortCircuitState.ShortCircuit_False))
                        {
                            this.lastErrorMessage = "Error parsing the iif[] short circuit operand function: Invalid format: Expecting the false condition";
                            return;
                        }
                        if (item5.TokenType == CMM.Dynamic.Calculator.Parser.TokenType.Token_Operand_Function_Stop)
                        {
                            if (num == 0)
                            {
                                this.rpn_queue.Add(item5);
                            }
                            else
                            {
                                switch (state)
                                {
                                    case IIFShortCircuitState.ShortCircuit_Condition:
                                        item.ShortCircuit.RPNCondition.Add(item5);
                                        goto Label_05F2;

                                    case IIFShortCircuitState.ShortCircuit_True:
                                        item.ShortCircuit.RPNTrue.Add(item5);
                                        goto Label_05F2;

                                    case IIFShortCircuitState.ShortCircuit_False:
                                        item.ShortCircuit.RPNFalse.Add(item5);
                                        goto Label_05F2;
                                }
                            }
                        }
                        else
                        {
                            switch (state)
                            {
                                case IIFShortCircuitState.ShortCircuit_Condition:
                                    item.ShortCircuit.RPNCondition.Add(item5);
                                    goto Label_05F2;

                                case IIFShortCircuitState.ShortCircuit_True:
                                    item.ShortCircuit.RPNTrue.Add(item5);
                                    goto Label_05F2;

                                case IIFShortCircuitState.ShortCircuit_False:
                                    item.ShortCircuit.RPNFalse.Add(item5);
                                    goto Label_05F2;
                            }
                        }
                    }
                    else
                    {
                        this.rpn_queue.Add(item5);
                    }
                Label_05F2:
                    if (item5.TokenType != CMM.Dynamic.Calculator.Parser.TokenType.Token_Operand_Function_Stop)
                    {
                        flag2 = true;
                        goto Label_049C;
                    }
                Label_0610:
                    if (flag)
                    {
                        if (num == 0)
                        {
                            flag = false;
                            item = null;
                            state = IIFShortCircuitState.ShortCircuit_Condition;
                        }
                        else
                        {
                            num--;
                        }
                    }
                    goto Label_0807;
                Label_063F:
                    this.rpn_queue.Add(item3);
                    goto Label_0807;
                Label_0654:
                    if (!item3.InOperandFunction)
                    {
                        goto Label_06CD;
                    }
                    if (stack2.Count <= 0)
                    {
                        goto Label_06C1;
                    }
                Label_0678:
                    if (item3.OrderOfOperationPrecedence >= stack2.Peek().OrderOfOperationPrecedence)
                    {
                        queue.Add(stack2.Pop());
                        if (stack2.Count != 0)
                        {
                            flag2 = true;
                            goto Label_0678;
                        }
                    }
                Label_06C1:
                    stack2.Push(item3);
                    goto Label_0807;
                Label_06CD:
                    if (stack.Count <= 0)
                    {
                        goto Label_072F;
                    }
                Label_06E1:
                    if (item3.OrderOfOperationPrecedence >= stack.Peek().OrderOfOperationPrecedence)
                    {
                        this.rpn_queue.Add(stack.Pop());
                        if (stack.Count != 0)
                        {
                            flag2 = true;
                            goto Label_06E1;
                        }
                    }
                Label_072F:
                    stack.Push(item3);
                    goto Label_0807;
                Label_073E:
                    if (stack.Count <= 0)
                    {
                        goto Label_079F;
                    }
                Label_0751:
                    if (item3.OrderOfOperationPrecedence >= stack.Peek().OrderOfOperationPrecedence)
                    {
                        this.rpn_queue.Add(stack.Pop());
                        if (stack.Count != 0)
                        {
                            flag2 = true;
                            goto Label_0751;
                        }
                    }
                Label_079F:
                    stack.Push(item3);
                    goto Label_0807;
                Label_07AA:
                    if (stack.Count <= 0)
                    {
                        goto Label_0807;
                    }
                Label_07BD:
                    item6 = stack.Pop();
                    this.rpn_queue.Add(item6);
                    if ((item6.TokenType != CMM.Dynamic.Calculator.Parser.TokenType.Token_Assignemt_Start) && (stack.Count != 0))
                    {
                        flag2 = true;
                        goto Label_07BD;
                    }
                Label_0807:;
                }
                int count = stack.Count;
                for (int i = 0; i < count; i++)
                {
                    this.rpn_queue.Add(stack.Pop());
                }
            }
        }

        private bool Open(FileInfo File, out string ErrorMsg)
        {
            Exception exception;
            int num;
            string str2;
            string str3;
            ErrorMsg = "";
            TextReader reader = null;
            try
            {
                reader = new StreamReader(File.FullName);
            }
            catch (Exception exception1)
            {
                exception = exception1;
                ErrorMsg = "Error in Tokens.Open() while reading the file '" + File.FullName + "' : " + exception.Message;
                return false;
            }
            string str = "";
            bool flag = false;
            try
            {
                str = reader.ReadToEnd();
            }
            catch (Exception exception2)
            {
                exception = exception2;
                flag = true;
                ErrorMsg = "Error in Tokens.Open() while reading the file: " + exception.Message;
            }
            finally
            {
                reader.Close();
            }
            if (flag)
            {
                return false;
            }
            string[] strArray = str.Split("\n".ToCharArray());
            SortedList<string, string> list = new SortedList<string, string>();
            StringBuilder builder = new StringBuilder();
            bool flag2 = true;
            bool flag3 = false;
            for (num = 0; num < strArray.Length; num++)
            {
                string str4 = strArray[num].Trim().ToLower();
                if (str4 == null)
                {
                    goto Label_0116;
                }
                if (!(str4 == ";variables;"))
                {
                    if (str4 == ";tokens;")
                    {
                        goto Label_0106;
                    }
                    if (str4 == ";rpn queue;")
                    {
                        goto Label_010E;
                    }
                    goto Label_0116;
                }
                flag3 = true;
                goto Label_019B;
            Label_0106:
                flag2 = false;
                goto Label_019B;
            Label_010E:
                flag2 = false;
                goto Label_019B;
            Label_0116:
                if (!flag3)
                {
                    builder.Append(strArray[num]);
                    builder.Append("\n");
                }
                else
                {
                    string[] strArray2 = strArray[num].Split("=".ToCharArray());
                    if (strArray2.Length == 2)
                    {
                        str2 = strArray2[0].Trim().ToLower();
                        str3 = strArray2[1];
                        if (!list.ContainsKey(str2))
                        {
                            list.Add(str2, str3);
                        }
                    }
                }
            Label_019B:
                if (!flag2)
                {
                    break;
                }
            }
            this.ruleSyntax = builder.ToString();
            this.GetTokens();
            if (this.AnyErrors)
            {
                ErrorMsg = this.lastErrorMessage;
                return false;
            }
            if (list.Count > 0)
            {
                for (num = 0; num < list.Count; num++)
                {
                    str2 = list.Keys[num];
                    str3 = list[str2];
                    str3 = str3.Replace("\r", "");
                    if (this.variables.VariableExists(str2))
                    {
                        this.variables[str2].VariableValue = str3;
                    }
                }
            }
            return true;
        }

        public bool Save(string Filename, out string ErrorMsg)
        {
            Exception exception;
            ErrorMsg = "";
            if (string.IsNullOrEmpty(this.ruleSyntax))
            {
                ErrorMsg = "Error in Tokens.Save(): No rule to save";
                return false;
            }
            TextWriter writer = null;
            try
            {
                writer = new StreamWriter(Filename, false);
            }
            catch (Exception exception1)
            {
                exception = exception1;
                ErrorMsg = "Error in Tokens.Save() while saving the rule: " + exception.Message;
                return false;
            }
            try
            {
                writer.WriteLine(this.ruleSyntax);
            }
            catch (Exception exception2)
            {
                exception = exception2;
                writer.Close();
                ErrorMsg = "Error in Tokens.Save() while saving the rule syntax: " + exception.Message;
                return false;
            }
            try
            {
                writer.WriteLine("");
                writer.WriteLine(";Variables;");
                if (this.variables != null)
                {
                    foreach (Variable variable in this.variables)
                    {
                        writer.Write(variable.VariableName);
                        writer.Write("=");
                        if (variable.VariableValue != null)
                        {
                            writer.WriteLine(variable.VariableValue);
                        }
                        else
                        {
                            writer.WriteLine("null");
                        }
                    }
                }
            }
            catch (Exception exception3)
            {
                exception = exception3;
                writer.Close();
                ErrorMsg = "Error in Tokens.Save() while saving the varaibles: " + exception.Message;
                return false;
            }
            try
            {
                writer.WriteLine("");
                writer.WriteLine(";Tokens;");
                if (this.tokenItems != null)
                {
                    foreach (TokenItem item in this.tokenItems)
                    {
                        writer.Write(item.TokenName);
                        writer.Write(",");
                        writer.Write(item.TokenType.ToString());
                        writer.Write(",");
                        writer.Write(item.TokenDataType.ToString());
                        writer.Write(",");
                        writer.WriteLine(item.InOperandFunction.ToString());
                    }
                }
            }
            catch (Exception exception4)
            {
                exception = exception4;
                writer.Close();
                ErrorMsg = "Error in Tokens.Save() while saving the tokens: " + exception.Message;
                return false;
            }
            try
            {
                writer.WriteLine("");
                writer.WriteLine(";RPN Queue;");
                if (this.rpn_queue != null)
                {
                    foreach (TokenItem item in this.rpn_queue)
                    {
                        writer.Write(item.TokenName);
                        writer.Write(",");
                        writer.Write(item.TokenType.ToString());
                        writer.Write(",");
                        writer.Write(item.TokenDataType.ToString());
                        writer.Write(",");
                        writer.WriteLine(item.InOperandFunction.ToString());
                    }
                }
            }
            catch (Exception exception5)
            {
                exception = exception5;
                writer.Close();
                ErrorMsg = "Error in Tokens.Save() while saving the tokens: " + exception.Message;
                return false;
            }
            writer.Close();
            return true;
        }

        public bool AnyAssignments
        {
            get
            {
                return this.anyAssignments;
            }
        }

        public bool AnyErrors
        {
            get
            {
                return !string.IsNullOrEmpty(this.lastErrorMessage);
            }
        }

        public int CharIndex
        {
            get
            {
                return this.charIndex;
            }
        }

        public string LastErrorMessage
        {
            get
            {
                return this.lastErrorMessage;
            }
            set
            {
                this.lastErrorMessage = value;
            }
        }

        public string LastEvaluationResult
        {
            get
            {
                return this.lastEvaluationResult;
            }
            set
            {
                this.lastEvaluationResult = value;
            }
        }

        public double LastEvaluationTime
        {
            get
            {
                return this.lastEvaluationTime;
            }
            set
            {
                this.lastEvaluationTime = value;
            }
        }

        public ExQueue<TokenItem> RPNQueue
        {
            get
            {
                return this.rpn_queue;
            }
        }

        public string RuleSyntax
        {
            get
            {
                return this.ruleSyntax;
            }
        }

        public CMM.Dynamic.Calculator.Parser.TokenGroup TokenGroup
        {
            get
            {
                return this.tokenGroup;
            }
            set
            {
                this.tokenGroup = value;
            }
        }

        public CMM.Dynamic.Calculator.Parser.TokenItems TokenItems
        {
            get
            {
                return this.tokenItems;
            }
        }

        public double TokenParseTime
        {
            get
            {
                return this.tokenParseTime;
            }
        }

        public CMM.Dynamic.Calculator.Parser.Variables Variables
        {
            get
            {
                return this.variables;
            }
        }
    }
}

