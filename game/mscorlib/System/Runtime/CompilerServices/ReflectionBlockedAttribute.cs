using System;

namespace System.Runtime.CompilerServices
{
	// Token: 0x02000818 RID: 2072
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Interface | AttributeTargets.Delegate, Inherited = false, AllowMultiple = false)]
	internal class ReflectionBlockedAttribute : Attribute
	{
		// Token: 0x06004658 RID: 18008 RVA: 0x00002050 File Offset: 0x00000250
		public ReflectionBlockedAttribute()
		{
		}
	}
}
