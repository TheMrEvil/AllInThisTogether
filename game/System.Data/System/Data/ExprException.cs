using System;
using System.Globalization;

namespace System.Data
{
	// Token: 0x020000F4 RID: 244
	internal sealed class ExprException
	{
		// Token: 0x06000E66 RID: 3686 RVA: 0x00003D93 File Offset: 0x00001F93
		private ExprException()
		{
		}

		// Token: 0x06000E67 RID: 3687 RVA: 0x0003CC3B File Offset: 0x0003AE3B
		private static OverflowException _Overflow(string error)
		{
			OverflowException ex = new OverflowException(error);
			ExceptionBuilder.TraceExceptionAsReturnValue(ex);
			return ex;
		}

		// Token: 0x06000E68 RID: 3688 RVA: 0x0003CC4A File Offset: 0x0003AE4A
		private static InvalidExpressionException _Expr(string error)
		{
			InvalidExpressionException ex = new InvalidExpressionException(error);
			ExceptionBuilder.TraceExceptionAsReturnValue(ex);
			return ex;
		}

		// Token: 0x06000E69 RID: 3689 RVA: 0x0003CC59 File Offset: 0x0003AE59
		private static SyntaxErrorException _Syntax(string error)
		{
			SyntaxErrorException ex = new SyntaxErrorException(error);
			ExceptionBuilder.TraceExceptionAsReturnValue(ex);
			return ex;
		}

		// Token: 0x06000E6A RID: 3690 RVA: 0x0003CC68 File Offset: 0x0003AE68
		private static EvaluateException _Eval(string error)
		{
			EvaluateException ex = new EvaluateException(error);
			ExceptionBuilder.TraceExceptionAsReturnValue(ex);
			return ex;
		}

		// Token: 0x06000E6B RID: 3691 RVA: 0x0003CC68 File Offset: 0x0003AE68
		private static EvaluateException _Eval(string error, Exception innerException)
		{
			EvaluateException ex = new EvaluateException(error);
			ExceptionBuilder.TraceExceptionAsReturnValue(ex);
			return ex;
		}

		// Token: 0x06000E6C RID: 3692 RVA: 0x0003CC77 File Offset: 0x0003AE77
		public static Exception InvokeArgument()
		{
			return ExceptionBuilder._Argument("Need a row or a table to Invoke DataFilter.");
		}

		// Token: 0x06000E6D RID: 3693 RVA: 0x0003CC83 File Offset: 0x0003AE83
		public static Exception NYI(string moreinfo)
		{
			return ExprException._Expr(SR.Format("The feature not implemented. {0}.", moreinfo));
		}

		// Token: 0x06000E6E RID: 3694 RVA: 0x0003CC95 File Offset: 0x0003AE95
		public static Exception MissingOperand(OperatorInfo before)
		{
			return ExprException._Syntax(SR.Format("Syntax error: Missing operand after '{0}' operator.", Operators.ToString(before._op)));
		}

		// Token: 0x06000E6F RID: 3695 RVA: 0x0003CCB1 File Offset: 0x0003AEB1
		public static Exception MissingOperator(string token)
		{
			return ExprException._Syntax(SR.Format("Syntax error: Missing operand after '{0}' operator.", token));
		}

		// Token: 0x06000E70 RID: 3696 RVA: 0x0003CCC3 File Offset: 0x0003AEC3
		public static Exception TypeMismatch(string expr)
		{
			return ExprException._Eval(SR.Format("Type mismatch in expression '{0}'.", expr));
		}

		// Token: 0x06000E71 RID: 3697 RVA: 0x0003CCD5 File Offset: 0x0003AED5
		public static Exception FunctionArgumentOutOfRange(string arg, string func)
		{
			return ExceptionBuilder._ArgumentOutOfRange(arg, SR.Format("{0}() argument is out of range.", func));
		}

		// Token: 0x06000E72 RID: 3698 RVA: 0x0003CCE8 File Offset: 0x0003AEE8
		public static Exception ExpressionTooComplex()
		{
			return ExprException._Eval("Expression is too complex.");
		}

