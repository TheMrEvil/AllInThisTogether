using System;

namespace QFSW.QC
{
	// Token: 0x0200002A RID: 42
	public interface IQcPreprocessor
	{
		// Token: 0x1700001E RID: 30
		// (get) Token: 0x060000A4 RID: 164
		int Priority { get; }

		// Token: 0x060000A5 RID: 165
		string Process(string text);
	}
}
