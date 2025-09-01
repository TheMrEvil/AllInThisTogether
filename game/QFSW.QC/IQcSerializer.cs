using System;

namespace QFSW.QC
{
	// Token: 0x0200003A RID: 58
	public interface IQcSerializer
	{
		// Token: 0x1700002E RID: 46
		// (get) Token: 0x0600014C RID: 332
		int Priority { get; }

		// Token: 0x0600014D RID: 333
		bool CanSerialize(Type type);

		// Token: 0x0600014E RID: 334
		string SerializeFormatted(object value, QuantumTheme theme, Func<object, QuantumTheme, string> recursiveSerializer);
	}
}
