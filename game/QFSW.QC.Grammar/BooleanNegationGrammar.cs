using System;
using System.Text.RegularExpressions;

namespace QFSW.QC.Grammar
{
	// Token: 0x0200000F RID: 15
	public class BooleanNegationGrammar : IQcGrammarConstruct
	{
		// Token: 0x1700002E RID: 46
		// (get) Token: 0x06000045 RID: 69 RVA: 0x0000269A File Offset: 0x0000089A
		public int Precedence
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x06000046 RID: 70 RVA: 0x0000269D File Offset: 0x0000089D
		public bool Match(string value, Type type)
		{
			return type == typeof(bool) && this._negationRegex.IsMatch(value);
		}

		// Token: 0x06000047 RID: 71 RVA: 0x000026BF File Offset: 0x000008BF
		public object Parse(string value, Type type, Func<string, Type, object> recursiveParser)
		{
			value = value.Substring(1);
			return !(bool)recursiveParser(value, type);
		}

		// Token: 0x06000048 RID: 72 RVA: 0x000026DF File Offset: 0x000008DF
		public BooleanNegationGrammar()
		{
		}

		// Token: 0x0400000E RID: 14
		private readonly Regex _negationRegex = new Regex("^!\\S+$");
	}
}
