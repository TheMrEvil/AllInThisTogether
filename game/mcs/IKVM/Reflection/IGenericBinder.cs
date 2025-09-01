using System;

namespace IKVM.Reflection
{
	// Token: 0x0200005B RID: 91
	internal interface IGenericBinder
	{
		// Token: 0x06000470 RID: 1136
		Type BindTypeParameter(Type type);

		// Token: 0x06000471 RID: 1137
		Type BindMethodParameter(Type type);
	}
}
