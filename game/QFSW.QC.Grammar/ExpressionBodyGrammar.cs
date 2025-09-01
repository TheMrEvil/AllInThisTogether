using System;
using System.Text.RegularExpressions;
using QFSW.QC.Utilities;

namespace QFSW.QC.Grammar
{
	// Token: 0x02000010 RID: 16
	public class ExpressionBodyGrammar : IQcGrammarConstruct
	{
		// Token: 0x1700002F RID: 47
		// (get) Token: 0x06000049 RID: 73 RVA: 0x000026F7 File Offset: 0x000008F7
		public int Precedence
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x0600004A RID: 74 RVA: 0x000026FA File Offset: 0x000008FA
		public bool Match(string value, Type type)
		{
			return this._expressionBodyRegex.IsMatch(value);
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00002708 File Offset: 0x00000908
		public object Parse(string value, Type type, Func<string, Type, object> recursiveParser)
		{
			bool flag = false;
			if (value.EndsWith("?"))
			{
				flag = true;
				value = value.Substring(0, value.Length - 1);
			}
			value = value.ReduceScope('{', '}');
			object obj = QuantumConsoleProcessor.InvokeCommand(value);
			if (obj == null)
			{
				if (!flag)
				{
					throw new ParserInputException("Expression body {" + value + "} evaluated to null. If this is intended, please use nullable expression bodies, {expr}?");
				}
				if (type.IsClass)
				{
					return obj;
				}
				throw new ParserInputException(string.Concat(new string[]
				{
					"Expression body {",
					value,
					"} evaluated to null which is incompatible with the expected type '",
					type.GetDisplayName(false),
					"'."
				}));
			}
			else
			{
				if (obj.GetType().IsCastableTo(type, true))
				{
					return type.Cast(obj);
				}
				throw new ParserInputException(string.Concat(new string[]
				{
					"Expression body {",
					value,
					"} evaluated to an object of type '",
					obj.GetType().GetDisplayName(false),
					"', which is incompatible with the expected type '",
					type.GetDisplayName(false),
					"'."
				}));
			}
		}

		// Token: 0x0600004C RID: 76 RVA: 0x0000280B File Offset: 0x00000A0B
		public ExpressionBodyGrammar()
		{
		}

		// Token: 0x0400000F RID: 15
		private readonly Regex _expressionBodyRegex = new Regex("^{.+}\\??$");
	}
}
