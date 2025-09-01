using System;

namespace System.Runtime.CompilerServices
{
	/// <summary>Marks a program element as read-only.</summary>
	// Token: 0x020007FD RID: 2045
	[AttributeUsage(AttributeTargets.All, Inherited = false)]
	public sealed class IsReadOnlyAttribute : Attribute
	{
		/// <summary>Creates a new instance of the <see cref="T:System.Runtime.CompilerServices.IsReadOnlyAttribute" /> class.</summary>
		// Token: 0x0600460F RID: 17935 RVA: 0x00002050 File Offset: 0x00000250
		public IsReadOnlyAttribute()
		{
		}
	}
}
