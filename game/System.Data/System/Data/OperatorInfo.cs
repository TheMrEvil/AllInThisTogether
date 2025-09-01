using System;

namespace System.Data
{
	// Token: 0x020000F0 RID: 240
	internal sealed class OperatorInfo
	{
		// Token: 0x06000E59 RID: 3673 RVA: 0x0003CBDE File Offset: 0x0003ADDE
		internal OperatorInfo(Nodes type, int op, int pri)
		{
			this._type = type;
			this._op = op;
			this._priority = pri;
		}

		// Token: 0x0400090C RID: 2316
		internal Nodes _type;

		// Token: 0x0400090D RID: 2317
		internal int _op;

		// Token: 0x0400090E RID: 2318
		internal int _priority;
	}
}
