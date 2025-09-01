using System;

namespace QFSW.QC
{
	// Token: 0x02000023 RID: 35
	public interface IQcParser
	{
		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000079 RID: 121
		int Priority { get; }

		// Token: 0x0600007A RID: 122
		bool CanParse(Type type);

		// Token: 0x0600007B RID: 123
		object Parse(string value, Type type, Func<string, Type, object> recursiveParser);
	}
}
