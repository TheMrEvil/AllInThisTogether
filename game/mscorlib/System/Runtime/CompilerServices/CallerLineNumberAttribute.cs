using System;

namespace System.Runtime.CompilerServices
{
	/// <summary>Allows you to obtain the line number in the source file at which the method is called.</summary>
	// Token: 0x020007E0 RID: 2016
	[AttributeUsage(AttributeTargets.Parameter, Inherited = false)]
	public sealed class CallerLineNumberAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.CompilerServices.CallerLineNumberAttribute" /> class.</summary>
		// Token: 0x060045D5 RID: 17877 RVA: 0x00002050 File Offset: 0x00000250
		public CallerLineNumberAttribute()
		{
		}
	}
}
