using System;

namespace QFSW.QC.Grammar
{
	// Token: 0x0200000B RID: 11
	public interface IBinaryOperator
	{
		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000032 RID: 50
		Type LArg { get; }

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000033 RID: 51
		Type RArg { get; }

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000034 RID: 52
		Type Ret { get; }

		// Token: 0x06000035 RID: 53
		object Invoke(object left, object right);
	}
}
