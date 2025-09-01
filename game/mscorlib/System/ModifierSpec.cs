using System;
using System.Text;

namespace System
{
	// Token: 0x0200025F RID: 607
	internal interface ModifierSpec
	{
		// Token: 0x06001BCD RID: 7117
		Type Resolve(Type type);

		// Token: 0x06001BCE RID: 7118
		StringBuilder Append(StringBuilder sb);
	}
}
