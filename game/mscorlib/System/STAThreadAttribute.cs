using System;

namespace System
{
	/// <summary>Indicates that the COM threading model for an application is single-threaded apartment (STA).</summary>
	// Token: 0x0200018F RID: 399
	[AttributeUsage(AttributeTargets.Method)]
	public sealed class STAThreadAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.STAThreadAttribute" /> class.</summary>
		// Token: 0x06000FCC RID: 4044 RVA: 0x00002050 File Offset: 0x00000250
		public STAThreadAttribute()
		{
		}
	}
}
