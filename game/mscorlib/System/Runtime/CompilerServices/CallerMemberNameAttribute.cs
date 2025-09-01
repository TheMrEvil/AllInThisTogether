using System;

namespace System.Runtime.CompilerServices
{
	/// <summary>Allows you to obtain the method or property name of the caller to the method.</summary>
	// Token: 0x020007E1 RID: 2017
	[AttributeUsage(AttributeTargets.Parameter, Inherited = false)]
	public sealed class CallerMemberNameAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.CompilerServices.CallerMemberNameAttribute" /> class.</summary>
		// Token: 0x060045D6 RID: 17878 RVA: 0x00002050 File Offset: 0x00000250
		public CallerMemberNameAttribute()
		{
		}
	}
}
