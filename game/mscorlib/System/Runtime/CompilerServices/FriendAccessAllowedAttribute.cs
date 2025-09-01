using System;

namespace System.Runtime.CompilerServices
{
	// Token: 0x02000837 RID: 2103
	[FriendAccessAllowed]
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Constructor | AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Event | AttributeTargets.Interface, AllowMultiple = false, Inherited = false)]
	internal sealed class FriendAccessAllowedAttribute : Attribute
	{
		// Token: 0x060046BC RID: 18108 RVA: 0x00002050 File Offset: 0x00000250
		public FriendAccessAllowedAttribute()
		{
		}
	}
}
