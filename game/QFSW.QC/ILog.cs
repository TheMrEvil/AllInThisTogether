using System;
using UnityEngine;

namespace QFSW.QC
{
	// Token: 0x02000013 RID: 19
	public interface ILog
	{
		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000038 RID: 56
		string Text { get; }

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000039 RID: 57
		LogType Type { get; }

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600003A RID: 58
		bool NewLine { get; }
	}
}
