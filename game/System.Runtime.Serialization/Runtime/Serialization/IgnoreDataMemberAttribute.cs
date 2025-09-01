using System;

namespace System.Runtime.Serialization
{
	/// <summary>When applied to the member of a type, specifies that the member is not part of a data contract and is not serialized.</summary>
	// Token: 0x020000E8 RID: 232
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, Inherited = false, AllowMultiple = false)]
	public sealed class IgnoreDataMemberAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Serialization.IgnoreDataMemberAttribute" /> class.</summary>
		// Token: 0x06000D41 RID: 3393 RVA: 0x000254FF File Offset: 0x000236FF
		public IgnoreDataMemberAttribute()
		{
		}
	}
}
