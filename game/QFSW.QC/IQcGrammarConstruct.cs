using System;

namespace QFSW.QC
{
	// Token: 0x02000022 RID: 34
	public interface IQcGrammarConstruct
	{
		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000076 RID: 118
		int Precedence { get; }

		// Token: 0x06000077 RID: 119
		bool Match(string value, Type type);

		// Token: 0x06000078 RID: 120
		object Parse(string value, Type type, Func<string, Type, object> recursiveParser);
	}
}