		// Token: 0x06000E73 RID: 3699 RVA: 0x0003CCF4 File Offset: 0x0003AEF4
		public static Exception UnboundName(string name)
		{
			return ExprException._Eval(SR.Format("Cannot find column [{0}].", name));
		}

		// Token: 0x06000E74 RID: 3700 RVA: 0x0003CD06 File Offset: 0x0003AF06
		public static Exception InvalidString(string str)
		{
			return ExprException._Syntax(SR.Format("The expression contains an invalid string constant: {0}.", str));
		}

		// Token: 0x06000E75 RID: 3701 RVA: 0x0003CD18 File Offset: 0x0003AF18
		public static Exception UndefinedFunction(string name)
		{
			return ExprException._Eval(SR.Format("The expression contains undefined function call {0}().", name));
		}

		// Token: 0x06000E76 RID: 3702 RVA: 0x0003CD2A File Offset: 0x0003AF2A
		public static Exception SyntaxError()
		{
			return ExprException._Syntax("Syntax error in the expression.");
		}

		// Token: 0x06000E77 RID: 3703 RVA: 0x0003CD36 File Offset: 0x0003AF36
		public static Exception FunctionArgumentCount(string name)
		{
			return ExprException._Eval(SR.Format("Invalid number of arguments: function {0}().", name));
		}

		// Token: 0x06000E78 RID: 3704 RVA: 0x0003CD48 File Offset: 0x0003AF48
		public static Exception MissingRightParen()
		{
			return ExprException._Syntax("The expression is missing the closing parenthesis.");
		}

		// Token: 0x06000E79 RID: 3705 RVA: 0x0003CD54 File Offset: 0x0003AF54
		public static Exception UnknownToken(string token, int position)
		{
			return ExprException._Syntax(SR.Format("Cannot interpret token '{0}' at position {1}.", token, position.ToString(CultureInfo.InvariantCulture)));
		}

		// Token: 0x06000E7A RID: 3706 RVA: 0x0003CD72 File Offset: 0x0003AF72
		public static Exception UnknownToken(Tokens tokExpected, Tokens tokCurr, int position)
		{
			return ExprException._Syntax(SR.Format("Expected {0}, but actual token at the position {2} is {1}.", tokExpected.ToString(), tokCurr.ToString(), position.ToString(CultureInfo.InvariantCulture)));
		}

		// Token: 0x06000E7B RID: 3707 RVA: 0x0003CDA9 File Offset: 0x0003AFA9
		public static Exception DatatypeConvertion(Type type1, Type type2)
		{
			return ExprException._Eval(SR.Format("Cannot convert from {0} to {1}.", type1.ToString(), type2.ToString()));
		}

		// Token: 0x06000E7C RID: 3708 RVA: 0x0003CDC6 File Offset: 0x0003AFC6
		public static Exception DatavalueConvertion(object value, Type type, Exception innerException)
		{
			return ExprException._Eval(SR.Format("Cannot convert value '{0}' to Type: {1}.", value.ToString(), type.ToString()), innerException);
		}

		// Token: 0x06000E7D RID: 3709 RVA: 0x0003CDE4 File Offset: 0x0003AFE4
		public static Exception InvalidName(string name)
		{
			return ExprException._Syntax(SR.Format("Invalid column name [{0}].", name));
		}

		// Token: 0x06000E7E RID: 3710 RVA: 0x0003CDF6 File Offset: 0x0003AFF6
		public static Exception InvalidDate(string date)
		{
			return ExprException._Syntax(SR.Format("The expression contains invalid date constant '{0}'.", date));
		}

		// Token: 0x06000E7F RID: 3711 RVA: 0x0003CE08 File Offset: 0x0003B008
		public static Exception NonConstantArgument()
		{
			return ExprException._Eval("Only constant expressions are allowed in the expression list for the IN operator.");
		}

		// Token: 0x06000E80 RID: 3712 RVA: 0x0003CE14 File Offset: 0x0003B014
		public static Exception InvalidPattern(string pat)
		{
			return ExprException._Eval(SR.Format("Error in Like operator: the string pattern '{0}' is invalid.", pat));
		}

