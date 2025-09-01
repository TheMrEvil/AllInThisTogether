using System;

namespace System.Runtime.Versioning
{
	// Token: 0x0200063A RID: 1594
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Constructor | AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
	internal sealed class NonVersionableAttribute : Attribute
	{
		// Token: 0x06003C21 RID: 15393 RVA: 0x00002050 File Offset: 0x00000250
		public NonVersionableAttribute()
		{
		}
	}
}
