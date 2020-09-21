namespace CMM.Dynamic.Calculator.Parser
{
    using System;

    public enum ParseState
    {
        Parse_State_Operand,
        Parse_State_Operator,
        Parse_State_Quote,
        Parse_State_OperandFunction,
        Parse_State_Comment
    }
}