		// Token: 0x06000E81 RID: 3713 RVA: 0x0003CE26 File Offset: 0x0003B026
		public static Exception InWithoutParentheses()
		{
			return ExprException._Syntax("Syntax error: The items following the IN keyword must be separated by commas and be enclosed in parentheses.");
		}

		// Token: 0x06000E82 RID: 3714 RVA: 0x0003CE32 File Offset: 0x0003B032
		public static Exception InWithoutList()
		{
			return ExprException._Syntax("Syntax error: The IN keyword must be followed by a non-empty list of expressions separated by commas, and also must be enclosed in parentheses.");
		}

		// Token: 0x06000E83 RID: 3715 RVA: 0x0003CE3E File Offset: 0x0003B03E
		public static Exception InvalidIsSyntax()
		{
			return ExprException._Syntax("Syntax error: Invalid usage of 'Is' operator. Correct syntax: <expression> Is [Not] Null.");
		}

		// Token: 0x06000E84 RID: 3716 RVA: 0x0003CE4A File Offset: 0x0003B04A
		public static Exception Overflow(Type type)
		{
			return ExprException._Overflow(SR.Format("Value is either too large or too small for Type '{0}'.", type.Name));
		}

		// Token: 0x06000E85 RID: 3717 RVA: 0x0003CE61 File Offset: 0x0003B061
		public static Exception ArgumentType(string function, int arg, Type type)
		{
			return ExprException._Eval(SR.Format("Type mismatch in function argument: {0}(), argument {1}, expected {2}.", function, arg.ToString(CultureInfo.InvariantCulture), type.ToString()));
		}

		// Token: 0x06000E86 RID: 3718 RVA: 0x0003CE85 File Offset: 0x0003B085
		public static Exception ArgumentTypeInteger(string function, int arg)
		{
			return ExprException._Eval(SR.Format("Type mismatch in function argument: {0}(), argument {1}, expected one of the Integer types.", function, arg.ToString(CultureInfo.InvariantCulture)));
		}

		// Token: 0x06000E87 RID: 3719 RVA: 0x0003CEA3 File Offset: 0x0003B0A3
		public static Exception TypeMismatchInBinop(int op, Type type1, Type type2)
		{
			return ExprException._Eval(SR.Format("Cannot perform '{0}' operation on {1} and {2}.", Operators.ToString(op), type1.ToString(), type2.ToString()));
		}

		// Token: 0x06000E88 RID: 3720 RVA: 0x0003CEC6 File Offset: 0x0003B0C6
		public static Exception AmbiguousBinop(int op, Type type1, Type type2)
		{
			return ExprException._Eval(SR.Format("Operator '{0}' is ambiguous on operands of type '{1}' and '{2}'. Cannot mix signed and unsigned types. Please use explicit Convert() function.", Operators.ToString(op), type1.ToString(), type2.ToString()));
		}

		// Token: 0x06000E89 RID: 3721 RVA: 0x0003CEE9 File Offset: 0x0003B0E9
		public static Exception UnsupportedOperator(int op)
		{
			return ExprException._Eval(SR.Format("The expression contains unsupported operator '{0}'.", Operators.ToString(op)));
		}

		// Token: 0x06000E8A RID: 3722 RVA: 0x0003CF00 File Offset: 0x0003B100
		public static Exception InvalidNameBracketing(string name)
		{
			return ExprException._Syntax(SR.Format("The expression contains invalid name: '{0}'.", name));
		}

		// Token: 0x06000E8B RID: 3723 RVA: 0x0003CF12 File Offset: 0x0003B112
		public static Exception MissingOperandBefore(string op)
		{
			return ExprException._Syntax(SR.Format("Syntax error: Missing operand before '{0}' operator.", op));
		}

		// Token: 0x06000E8C RID: 3724 RVA: 0x0003CF24 File Offset: 0x0003B124
		public static Exception TooManyRightParentheses()
		{
			return ExprException._Syntax("The expression has too many closing parentheses.");
		}

		// Token: 0x06000E8D RID: 3725 RVA: 0x0003CF30 File Offset: 0x0003B130
		public static Exception UnresolvedRelation(string name, string expr)
		{
			return ExprException._Eval(SR.Format("The table [{0}] involved in more than one relation. You must explicitly mention a relation name in the expression '{1}'.", name, expr));
		}

