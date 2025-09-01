using System;

namespace IKVM.Reflection
{
	// Token: 0x0200005A RID: 90
	internal interface IGenericContext
	{
		// Token: 0x0600046E RID: 1134
		Type GetGenericTypeArgument(int index);

		// Token: 0x0600046F RID: 1135
		Type GetGenericMethodArgument(int index);
	}
}
