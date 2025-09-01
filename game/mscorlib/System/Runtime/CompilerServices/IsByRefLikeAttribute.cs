using System;

namespace System.Runtime.CompilerServices
{
	/// <summary>Indicates that a structure is byref-like.</summary>
	// Token: 0x020007FB RID: 2043
	[AttributeUsage(AttributeTargets.Struct)]
	public sealed class IsByRefLikeAttribute : Attribute
	{
		/// <summary>Creates a new instance of the <see cref="T:System.Runtime.CompilerServices.IsByRefLikeAttribute" /> class.</summary>
		// Token: 0x0600460E RID: 17934 RVA: 0x00002050 File Offset: 0x00000250
		public IsByRefLikeAttribute()
		{
		}
	}
}