		// Token: 0x06000E8E RID: 3726 RVA: 0x0003CF43 File Offset: 0x0003B143
		internal static EvaluateException BindFailure(string relationName)
		{
			return ExprException._Eval(SR.Format("Cannot find the parent relation '{0}'.", relationName));
		}

		// Token: 0x06000E8F RID: 3727 RVA: 0x0003CF55 File Offset: 0x0003B155
		public static Exception AggregateArgument()
		{
			return ExprException._Syntax("Syntax error in aggregate argument: Expecting a single column argument with possible 'Child' qualifier.");
		}

		// Token: 0x06000E90 RID: 3728 RVA: 0x0003CF61 File Offset: 0x0003B161
		public static Exception AggregateUnbound(string expr)
		{
			return ExprException._Eval(SR.Format("Unbound reference in the aggregate expression '{0}'.", expr));
		}

		// Token: 0x06000E91 RID: 3729 RVA: 0x0003CF73 File Offset: 0x0003B173
		public static Exception EvalNoContext()
		{
			return ExprException._Eval("Cannot evaluate non-constant expression without current row.");
		}

		// Token: 0x06000E92 RID: 3730 RVA: 0x0003CF7F File Offset: 0x0003B17F
		public static Exception ExpressionUnbound(string expr)
		{
			return ExprException._Eval(SR.Format("Unbound reference in the expression '{0}'.", expr));
		}

		// Token: 0x06000E93 RID: 3731 RVA: 0x0003CF91 File Offset: 0x0003B191
		public static Exception ComputeNotAggregate(string expr)
		{
			return ExprException._Eval(SR.Format("Cannot evaluate. Expression '{0}' is not an aggregate.", expr));
		}

		// Token: 0x06000E94 RID: 3732 RVA: 0x0003CFA3 File Offset: 0x0003B1A3
		public static Exception FilterConvertion(string expr)
		{
			return ExprException._Eval(SR.Format("Filter expression '{0}' does not evaluate to a Boolean term.", expr));
		}

		// Token: 0x06000E95 RID: 3733 RVA: 0x0003CFB5 File Offset: 0x0003B1B5
		public static Exception LookupArgument()
		{
			return ExprException._Syntax("Syntax error in Lookup expression: Expecting keyword 'Parent' followed by a single column argument with possible relation qualifier: Parent[(<relation_name>)].<column_name>.");
		}

		// Token: 0x06000E96 RID: 3734 RVA: 0x0003CFC1 File Offset: 0x0003B1C1
		public static Exception InvalidType(string typeName)
		{
			return ExprException._Eval(SR.Format("Invalid type name '{0}'.", typeName));
		}

		// Token: 0x06000E97 RID: 3735 RVA: 0x0003CFD3 File Offset: 0x0003B1D3
		public static Exception InvalidHoursArgument()
		{
			return ExprException._Eval("'hours' argument is out of range. Value must be between -14 and +14.");
		}

		// Token: 0x06000E98 RID: 3736 RVA: 0x0003CFDF File Offset: 0x0003B1DF
		public static Exception InvalidMinutesArgument()
		{
			return ExprException._Eval("'minutes' argument is out of range. Value must be between -59 and +59.");
		}

		// Token: 0x06000E99 RID: 3737 RVA: 0x0003CFEB File Offset: 0x0003B1EB
		public static Exception InvalidTimeZoneRange()
		{
			return ExprException._Eval("Provided range for time one exceeds total of 14 hours.");
		}

		// Token: 0x06000E9A RID: 3738 RVA: 0x0003CFF7 File Offset: 0x0003B1F7
		public static Exception MismatchKindandTimeSpan()
		{
			return ExprException._Eval("Kind property of provided DateTime argument, does not match 'hours' and 'minutes' arguments.");
		}

		// Token: 0x06000E9B RID: 3739 RVA: 0x0003D003 File Offset: 0x0003B203
		public static Exception UnsupportedDataType(Type type)
		{
			return ExceptionBuilder._Argument(SR.Format("A DataColumn of type '{0}' does not support expression.", type.FullName));
		}
	}
}
