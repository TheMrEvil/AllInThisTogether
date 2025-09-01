using System;

namespace System.Runtime.CompilerServices
{
	/// <summary>Allows you to obtain the full path of the source file that contains the caller. This is the file path at the time of compile.</summary>
	// Token: 0x020007DF RID: 2015
	[AttributeUsage(AttributeTargets.Parameter, Inherited = false)]
	public sealed class CallerFilePathAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.CompilerServices.CallerFilePathAttribute" /> class.</summary>
		// Token: 0x060045D4 RID: 17876 RVA: 0x00002050 File Offset: 0x00000250
		public CallerFilePathAttribute()
		{
		}
	}
}
