namespace CMM.Dynamic.Calculator.Parser
{
    using System;

    public enum TokenType
    {
        Token_Operand,
        Token_Operand_Function_Start,
        Token_Open_Parenthesis,
        Token_Close_Parenthesis,
        Token_Operand_Function_Stop,
        Token_Operand_Function_Delimiter,
        Token_Operator,
        Token_Assignemt_Start,
        Token_Assignment_Stop
    }
}

